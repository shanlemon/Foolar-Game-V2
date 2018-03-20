using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathScript : MonoBehaviour {

	public float lifeSpan;
	public GameObject deathAnimation;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if ((lifeSpan -= Time.deltaTime) <= 0) {
			if(deathAnimation != null)
				Instantiate (deathAnimation, gameObject.transform.position, gameObject.transform.rotation);
			Destroy (gameObject);
		}
	}

    

    
}
