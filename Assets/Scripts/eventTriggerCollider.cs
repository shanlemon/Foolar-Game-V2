using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eventTriggerCollider : MonoBehaviour {

	public UnityEngine.Events.UnityEvent afterEvent;
	public string colliderTag;

	void OnTriggerEnter(Collider other) {
		if(other.tag == colliderTag) {
			afterEvent.Invoke();
		}
	}
}
