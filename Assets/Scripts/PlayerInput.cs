using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public int rotateD;
    public bool action;

  

    void Update()
    {
        //if both left and right
        rotateD = 0;
        if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) && (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))) rotateD = 0;

        //if left is press then this, else right this
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) rotateD = 1;
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) rotateD = -1;

        if (Input.GetKey(KeyCode.Space)) action = true;
        else action = false;
    }
}