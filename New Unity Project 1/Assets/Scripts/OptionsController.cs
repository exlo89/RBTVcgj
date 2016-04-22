using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OptionsController : MonoBehaviour {

    public Slider volumeSlider, difficultySlider;
    public Text volumeText, difficultyText;
    public LevelManager levelManager;

    private MusicManager musicManager;

	// Use this for initialization
	void Start () {
        musicManager = FindObjectOfType<MusicManager>();
        volumeSlider.value = PlayerPrefsManager.GetMasterVolume();
        difficultySlider.value = PlayerPrefsManager.GetDifficulty();
        volumeText.text = PlayerPrefsManager.GetMasterVolume().ToString() + "%";
        if(difficultySlider.value == 1) {
            difficultyText.text = "easy";
        } else if(difficultySlider.value == 2) {
            difficultyText.text = "normal";
        }else if(difficultySlider.value == 3) {
            difficultyText.text = "hard";
        }

	}
	
	// Update is called once per frame
	void Update () {
        float volumeValue;
        musicManager.SetVolume(volumeSlider.value);
        PlayerPrefsManager.SetMasterVolume(volumeSlider.value);
        volumeValue = Mathf.Round(PlayerPrefsManager.GetMasterVolume() * 100);
        volumeText.text = volumeValue + "%";
        if(difficultySlider.value == 1) {
            difficultyText.text = "easy";
        } else if(difficultySlider.value == 2) {
            difficultyText.text = "normal";
        } else if(difficultySlider.value == 3) {
            difficultyText.text = "hard";
        }
    }

    public void SaveAndExit() {
        PlayerPrefsManager.SetMasterVolume(volumeSlider.value);
        PlayerPrefsManager.SetDifficulty(difficultySlider.value);
        levelManager.LoadLevel("01a Start");
    }

    public void SetDefaults() {
        volumeSlider.value = 0.8f;
        difficultySlider.value = 2f;
    }
}
