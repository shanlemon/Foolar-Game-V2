using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class Bubble : Spells2 {

	public Vector3 offset;


	public override void cast() {
		if (canCast()) {
            currentCharges--;
            playerCasting.mana -= manaCost * 10;

            StartCoroutine(ExecuteAfterTime(delay));
            //Instantiate(effect, player.transform.Find("Shoot Target").position, rotation);
        }
	}

	[Command]
	public void CmdBubble(Vector3 loc, Quaternion rotation) {
		anim.Play("Wall");
		GameObject obj = Instantiate(effect, loc, rotation);
		FindObjectOfType<AudioManager> ().Play ("bubble_start");
		NetworkServer.Spawn(obj);
	}

	IEnumerator ExecuteAfterTime(float time) {
		yield return new WaitForSeconds(time);

		Quaternion rotation = player.cam.transform.rotation;
		CmdBubble(player.transform.position + offset, Quaternion.Euler(Vector3.zero));
	}


	public override void showHologram() {
		throw new NotImplementedException();
	}

	public override void deleteHologram() {
		throw new NotImplementedException();
	}

	public override void updateHologram() {
		throw new NotImplementedException();
	}
}
