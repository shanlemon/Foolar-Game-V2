using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballMoveExample : MonoBehaviour {

	public float speed;
	public Vector3 startPos, endPos;
	private float dTime;
	private bool start = true;
	public float time;

	void onEnable() {
		transform.position = startPos;
		start = true;
		GetComponent<Rigidbody>().isKinematic = true;
	}

	void OnDisable() {
		GetComponent<Rigidbody>().isKinematic = false;
	}

	// Update is called once per frame
	void FixedUpdate () {

		
		dTime += Time.deltaTime;
		
		if(dTime >= time) {
			start = !start;
			dTime = 0;
		}
		

		/*
		dTime += .1f;
		if(dTime >= 1) {
			start = !start;
			dTime = 0;
		}
		*/

		if (start) {
			transform.position = Vector3.Slerp(transform.position, endPos, speed);

		} else {
			transform.position = Vector3.Slerp(transform.position, startPos, speed);

		}

	}
}
