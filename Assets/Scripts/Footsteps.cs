using UnityEngine;
using System.Collections;

public class Footsteps : MonoBehaviour {
	
	public PlayerControl cc;
	public AudioSource audio;

	void Start () {
		cc = FindObjectOfType<PlayerControl> ();
		audio.volume = 0f;
	}

	void Update() {
		if (cc.isGrounded == true && cc.rb.velocity.magnitude > 2f && !audio.isPlaying) {
			audio.volume = Random.Range (.8f, 1f);
			audio.pitch = Random.Range (.8f, 1.2f);

			audio.Play ();
		}
	}

}