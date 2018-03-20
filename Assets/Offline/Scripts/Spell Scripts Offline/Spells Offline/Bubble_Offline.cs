using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Bubble_Offline : Spells2_Offline {

	public Vector3 offset;


	public override void cast() {
		if (canCast()) {
            currentCharges--;
            playerCasting.mana -= manaCost * 10;

            StartCoroutine(ExecuteAfterTime(delay));
            //Instantiate(effect, player.transform.Find("Shoot Target").position, rotation);
        }
	}

	public void CmdBubble(Vector3 loc, Quaternion rotation) {
		anim.Play("Wall");
		Instantiate(effect, loc, rotation);
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
