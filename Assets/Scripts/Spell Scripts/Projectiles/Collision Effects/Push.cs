using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Push : CollisionEffect {

	public float power;

	public override void collideEffect(BallController ball, GameObject obj) {
		ball.push (power, obj);
	}
}
