using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Wall : Spells2 {

    public float range;
    public Vector3 offset;
	public Vector3 hologramOffset;
	private GameObject hologram;


	public override void cast() {
        if (!isLocalPlayer) 
            return;

		if (canCast ()) {
			currentCharges--;
			playerCasting.mana -= manaCost * 10;

			Ray ray = player.cam.ViewportPointToRay (new Vector3 (.5f, .5f, 0f));
			RaycastHit hit;
		
			if (Physics.Raycast (ray, out hit, range)) {
				Vector3 loc = hit.point + offset;
				Quaternion rotation = Quaternion.Euler (-90, player.transform.eulerAngles.y, 0);
				CmdWall (loc, rotation);
			} else if (hologram != null) {
				CmdWall (hologram.transform.position, hologram.transform.rotation);
			}
		}
    }

	[Command]
	public void CmdWall( Vector3 loc, Quaternion rotation) {
		anim.Play("Wall");
		StartCoroutine(placeWall(delay,loc, rotation));
	}

	IEnumerator placeWall(float time, Vector3 loc, Quaternion rotation) {
		yield return new WaitForSeconds(time);
		GameObject obj = Instantiate(effect, loc, rotation);
		//GameObject obj = Instantiate(effect, new Vector3(0,0,0), new Quaternion(-90,0,-90,0));
		//obj.transform.GetChild(0).transform.position = loc;
		//obj.transform.GetChild(0).transform.rotation = rotation;
		NetworkServer.Spawn(obj);
	}

	public override void showHologram() {
		if (canCast ()) {
			
			if (hologram == null) {
				Ray ray = player.cam.ViewportPointToRay (new Vector3 (.5f, .5f, 0f));
				RaycastHit hit;
				if (Physics.Raycast (ray, out hit, range)) {
					Vector3 loc = hit.point + hologramOffset;
					hologram = Instantiate (hologramPrefab, loc, Quaternion.Euler (-90, player.transform.eulerAngles.y, 0));
				}
			} else {
				deleteHologram ();
			}

		}
    }

    public override void deleteHologram() {
        Destroy(hologram);
        hologram = null;
    }

    public override void updateHologram() {
		if (hologram != null) {
			Ray ray = player.cam.ViewportPointToRay (new Vector3 (.5f, .5f, 0f));
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit, range)) {
				Vector3 loc = hit.point + hologramOffset;
				hologram.transform.position = loc;
				hologram.transform.rotation = Quaternion.Euler (-90, player.transform.eulerAngles.y, 0);
			}
		}
	}

	
}
