using UnityEngine;
using System.Collections;

public class Footsteps : MonoBehaviour {
	
	public PlayerControl cc;
	public PlayerControl_Offline cc2;
	public AudioSource audio;

	void Start () {
		cc = FindObjectOfType<PlayerControl> ();
		cc2 = FindObjectOfType<PlayerControl_Offline>();
		audio.volume = 0f;
	}

	void Update() {
		if(cc != null) {
			//if (cc.isGrounded && Mathf.Abs(cc.rb.velocity.sqrMagnitude) > .01 && !audio.isPlaying) {
			if((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S)|| Input.GetKey(KeyCode.D)) && !audio.isPlaying && cc.isGrounded) { 
				audio.volume = Random.Range(.8f, 1f);
				audio.pitch = Random.Range(.8f, 1.2f);

				audio.Play();
			}
		}else if(cc2 != null) {
			if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)) && !audio.isPlaying && cc2.isGrounded) {
				audio.volume = Random.Range(.8f, 1f);
				audio.pitch = Random.Range(.8f, 1.2f);

				audio.Play();
			}
		}else {
			Debug.Log("in footsteps script, both player controllers are null");
		}
		
	}

}