using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Dash_Offline : Spells2_Offline {

    public float dashStrength;
    public float dashTime;

	public override void cast() {
		if (canCast()) {
            currentCharges--;
            playerCasting.mana -= manaCost * 10;
            StartCoroutine(dash());
        }
        
    }

	IEnumerator dash() { 
		Physics.gravity = Vector3.zero;
		player.rb.velocity = player.cam.transform.forward * dashStrength;
		anim.Play("Dash");
		yield return new WaitForSeconds(dashTime);
		Physics.gravity = new Vector3(0, PlayerControl.normalGrav, 0);
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
