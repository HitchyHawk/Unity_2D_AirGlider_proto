using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
public class PlayerMovement : MonoBehaviour {

    public PlayerInput pl;
    public Rigidbody2D rb;
    float degToRad = 2 * Mathf.PI / 360;

    private int rD;
    private bool action;

    public float spinMax = 125;
    public float spinSpeed = 37;
    public float spinSpecial = 2;
    public float spinDrag = 10;
    public float spinDrop = 1.4f;

    private float rotation = 0;
    private float spinRotate = 0;
    private float spinWorldRotation = 0;

    public Vector3 flyVelocity = new Vector3(0.1f,0,0);
    private Vector2 flyVector;
    public Vector2 gravVector = new Vector2Int(0,-1);
    public float gravity = -1;
    private float flySpeed;
    public float flyAcceleration = 2;
    public float flyMax = 100;

    private Vector3 temp;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        pl = GetComponent<PlayerInput>();
    }

    void FixedUpdate () {
        rD = pl.rotateD;
        action = pl.action;


        //the value for how much rotation needs to be applied
        //current world rotation
        
        temp = 4*Vector3.Cross(Vector3.Normalize(flyVector),Vector3.Normalize(flyVelocity) /*new Vector3(gravVector.x, gravVector.y, 0)*/);
        if (!action)
        {

            spinRotate = (GetsRotation(rD, action)) * Time.fixedDeltaTime;
            spinWorldRotation += spinRotate + temp.z;
        }
        else
        {
            temp /= spinDrop;
            if (spinRotate != 0) { spinRotate -= spinDrag * spinRotate / Mathf.Abs(spinRotate); spinRotate *= Time.fixedDeltaTime; }
            spinWorldRotation += (spinRotate + temp.z) * spinSpecial;
        }
        
        //keeps World rotation in 360    
        if (Mathf.Abs(spinWorldRotation) > 360) spinWorldRotation -= 360 * spinWorldRotation / Mathf.Abs(spinWorldRotation);


        //The flight velocity based off of world rotation
        flyVector = new Vector3(Mathf.Cos(spinWorldRotation * degToRad), Mathf.Sin(spinWorldRotation * degToRad), 0);

        if (!action) flyVelocity = GetFlyVelocity() * Time.fixedDeltaTime;
        else
        {
            flyVector = new Vector3(Mathf.Cos(spinWorldRotation * degToRad), Mathf.Sin(spinWorldRotation * degToRad), 0);
            flyVelocity += new Vector3(gravVector.x, gravVector.y, 0) * gravity * Time.fixedDeltaTime;// / 4;
            flySpeed = Vector3.Magnitude(flyVelocity) * Vector3.Dot(flyVector, Vector3.Normalize(flyVelocity)) / Time.fixedDeltaTime;
        }



        //applying the the transformations
        Transormation(flyVelocity, spinRotate + temp.z,spinSpecial,action);
    }

    void Transormation(Vector3 v,float r,float s,bool i)
    {
        //resets position and flyspeed
        if (Input.GetKey(KeyCode.R))
        {
            transform.position = new Vector3(0, 0, 0);
            flyVelocity = new Vector3(0, 0, 0);
            flySpeed = 0;
        }

        if (i)
        {
            rb.transform.Rotate(0, 0, r * s);
            rb.transform.Translate(v, Space.World);
            rb.transform.localScale = new Vector3(0.5f, 1, 1);
            /*
            transform.Rotate(0, 0, r * s);
            transform.Translate(v, Space.World);
            transform.localScale = new Vector3(0.5f, 1, 1);
            */
        }
        else
        {
            rb.transform.Rotate(0, 0, r);
            rb.transform.Translate(v, Space.World);
            rb.transform.localScale = new Vector3(1, 1, 1);
            /*
            transform.Rotate(0, 0, r);
            transform.Translate(v, Space.World);
            transform.localScale = new Vector3(1, 1, 1);
            */
        }
    }

    Vector3 GetFlyVelocity() {

        //addes acceleration
        flySpeed += Vector3.Dot(gravVector, flyVector) * flyAcceleration;

        //max speed
        if (Mathf.Abs(flySpeed) > flyMax) flySpeed = flyMax * flySpeed / Mathf.Abs(flySpeed);

        flyVector *= flySpeed ;

        return new Vector3(flyVector.x + gravVector.x * gravity, flyVector.y + gravVector.y * gravity, 0);

    }

    float GetsRotation(int r, bool a) {

        rotation += r * spinSpeed;

        //maxs out the rotation
        if (Mathf.Abs(rotation) >= spinMax) rotation = r * spinMax;

        //Drag force, using rotation instead of r because want it to slow down when not actively rotating
        if (rotation != 0) rotation -= spinDrag * rotation / Mathf.Abs(rotation);

        return rotation;
    }

}
