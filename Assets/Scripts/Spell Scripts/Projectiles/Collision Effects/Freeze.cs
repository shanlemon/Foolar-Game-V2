using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Freeze : CollisionEffect {

	public float freezeTime;

	public override void collideEffect(BallController ball, GameObject obj) {
		ball.StartCoroutine(ball.freezeBall(freezeTime));
	}
}
