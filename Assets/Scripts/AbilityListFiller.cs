using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class AbilityListFiller : MonoBehaviour {

	public Sprite AbilityImage;
	public string AbilityName;
	public string manaCost;
	public string cooldown;
	public int index;

	public void setMovementSpell() {
		//FindObjectOfType<GameController>().spells[0] = (PlayerCasting.SpellsList)index;
		GameController.instance.spells[0] = (PlayerCasting.SpellsList)index; 
	}

	public void setAttackSpell() {
		//FindObjectOfType<GameController>().spells[1] = (PlayerCasting.SpellsList)index;
		GameController.instance.spells[1] = (PlayerCasting.SpellsList)index;


	}

	public void setUltimateSpell() {
		//FindObjectOfType<GameController>().spells[2] = (PlayerCasting.SpellsList)index;
		GameController.instance.spells[2] = (PlayerCasting.SpellsList)index;

	}

	void Start () {
		transform.GetChild(1).GetComponent<Image>().sprite = AbilityImage;
		transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = AbilityName;
		transform.GetChild(3).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = manaCost;
		transform.GetChild(3).transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = cooldown;

	}


}
