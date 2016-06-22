using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OptionsController : MonoBehaviour {

    [Tooltip("This is the slider bar for the volume.")]
    public Slider volumeSlider;
    [Tooltip("This is the textfield for the volume. It show the current volume value.")]
    public Text volumeText;

    private LevelManager levelManager;
    private MusicManager musicManager;

	void Start () {
        levelManager = GameObject.Find("Game Manager").GetComponent<LevelManager>();
        musicManager = GameObject.Find("Game Manager").GetComponent<MusicManager>();
        volumeSlider.value = PlayerPrefsManager.GetMasterVolume();
        volumeText.text = PlayerPrefsManager.GetMasterVolume().ToString() + "%";
	}

	void Update () {
        float volumeValue;
        musicManager.SetVolume(volumeSlider.value);
        PlayerPrefsManager.SetMasterVolume(volumeSlider.value);
        volumeValue = Mathf.Round(PlayerPrefsManager.GetMasterVolume() * 100);
        volumeText.text = volumeValue + "%";
    }

    public void SaveAndExit() {
        PlayerPrefsManager.SetMasterVolume(volumeSlider.value);
        levelManager.LoadLevel("01a Start");
    }

    public void SetDefaults() {
        volumeSlider.value = 0.8f;
    }
}
