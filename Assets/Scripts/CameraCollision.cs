﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCollision : MonoBehaviour {

	public float minDistance = 1f;
	public float maxDistance = 4f;
	public float smooth = 10f;

	Vector3 dollyDir;
	public Vector3 dollyDirAdjusted;
	public float distance;

	private void Awake() {
		dollyDir = transform.localPosition.normalized;
		distance = transform.localPosition.magnitude;

	}

	// Update is called once per frame
	void Update () {

		Vector3 desiredCameraPos = transform.TransformPoint(dollyDir * maxDistance);
		RaycastHit hit;
		
		if(Physics.Linecast(transform.position, desiredCameraPos, out hit)) {
			distance = Mathf.Clamp((hit.distance * .9f), minDistance, maxDistance);
		} else {
			distance = maxDistance;
		}

		transform.localPosition = Vector3.Lerp(transform.localPosition, dollyDir * distance + dollyDirAdjusted, Time.deltaTime * smooth);

	}
}
