using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingsMenu : MonoBehaviour {

    public float mouseSensitivity = 5;
    public AudioMixer audioMixer;
    Resolution[] resolutions;
    public TMP_Dropdown resolutionDropdown;

    void Start() {

        //setup resolution dropdown
        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for(int i = 0; i < resolutions.Length; i++) {

            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if(resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height) {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

	public void setVolume(float volume) {
        audioMixer.SetFloat("Volume", volume);
    }

    public void setGraphics(int index) {
        QualitySettings.SetQualityLevel(index);
    }

    public void setSensitivity(float sens) {
        mouseSensitivity = sens;
        CameraController.mouseSensitivity = mouseSensitivity;
    }

    public void setFullscreen(bool isFullscreen) {
        Screen.fullScreen = isFullscreen;
    }

    public void setResolution(int index) {
        Resolution res = resolutions[index];
        Screen.SetResolution(res.width, res.height, Screen.fullScreen);
    }

    public void backToMainMenu() {
        SceneManager.LoadScene(0);
    }

}
