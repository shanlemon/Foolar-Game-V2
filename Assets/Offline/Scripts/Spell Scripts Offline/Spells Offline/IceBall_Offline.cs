using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class IceBall_Offline : Spells2_Offline {


	public override void cast() {
		if (canCast()) {
            currentCharges--;
            playerCasting.mana -= manaCost * 10;
            anim.Play("Iceball");
            StartCoroutine(ExecuteAfterTime(delay));
        }

	}


	public void CmdIceball(Vector3 loc, Quaternion rotation) {
		Instantiate(effect, loc, rotation);
	}

	IEnumerator ExecuteAfterTime(float time) {

		yield return new WaitForSeconds(time);

		Quaternion rotation = player.cam.transform.rotation;
		CmdIceball(player.transform.Find("Shoot Target").position, rotation);

	}

	public override void deleteHologram() {
        throw new NotImplementedException();
    }

    public override void showHologram() {
        throw new NotImplementedException();
    }

    public override void updateHologram() {
        throw new NotImplementedException();
    }

}
