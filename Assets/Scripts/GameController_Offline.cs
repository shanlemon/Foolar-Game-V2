using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController_Offline : MonoBehaviour {

	public static bool settingsOpen;
	public GameObject settings;
	private bool tutorialDone = true;
	public GameObject[] disableIfTutorialNotDone;

	public PlayerCasting_Offline.SpellsList[] spells;

	void Start() {
		settingsOpen = false;
		//setButtonsNotInteractable();

		//spells = ;

	}




	public void setButtonsInteractable() {
		foreach (GameObject obj in disableIfTutorialNotDone) {
			Button b = obj.GetComponent<Button>();
			if (b != null)
				b.interactable = true;
		}
	}

	public void setButtonsNotInteractable() {
		foreach (GameObject obj in disableIfTutorialNotDone) {
			Button b = obj.GetComponent<Button>();
			if (b != null)
				b.interactable = false;
		}
	}

	public void lockControl() {
		settingsOpen = !settingsOpen;


		if (settingsOpen) {
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
		} else {
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
		}
	}


	// Update is called once per frame
	void Update() {
		if (Input.GetKeyDown(KeyCode.Escape)) {
			lockControl();
			settings.SetActive(settingsOpen);
		}
	}

	public void goBack() {
		settingsOpen = !settingsOpen;
		settings.SetActive(settingsOpen);
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}
}
