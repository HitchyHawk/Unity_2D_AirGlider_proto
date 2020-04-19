using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTracker : MonoBehaviour {

    public PlayerMovement target;


	// Use this for initialization
	void Start () {
        target = GetComponent<PlayerMovement>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
