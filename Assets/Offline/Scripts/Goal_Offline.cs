using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal_Offline : MonoBehaviour {

	public GoalKeeper_Offline goalKeeper;
	public int teamIndex;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other) {
		GameObject obj = other.gameObject;
		if (obj.tag.Equals ("Ball")) {
			goalKeeper.RpcUpdateScore(teamIndex);
			goalKeeper.resetGame ();
		}
	}

}
