using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;

public class GoalKeeper : NetworkBehaviour {

	public TextMeshProUGUI team1Text;
	public TextMeshProUGUI team2Text;
	public TextMeshProUGUI timeText;

	[SyncVar]
	public double time;

	[SyncVar]
	public int team1;

	[SyncVar]
	public int team2;

	public enum Teams {
		blue, orange
	}


	public BallController ball;

	void Start() {
		team1Text.text = team1.ToString();
		team2Text.text = team2.ToString();
		timeText.text = Time.time.ToString();
	}

	private void find() {
		ball = FindObjectOfType<BallController>();
		team1Text = GameObject.Find("Team1Score").GetComponent<TextMeshProUGUI>();
		team2Text = GameObject.Find("Team1Score").GetComponent<TextMeshProUGUI>();
		timeText = GameObject.Find("TimeText").GetComponent<TextMeshProUGUI>();

	}

	void Update() {
		if (ball == null || team1Text == null || team2Text == null || timeText == null) {
			find();
		}

		timeText.text = time.ToString("F1");
		
		if (!isServer) {
			return;
		}
		CmdUpdateTime();
		
		
	}

	[Command] 
	void CmdUpdateTime() {
		time += Time.deltaTime;
	}

	[ClientRpc]
	public void RpcUpdateScore(int teamIndex){
		//score [teamIndex]++;
		//if (!isServer) {
		//	return;
		//}
		switch (teamIndex) {
			case 0:
				team1++;
				team1Text.text = team1.ToString();
				break;
			case 1:
				team2++;
				team2Text.text = team2.ToString();
				break;
			default:
				Debug.Log("goalIndex > 1");
				break;
		}
	}

	public void resetGame(){
		ball.resetBall();
		team1Text.text = team1.ToString();
		team2Text.text = team2.ToString();

		CmdResetPlayers();
		//scoreText.text = score[0] + "   00:00   " + score[1];
		Debug.Log("Team 1: " + team1 + "\n Team 2: " + team2);
	}



	[Command]
	public void CmdResetPlayers() {
		Debug.Log("running cmd reset players");
		if (isServer) {
			foreach(GameObject obj in GameObject.FindGameObjectsWithTag("Player")) {
				Debug.Log(obj.name);
				StartCoroutine(resetAllPlayerPositions(obj));
			}
		}
	}

	IEnumerator resetAllPlayerPositions(GameObject obj) {
		yield return new WaitForSeconds(1);
		Debug.Log("reseting player positions");
		MatchManager mm = FindObjectOfType<MatchManager>();
		if (obj.GetComponent<PlayerControl>().playerTeam == 0) {
			obj.transform.position = mm.blueSpawnPoints[Random.Range(0, mm.blueSpawnPoints.Length)].transform.position;
		} else if (obj.GetComponent<PlayerControl>().playerTeam == 1) {
			obj.transform.position = mm.redSpawnPoints[Random.Range(0, mm.redSpawnPoints.Length)].transform.position;
		}
	}
}
