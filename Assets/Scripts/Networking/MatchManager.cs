using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MatchManager : NetworkBehaviour {

	public GameObject[] blueSpawnPoints;
	public GameObject[] redSpawnPoints;

	[SyncVar]
	public double countdownToStart;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (isServer) {
			CmdUpdate();
		}

		if (isLocalPlayer) {
			Debug.Log("asdf");
			if(countdownToStart < 0) {
				Debug.Log("game started");
			}
		}

	}

	[Command]
	void CmdUpdate() {
		if (countdownToStart > 0) {
			countdownToStart -= Time.deltaTime;
		}
	}

}
