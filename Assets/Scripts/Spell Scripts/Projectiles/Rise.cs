using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class Rise : NetworkBehaviour {

	public float riseSpeed;
	public float stopHeight;
	public bool rise;

	

	[Server]
	void FixedUpdate() {
		if (isServer == false) {
			return;
		}

		if (rise) {
			if (transform.position.y < stopHeight) {
				Vector3 pos = transform.position;
				pos.y += riseSpeed;
				transform.position = pos;

			}
			if (transform.position.y > stopHeight) {
				Vector3 pos = transform.position;
				pos.y = stopHeight;
				transform.position = pos;
			}
		} else {
			if (transform.position.y > -19) {
				Vector3 pos = transform.position;
				pos.y -= riseSpeed;
				transform.position = pos;

			}
			if (transform.position.y < -19) {
				Vector3 pos = transform.position;
				pos.y = -19;
				transform.position = pos;
			}
		}

	}
}
