using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OptionsController : MonoBehaviour {

    public Slider volumeSlider;
    public Text volumeText;
    public GameObject gameManager;

    private MusicManager musicManager;

	// Use this for initialization
	void Start () {
        musicManager = gameManager.GetComponent<MusicManager>();
        volumeSlider.value = PlayerPrefsManager.GetMasterVolume();
        volumeText.text = PlayerPrefsManager.GetMasterVolume().ToString() + "%";
	}
	
	// Update is called once per frame
	void Update () {
        float volumeValue;
        musicManager.SetVolume(volumeSlider.value);
        PlayerPrefsManager.SetMasterVolume(volumeSlider.value);
        volumeValue = Mathf.Round(PlayerPrefsManager.GetMasterVolume() * 100);
        volumeText.text = volumeValue + "%";
    }

    public void SaveAndExit() {
        PlayerPrefsManager.SetMasterVolume(volumeSlider.value);
        gameManager.GetComponent<LevelManager>().LoadLevel("01a Start");
    }

    public void SetDefaults() {
        volumeSlider.value = 0.8f;
    }
}
