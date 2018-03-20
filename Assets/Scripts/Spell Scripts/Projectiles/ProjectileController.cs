using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour {

	public float moveSpeed, acc;
	private Rigidbody rb;
	public CollisionEffect collision;

	public GameObject hitEffect;

	void Start () {	
		rb = GetComponent<Rigidbody>();
	}

	void FixedUpdate() {
		rb.MovePosition(transform.position + (moveSpeed * transform.forward * Time.deltaTime));
		//rb.AddForce (moveSpeed * transform.forward * Time.deltaTime);
		moveSpeed += acc;
	}

	public void delete() {
		if (hitEffect != null) {
			Instantiate(hitEffect, transform.position, transform.rotation);
		}
		rb.constraints = RigidbodyConstraints.FreezePosition;
		Destroy (gameObject, 1.25f);
	}

	public void delete(float t) {
		if (hitEffect != null) {
			Instantiate(hitEffect, transform.position, transform.rotation);
		}
		rb.constraints = RigidbodyConstraints.FreezePosition;
		Destroy(gameObject, t);
	}


	void OnTriggerEnter(Collider other) {
		GameObject obj = other.gameObject;
		if (obj.tag.Equals("Ball")) {
			collision.collideEffect (obj.GetComponent<BallController>(), gameObject);
			delete (.25f);
		}
		delete();
	}
}
