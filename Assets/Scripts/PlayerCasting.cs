using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PlayerCasting : NetworkBehaviour {

    public CameraController cameraController;
	public PlayerController player;
    public Spells2[] spell;
    public KeyCode[] keys;
	public SpellsList[] spells;
    private int castingIndex;
	private bool isCasting;
    public float mana;
    public Slider slider;
	public float manaRate;
	public Animator lockAlert;
	public Animator manaAlert;



	public enum SpellsList
	{
		fireball, iceball, wall, wind, bubble, dash, teleport
	}

	public override void OnStartLocalPlayer() {
		base.OnStartLocalPlayer();
		gameObject.name = "Local";
	}

	void Start() {
        slider = GameObject.Find("Slider").GetComponent<Slider>();
        slider.value = mana;

		StartCoroutine(initializeSpells());
		
    }
	IEnumerator initializeSpells() {
		yield return new WaitForSeconds(1);

		//SpellsList[] list = FindObjectOfType<GameController>().spells;
		SpellsList[] list = GameController.instance.spells;

		if (list.Length != 3) {
			Debug.Log("Taking spells from inspector, not GameController");
			instantiateSpells();
		} else {
			instantiateSpells(list);
		}
	}

	public void instantiateSpells() {
		Debug.Log("no parameter");
		spell = new Spells2[spells.Length];
		for (int i = 0; i < spell.Length; i++) {
			spell[i] = getSpell(spells[i]);
		}
	}

	public void instantiateSpells(SpellsList[] list) {

		spell = new Spells2[spells.Length];
		for (int i = 0; i < spell.Length; i++) {
			spell[i] = getSpell(list[i]);
		}
	}

	public Spells2 getSpell(SpellsList spellEnum){
		switch (spellEnum) {
		case SpellsList.fireball:
			return GetComponent<Fireball> ();
			//break;

		case SpellsList.iceball:
			return GetComponent<IceBall> ();
			//break;

		case SpellsList.wall:
			return GetComponent<Wall> ();
			//break;

		case SpellsList.wind:
			return GetComponent<Wind> ();
			//break;

		case SpellsList.bubble:
			return GetComponent<Bubble> ();
			//break;

		case SpellsList.dash:
			return GetComponent<Dash> ();
			//break;

		case SpellsList.teleport:
			return GetComponent<Teleport> ();
			//break;
		}

		return null;
	}

    // Update is called once per frame
    void Update() {
        if (!isLocalPlayer)
            return;
		if (mana <= 100) {
			mana += Time.deltaTime * manaRate;
			slider.value = mana;
		}
		
        if (!GameController.settingsOpen && !cameraController.isBallFocused) {
            if (!cameraController.isBallFocused) {
                for (int i = 0; i < keys.Length; i++) {
                    if (Input.GetKeyDown(keys[i])) {
						Debug.Log(i);
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
						} else {
							if (manaAlert == null) {
								manaAlert = GameObject.Find("ManaAlert").GetComponent<Animator>();
							}
							Debug.Log("play fade");
							manaAlert.Play("fade");
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
            }
        } else if (cameraController.isBallFocused) {
			for (int i = 0; i < keys.Length; i++) {
				if (Input.GetKeyDown(keys[i])) {
					if (lockAlert == null) {
						lockAlert = GameObject.Find("LockAlert").GetComponent<Animator>();
					}
					Debug.Log("play fade");
					lockAlert.Play("fade");
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