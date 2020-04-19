using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {

    public GameObject Player;

    public float smooth = 5;
    public Vector3 offset = new Vector3(0,0,-10);
    private Vector3 cameraOffset = new Vector3(0, 0, 0);
    private Vector3 target;

    private Vector3 move;

	void Start () {
        Player = GameObject.Find("/Player");
	}
	
	void FixedUpdate () {
        target = Player.transform.position;

        move = Time.fixedDeltaTime*(target - transform.position)/smooth;


        transform.Translate(move);
        transform.position = new Vector3(transform.position.x+offset.x, transform.position.y+offset.y, offset.z);
	}
}
