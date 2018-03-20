using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spells2_Offline : MonoBehaviour {

    //public List<Rigidbody> rigidbodies = new List<Rigidbody>();

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

	public PlayerControl_Offline player;
	public PlayerCasting_Offline playerCasting;
	public float manaCost;

	void Start() {
		anim = player.anim;
	}

	public float cooldown, charges, currentCooldown, currentCharges;
    private void FixedUpdate() {
		if (currentCharges < charges) {
			currentCooldown += Time.deltaTime;
			if (currentCooldown >= cooldown) {
				currentCharges++;
				currentCooldown = 0;
			}
		}
    }
	
    public bool canCast() {
		return currentCharges > 0 && (playerCasting.mana - (manaCost * 10)) >= 0;
    }

	public abstract void cast();
	
	public abstract void showHologram();
    public abstract void deleteHologram();
    public abstract void updateHologram();

}