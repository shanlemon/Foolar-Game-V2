using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HUDManager_Online : MonoBehaviour {

	public Image movementImage;
	public Image attackImage;
	public Image ultimateImage;

	public Slider movementSlider;
	public Slider attackSlider;
	public Slider ultimateSlider;

	public PlayerCasting pc;
	public PlayerCasting_Offline pcO;

	public Sprite[] abilityIcons;


	// Use this for initialization
	void Start () {
		pc = GameObject.Find("Local").GetComponent<PlayerCasting>();
		updateIcons();
	}
	
	public void updateIcons() {
		movementImage.sprite = abilityIcons[(int)GameController.instance.spells[0]];
		attackImage.sprite = abilityIcons[(int)GameController.instance.spells[1]];
		ultimateImage.sprite = abilityIcons[(int)GameController.instance.spells[2]];

	}

	// Update is called once per frame
	void Update () {
		//float value = (pc.spell[0].currentCooldown * 5) / (pc.spell[0].cooldown * 5);
		//Debug.Log(pc.spell[0].currentCooldown + " / " + pc.spell[0].cooldown + " = " + value);
		//movementSlider.value = value;
		if (pc != null) {
			Color c = movementImage.color;
			c.a = (pc.spell[0].currentCooldown / pc.spell[0].cooldown);

			c.a = (pc.spell[0].currentCooldown == 0) ? 1 : c.a;
			movementSlider.value = c.a;

			c.a = (pc.spell[0].canCast()) ? 1 : 0.25f;

			movementImage.color = c;

			Color a = attackImage.color;
			a.a = (pc.spell[1].currentCooldown / pc.spell[1].cooldown);

			a.a = (pc.spell[1].currentCooldown == 0) ? 1 : a.a;
			attackSlider.value = a.a;

			a.a = (pc.spell[1].canCast()) ? 1 : 0.25f;
			attackImage.color = a;

			Color w = ultimateImage.color;
			w.a = (pc.spell[2].currentCooldown / pc.spell[2].cooldown);

			w.a = (pc.spell[2].currentCooldown == 0) ? 1 : w.a;
			ultimateSlider.value = w.a;

			w.a = (pc.spell[2].canCast()) ? 1 : 0.25f;

			ultimateImage.color = w;
		}else {
			Color c = movementImage.color;
			c.a = (pcO.spell[0].currentCooldown / pcO.spell[0].cooldown);

			c.a = (pcO.spell[0].currentCooldown == 0) ? 1 : c.a;
			movementSlider.value = c.a;

			c.a = (pcO.spell[0].canCast()) ? 1 : 0.25f;

			movementImage.color = c;

			Color a = attackImage.color;
			a.a = (pcO.spell[1].currentCooldown / pcO.spell[1].cooldown);

			a.a = (pcO.spell[1].currentCooldown == 0) ? 1 : a.a;
			attackSlider.value = a.a;

			a.a = (pcO.spell[1].canCast()) ? 1 : 0.25f;
			attackImage.color = a;

			Color w = ultimateImage.color;
			w.a = (pcO.spell[2].currentCooldown / pcO.spell[2].cooldown);

			w.a = (pcO.spell[2].currentCooldown == 0) ? 1 : w.a;
			ultimateSlider.value = w.a;

			w.a = (pcO.spell[2].canCast()) ? 1 : 0.25f;

			ultimateImage.color = w;
		}
	}


}
