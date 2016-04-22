using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour {

    public AudioClip[] levelMusicChangeArray;
    private AudioSource audioSource;

    // Use this for initialization
    void Awake() {
		if(GameObject.FindGameObjectsWithTag("Musicplayer").Length > 1) {
			Destroy(gameObject);
		} else {
			DontDestroyOnLoad(gameObject);
			//Debug.Log("Don't destroy on load: " + name);
		}
	}

    void Start() {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = PlayerPrefsManager.GetMasterVolume();
		audioSource.clip = levelMusicChangeArray[0];
		audioSource.loop = true;
		audioSource.Play();
    }

    void OnLevelWasLoaded(int level) {
        AudioClip thisLevelMusic = levelMusicChangeArray[level];
        //Debug.Log("Playing clip: " + thisLevelMusic + " on  level " + level);
        if(level > 1) {
            audioSource.clip = thisLevelMusic;
            audioSource.loop = true;
            audioSource.Play();
        }
    }

    public void SetVolume(float volume) {
        audioSource.volume = volume;
    }
}
