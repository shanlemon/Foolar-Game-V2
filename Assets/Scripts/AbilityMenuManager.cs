using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityMenuManager : MonoBehaviour {

	public GameController gc;
	public GameObject movementSlot;
	public GameObject attackSlot;
	public GameObject ultimateSlot;
	public GameObject[] templates;
	public GameObject[] abilityListTemplates;
	public bool button;

	public GameObject chosenMovement;
	public GameObject chosenAttack;
	public GameObject chosenUltimate;


	private void Start() {

		updateSlots();
	}

	void Update() {
		if(button) {
			updateSlots();

			button = false;
		}	
	}


	public void updateSlots() {
		removeTemplate();
		/*
		Instantiate(templates[(int) gc.spells[0]], movementSlot.transform);
		Instantiate(templates[(int) gc.spells[1]], attackSlot.transform);
		Instantiate(templates[(int) gc.spells[2]], ultimateSlot.transform);

		Instantiate(templates[(int)gc.spells[0]], chosenMovement.transform);
		Instantiate(templates[(int)gc.spells[1]], chosenAttack.transform);
		Instantiate(templates[(int)gc.spells[2]], chosenUltimate.transform);
		*/
		Instantiate(templates[(int)GameController.instance.spells[0]], movementSlot.transform);
		Instantiate(templates[(int)GameController.instance.spells[1]], attackSlot.transform);
		Instantiate(templates[(int)GameController.instance.spells[2]], ultimateSlot.transform);

		Instantiate(templates[(int)GameController.instance.spells[0]], chosenMovement.transform);
		Instantiate(templates[(int)GameController.instance.spells[1]], chosenAttack.transform);
		Instantiate(templates[(int)GameController.instance.spells[2]], chosenUltimate.transform);




	}

	void removeTemplate() {
		GameObject movement = movementSlot.GetComponentInChildren<AbilityFiller>().gameObject;
		GameObject attack = attackSlot.GetComponentInChildren<AbilityFiller>().gameObject;
		GameObject ultimate = ultimateSlot.GetComponentInChildren<AbilityFiller>().gameObject;

		GameObject chosenMovementd = chosenMovement.GetComponentInChildren<AbilityFiller>().gameObject;
		GameObject chosenAttackd = chosenAttack.GetComponentInChildren<AbilityFiller>().gameObject;
		GameObject chosenUltimated = chosenUltimate.GetComponentInChildren<AbilityFiller>().gameObject;

		if (movement != null) {
			Destroy(movement);
		}
		if (attack != null) {
			Destroy(attack);
		}
		if (ultimate != null) {
			Destroy(ultimate);
		}
		if (chosenMovementd != null) {
			Destroy(chosenMovementd);
		}
		if (chosenAttackd != null) {
			Destroy(chosenAttackd);
		}
		if (chosenUltimated != null) {
			Destroy(chosenUltimated);
		}
	}

}


