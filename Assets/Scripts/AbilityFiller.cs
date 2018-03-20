using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class AbilityFiller : MonoBehaviour {

	public Sprite AbilityImage;
	public string AbilityName;
	[TextArea(3, 10)]
	public string AbilityDescription;
	public string manaCost;
	public string cooldown;

	// Use this for initialization
	void Start () {
		try {
			transform.GetChild(0).GetComponent<Image>().sprite = AbilityImage;
			transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = AbilityName;
			transform.GetChild(2).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = manaCost;
			transform.GetChild(2).transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = cooldown;
			transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = AbilityDescription;
		}catch(System.Exception e) {
			Debug.Log(e.Message);
		}
	}

}
