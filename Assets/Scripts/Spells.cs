using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spells : MonoBehaviour {

	public Transform player;
	public int cooldown, charges;
	public int currentCD = 0, currentCharges = 1;
    public GameObject hologramPrefab;
    public float range = 10f;
    public Vector3 offset;
    public bool quickCast;

    public enum Types {
		projectile,
		aoe,
		movement,
        placeable
	}
	public Types type;
	public GameObject effect;

	void FixedUpdate () {
		if (currentCD > 0) {
			currentCD--;

			if (currentCD <= 0) {
				if (charges > 1 && currentCharges < charges) {
					currentCharges++;
					currentCD = cooldown;
				}
            }
		}

	}

    GameObject holoGram;
    public bool showingHologram;

    public void showHologram() {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(.5f, .5f, 0f));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, range)) {
            Vector3 loc = hit.point + offset;
            holoGram = Instantiate(hologramPrefab, loc, Quaternion.Euler(-90, player.eulerAngles.y, -90));
        }
        showingHologram = true;
    }

    public void updateHologram() {
        if(holoGram != null) {
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(.5f, .5f, 0f));
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, range)) {
                Vector3 loc = hit.point + offset;
                holoGram.transform.position = loc;
                holoGram.transform.rotation = Quaternion.Euler(-90, player.eulerAngles.y, -90);
            }
        }
        
    }

    public void deleteHologram() {
        showingHologram = false;
        Destroy(holoGram);
        holoGram = null;
    }

    public void cast() {
        if (currentCD <= 0 || charges > 1) {
            if (charges == 1) {
                castType();
                currentCD = cooldown;
            } else {
                if (currentCharges > 0) {
                    castType();
                    if (currentCharges == charges)
                        currentCD = cooldown;
                    currentCharges--;
                }
            }
        }

    }

    public bool canCast() {
        if (currentCD <= 0 || charges > 1) {
            if (charges == 1) {
                return true;
            } else {
                if (currentCharges > 0) {
                    return true;
                }
            }
         }
        return false;
    }

    void castType() {
        switch (type) {
            case Types.projectile:
                fireProjectile();
                break;
            case Types.placeable:
                placeProjectile();
                break;
        }
    }

	void fireProjectile () {
		Quaternion rotation = Camera.main.transform.rotation;
		Instantiate (effect, player.Find("Shoot Target").position,rotation);
	}

    void placeProjectile() {
		Ray ray = Camera.main.ViewportPointToRay (new Vector3 (.5f, .5f, 0f));
		RaycastHit hit;
		if (Physics.Raycast (ray, out hit, range)) {
			Vector3 loc = hit.point + offset;
			Instantiate (effect, loc, Quaternion.Euler(-90, player.eulerAngles.y, -90));
		}
	}

}
