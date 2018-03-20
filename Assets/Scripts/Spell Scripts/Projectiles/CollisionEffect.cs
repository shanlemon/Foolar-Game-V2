using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CollisionEffect : MonoBehaviour {

	public abstract void collideEffect(BallController ball, GameObject obj);
}
