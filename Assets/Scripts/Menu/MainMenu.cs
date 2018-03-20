using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour {

    public GameObject settings;

    public void playGame() {
        SceneManager.LoadScene(2);
    }
	public void goBackAScene() {
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1 < 0 ? SceneManager.GetActiveScene().buildIndex : SceneManager.GetActiveScene().buildIndex - 1);
	}

	public void exitGame() {
        Application.Quit();
    }

	public void goToScene(int index) {
		SceneManager.LoadScene(index);

	}

}
