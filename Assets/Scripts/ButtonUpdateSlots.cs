using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonUpdateSlots : MonoBehaviour {

	//private GameController gc;
	private AbilityMenuManager amm;
	private AbilityListManager alm;

	// Use this for initialization
	void Start () {
		//gc = FindObjectOfType<GameController>();
		amm = FindObjectOfType<AbilityMenuManager>();
		alm = FindObjectOfType<AbilityListManager>();

	}

	void updateSpells() {
		amm.updateSlots();
		alm.updateAbilityList();
	}

	public void changeMovementSpell(int index) {
		//gc.spells[0] = (PlayerCasting.SpellsList)index;
		GameController.instance.spells[0] = (PlayerCasting.SpellsList)index;

		updateSpells();
	}
	public void changeAttackSpell(int index) {
		//gc.spells[1] = (PlayerCasting.SpellsList)index;
		GameController.instance.spells[1] = (PlayerCasting.SpellsList)index;

		updateSpells();
	}
	public void changeUltimateSpell(int index) {
		//gc.spells[2] = (PlayerCasting.SpellsList)index;
		GameController.instance.spells[2] = (PlayerCasting.SpellsList)index;

		updateSpells();
	}

}
