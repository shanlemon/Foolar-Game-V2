using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterpolateMove : MonoBehaviour {

	public bool enable;
	public bool log;
	public Vector3 endPosition;
	public Quaternion endRotation;
	public float interpolateSpeed;
	public UnityEngine.Events.UnityEvent afterEvent;
	private float dTime;
	public float time;


	void Update() {
		if (log) {
			Debug.Log("Current Position: " + transform.position.ToString("F4"));
			Debug.Log("Current Rotation: " + transform.rotation.ToString("F4"));
		}
	}

	public void enableEvent() {
		enable = true;
	}

	// Update is called once per frame
	void FixedUpdate () {
		if (enable) {
			transform.rotation = Quaternion.Slerp(transform.rotation, endRotation, interpolateSpeed *  .33f);
			transform.position = Vector3.Lerp(transform.position, endPosition, interpolateSpeed);
			dTime += Time.deltaTime;
		}

		if ((transform.position == endPosition && transform.rotation == endRotation) || dTime >= time) {
			afterEvent.Invoke();
			enable = false;
		}
	}

}
