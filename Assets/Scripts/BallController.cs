using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour {

	private Rigidbody rb;
	private bool hit;
	public float maxVelocity;

	void Start () {
		rb = GetComponent<Rigidbody> ();
	}

	void FixedUpdate() {
		if (!hit) {
			rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
		}

		rb.velocity = new Vector3(Mathf.Clamp(rb.velocity.x, -maxVelocity, maxVelocity), Mathf.Clamp(rb.velocity.y, -maxVelocity, maxVelocity), Mathf.Clamp(rb.velocity.z, -maxVelocity, maxVelocity));
	}

	public void OnCollisionEnter(Collision collision) {
		hit = true;
        rb.constraints = RigidbodyConstraints.None;
    }

	public void resetBall() {
		rb.velocity = Vector3.zero;
		transform.position = new Vector3(0, 60, 0);
		//BallThrower bt = FindObjectOfType<BallThrower>();
		//if(bt != null) {
		//	bt.levels[bt.currentLvl]
		//}
		hit = false;
	}


	public void push (float power, GameObject obj) {
        //Vector3 shootDir = transform.position - trans.position; //calculate delta vector
        //shootDir.Normalize (); //normalize
        Vector3 shootDir = obj.GetComponent<Rigidbody>().velocity.normalized;

        rb.constraints = RigidbodyConstraints.None;

		//rb.velocity = shootDir * (power * .00000022222f);
        rb.AddForce (shootDir * (power *.00000022222f), ForceMode.Impulse);
	}

    public IEnumerator freezeBall(float time) {
        rb.constraints = RigidbodyConstraints.FreezeAll;
        yield return new WaitForSeconds(time);
        rb.constraints = RigidbodyConstraints.None;
    }


}
