using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public abstract class Spells2 : NetworkBehaviour {

    public List<Rigidbody> rigidbodies = new List<Rigidbody>();

    public enum InputSent {
        rightClick,
        leftClick,
        keyboard
    }

	public Sprite icon;
    public GameObject hologramPrefab;
    public GameObject effect;
	public Animator anim;
	public float delay;

	public PlayerControl player;
	public PlayerCasting playerCasting;
	public float manaCost;

	public float cooldown, charges, currentCooldown, currentCharges;


	void Start() {
		anim = player.anim;
		currentCharges = charges;
		currentCooldown = cooldown;
	}



    private void FixedUpdate() {
		if (currentCharges < charges) {
			currentCooldown += Time.deltaTime;
			if (currentCooldown >= cooldown) {
				currentCharges++;
				currentCooldown = 0;
			}
		}
		/*
        if (currentCharges < charges) {
            currentCooldown++;
            if (currentCooldown >= cooldown) {
                currentCharges++;
                currentCooldown = 0;
            }
        }
        */
    }
	
    public bool canCast() {
		return currentCharges > 0 && (playerCasting.mana - (manaCost * 10)) >= 0;
    }

	//[Command]
	//public abstract void CmdCast(GameObject effect, Vector3 loc, Quaternion rotation);

	public abstract void cast();


	public abstract void showHologram();
    public abstract void deleteHologram();
    public abstract void updateHologram();

}