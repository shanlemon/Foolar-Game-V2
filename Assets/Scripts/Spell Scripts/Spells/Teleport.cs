using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class Teleport : Spells2 {

    public float range;
    public Vector3 offset;
    private GameObject hologram;


	public override void cast() {
		if (canCast()) {

            currentCharges--;
            playerCasting.mana -= manaCost * 10;

            Ray ray = player.cam.ViewportPointToRay(new Vector3(.5f, .5f, 0f));
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, range)) {
                Vector3 loc = hit.point + offset;
                anim.Play("Teleport");
                StartCoroutine(teleportToRay(delay, loc));
            } else if (hologram != null) {
                anim.Play("Teleport");
                StartCoroutine(teleportToHologram(delay, hologram));
            }
        }
	}

	IEnumerator teleportToHologram(float time, GameObject holo) {
		yield return new WaitForSeconds(time);
		player.transform.position = holo.transform.position;
	}

	IEnumerator teleportToRay(float time, Vector3 loc) {
		yield return new WaitForSeconds(time);
		player.transform.position = loc;
	}

	public override void showHologram() {
		if (canCast ()) {
			
			if (hologram == null) {
				Ray ray = player.cam.ViewportPointToRay (new Vector3 (.5f, .5f, 0f));
				RaycastHit hit;
				if (Physics.Raycast (ray, out hit, range)) {
					Vector3 loc = hit.point + offset;
					hologram = Instantiate (hologramPrefab, loc, Quaternion.Euler (-90, player.transform.eulerAngles.y, -90));
				}
			} else {
				deleteHologram ();
			}

		}
    }

    public override void deleteHologram() {
			Destroy (hologram);
			hologram = null;
    }

    public override void updateHologram() {
        if (hologram != null) {
            Ray ray = player.cam.ViewportPointToRay(new Vector3(.5f, .5f, 0f));
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, range)) {
                Vector3 loc = hit.point + offset;
                hologram.transform.position = loc;
                hologram.transform.rotation = Quaternion.Euler(-90, player.transform.eulerAngles.y, -90);
            }
        }
    }


}
