using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball_Offline : Spells2_Offline {

	

	public override void cast() {
		if (canCast()) {
            currentCharges--;
            playerCasting.mana -= manaCost * 10;

            anim.Play("Fireball");
            StartCoroutine(ExecuteAfterTime(delay));
            //Instantiate(effect, player.transform.Find("Shoot Target").position, rotation);
        }
     }

	public void CmdFireball(Vector3 loc, Quaternion rotation) {
		Instantiate(effect, loc, rotation);
	}

	IEnumerator ExecuteAfterTime(float time) {
		yield return new WaitForSeconds(time);

		Quaternion rotation = player.cam.transform.rotation;
		CmdFireball(player.transform.Find("Shoot Target").position, rotation);
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
