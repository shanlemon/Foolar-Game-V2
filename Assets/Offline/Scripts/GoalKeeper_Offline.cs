using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GoalKeeper_Offline : MonoBehaviour {

	public TextMeshProUGUI team1Text;
	public TextMeshProUGUI team2Text;
	public TextMeshProUGUI timeText;


	public double time;

	public int team1;

	public int team2;

	public enum Teams
	{
		blue, orange
	}
	

	public BallController ball;

	void Start () {
		team1Text.text = team1.ToString();
		team2Text.text = team2.ToString();
		timeText.text = Time.time.ToString();
	}

	
	void Update () {
		timeText.text = time.ToString("F1");
		
		CmdUpdateTime();

		
	}

	void CmdUpdateTime() {
		time += Time.deltaTime;
	}

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
		//scoreText.text = score[0] + "   00:00   " + score[1];
		Debug.Log("Team 1: " + team1 + "\n Team 2: " + team2);
	}
}
