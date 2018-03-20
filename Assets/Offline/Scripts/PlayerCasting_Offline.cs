using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCasting_Offline : MonoBehaviour {

    public CameraController cameraController;
	private GameController_Offline gc;
	public PlayerControl_Offline player;
    public Spells2_Offline[] spell;
    public KeyCode[] keys;
	public SpellsList[] spells;
    private int castingIndex;
	private bool isCasting;
    public float mana;
    public Slider slider;
	public float manaRate;
	public Animator alert;

	public enum SpellsList
	{
		fireball, iceball, wall, wind, bubble, dash, teleport
	}

	void Start() {
		//slider = GameObject.Find("Slider").GetComponent<Slider>();
		gc = FindObjectOfType<GameController_Offline>();
		slider.value = mana;

		/* saved later for robustness
		spell = new Spells2_Offline[gc.spells.Length];
		for (int i = 0; i < spell.Length; i++) {
			spell[i] = getSpell(gc.spells[i]);
		}
		*/

		spell = new Spells2_Offline[3];
		spell[0] = GetComponent<Dash_Offline>();
		spell[1] = GetComponent<Fireball_Offline>();
		spell[2] = GetComponent<Wall_Offline>();


		/* old method
		spell = new Spells2_Offline[spells.Length];
		for (int i = 0; i < spell.Length; i++) {
			spell [i] = getSpell (spells [i]);
		}
		*/
	}

	public Spells2_Offline getSpell(SpellsList spellEnum){
		switch (spellEnum) {
		case SpellsList.fireball:
			return GetComponent<Fireball_Offline> ();
			//break;

		case SpellsList.iceball:
			return GetComponent<IceBall_Offline> ();
			//break;

		case SpellsList.wall:
			return GetComponent<Wall_Offline> ();
			//break;

		case SpellsList.wind:
			return GetComponent<Wind_Offline> ();
			//break;

		case SpellsList.bubble:
			return GetComponent<Bubble_Offline> ();
			//break;

		case SpellsList.dash:
			return GetComponent<Dash_Offline> ();
			//break;

		case SpellsList.teleport:
			return GetComponent<Teleport_Offline> ();
			//break;
		}

		return null;
	}

	public void maxMana() {
		mana = 100;
		slider.value = mana;
	}

    // Update is called once per frame
    void Update() {
		if (mana <= 100) {
			mana += Time.deltaTime * manaRate;
			slider.value = mana;
		}
		
        if (!GameController.settingsOpen && !cameraController.isBallFocused) {
                for (int i = 0; i < keys.Length; i++) {
                    if (Input.GetKeyDown(keys[i])) {
                        if (spell[i].canCast()) {
                            if (spell[i].hologramPrefab != null) {
                                castingIndex = i;
                                isCasting = true;
                                spell[i].showHologram();
                            } else {
                                if (isCasting) {
                                    stopHologram();
                                }
                                spell[i].cast();
                                slider.value = mana;

                            }
                        }
                    }
                }
                if (isCasting) {
                    if (Input.GetMouseButtonDown(0)) {
                        spell[castingIndex].cast();
                        slider.value = mana;
                        stopHologram();

                    }
                     //if right click
                     else if (Input.GetMouseButtonDown(1)) {
                        stopHologram();
                    }

                     //showHologram
                     else {
                        spell[castingIndex].updateHologram();
                    }
                }
        } else if (cameraController.isBallFocused) {
			for (int i = 0; i < keys.Length; i++) {
				if (Input.GetKeyDown(keys[i])) {
					if(alert == null) {
						alert = GameObject.Find("LockAlert").GetComponent<Animator>();
					}
					alert.Play("fade");
				}
			}
		}
    }

    public void stopHologram() {
        spell[castingIndex].deleteHologram();
        castingIndex = -1;
        isCasting = false;
    }
}