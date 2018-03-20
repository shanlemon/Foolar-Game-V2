using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Fireball : Spells2 {

	

	public override void cast() {
		if (canCast()) {
            currentCharges--;
            playerCasting.mana -= manaCost * 10;

            anim.Play("Fireball");
            StartCoroutine(ExecuteAfterTime(delay));
            //Instantiate(effect, player.transform.Find("Shoot Target").position, rotation);
        }
     }

	[Command]
	public void CmdFireball(Vector3 loc, Quaternion rotation) {
		GameObject obj = Instantiate(effect, loc, rotation);
		NetworkServer.Spawn(obj);
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
