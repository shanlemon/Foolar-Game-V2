using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameController : MonoBehaviour {

    public static bool settingsOpen;
    public GameObject settings;
	public List<GameObject> disableIfTutorialNotDone;

	public PlayerCasting.SpellsList[] spells;

	public PlayerCasting.SpellsList[] initialSpells;

	public static GameController instance;

	private void Awake() {
		DontDestroyOnLoad(this);
		if(instance != null) {
			Destroy(gameObject);
		} else {
			instance = this;
		}

		//DontDestroyOnLoad(this);	
		spells = new PlayerCasting.SpellsList[initialSpells.Length];
		for (int i = 0; i < initialSpells.Length; i++) {
			spells[i] = initialSpells[i];
		}
	}

	void Start() {
        settingsOpen = false;
		setButtonsInteractable();
	}

	public void setButtonsInteractable() {
		disableIfTutorialNotDone.Add(GameObject.Find("Singleplayer"));
		disableIfTutorialNotDone.Add(GameObject.Find("Multiplayer"));
		disableIfTutorialNotDone.Add(GameObject.Find("CustomButton"));

		foreach (GameObject obj in disableIfTutorialNotDone) {
			Button b = null;
			if (obj != null) {
				b = obj.GetComponent<Button>();
			}
			if (b != null)
				b.interactable = true;
			else
				obj.SetActive(true);
		}
	}


	public void setButtonsNotInteractable() {
		disableIfTutorialNotDone.Add(GameObject.Find("Singleplayer"));
		disableIfTutorialNotDone.Add(GameObject.Find("Multiplayer"));
		disableIfTutorialNotDone.Add(GameObject.Find("CustomButton"));	
		foreach (GameObject obj in disableIfTutorialNotDone) {
			Button b = null;
			if(obj != null) {
				b = obj.GetComponent<Button>();
			}
			if (b != null)
				b.interactable = false;
			else
				obj.SetActive(false);
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
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape)) {
			lockControl();
			if (settings == null)
				settings = GameObject.Find("SettingsMenu");
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
