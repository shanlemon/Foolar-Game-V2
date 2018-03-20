using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallThrower : MonoBehaviour {

	public GameObject ball;
	public GameObject target;
	public GameObject player;

	[SerializeField]
	public BallThrowLvl[] levels;

	public int currentLvl;

	public float height;
	public float gravity = Physics.gravity.y;

	private void Start() {
		target = player;

		ball = GameObject.Find("BallOffline");
		player = GameObject.Find("PlayerOffline_Training");

		FindObjectOfType<GameController_Offline>().lockControl();
	}

	public void changeLevel(int i) {
		currentLvl = i;
		Debug.Log("Current Level: " + i);
	}

	public void setTarget(GameObject targ) {
		target = targ;
	}

	public void tossBall() {
		BallThrowLvl lvl = levels[currentLvl];
		transform.position = lvl.throwPos.position;
		ball.transform.position = transform.position;
		player.transform.position = lvl.playerPos.position;
		Rigidbody rb = ball.GetComponent<Rigidbody>();
		rb.velocity = CalculateLaunchVelocity(lvl);
	}

	Vector3 CalculateLaunchVelocity(BallThrowLvl lvl) {
		gravity = lvl.gravity;
		height = lvl.height;

		float displacementY = target.transform.position.y - ball.transform.position.y;
		Vector3 displacementXZ = new Vector3(target.transform.position.x - ball.transform.position.x, 0, target.transform.position.z - ball.transform.position.z);

		Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * gravity * height);
		Vector3 velocityXZ = displacementXZ / (Mathf.Sqrt(-2 * height / gravity) + Mathf.Sqrt(2 * (displacementY - height) / gravity));

	
		
		return velocityXZ + velocityY;
	}

}
