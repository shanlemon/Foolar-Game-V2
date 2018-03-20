using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityListManager : MonoBehaviour {

	public GameObject[] thisAbilityListTemplates;
	public GameObject currentSpell;
	public GameController gc; 
	public AbilityMenuManager amm;
	public int index;

	void Start() {
		updateAbilityList();
	}

	private void OnEnable() {
		updateAbilityList();
	}

	
	public void updateAbilityList() {
		//currentSpell = amm.abilityListTemplates[(int)gc.spells[index]];
		currentSpell = amm.abilityListTemplates[(int)GameController.instance.spells[index]];

		foreach (AbilityListFiller obj in GetComponentsInChildren<AbilityListFiller>()) {
			Destroy(obj.gameObject);
		}

		foreach (GameObject obj in thisAbilityListTemplates) {
			if(obj != currentSpell)
			GameObject.Instantiate(obj, transform);
		}

	}


}
