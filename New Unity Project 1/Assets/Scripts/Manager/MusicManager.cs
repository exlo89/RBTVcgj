using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour {

    [Header("Song container for the game")]
    [Tooltip("Set the quantity of the ingame songs. After this put the songs in the elements. " +
            "Every element represent a scene. In scene one the first element would be load. " +
            "In the second scene the second song and so on.")]
    public AudioClip[] levelMusicChangeArray;
    private AudioSource audioSource;

    void Start() {
        Debug.Log("Test " + gameObject);
        audioSource = gameObject.GetComponent<AudioSource>();
		Debug.Log(audioSource);
        audioSource.volume = PlayerPrefsManager.GetMasterVolume();
		audioSource.clip = levelMusicChangeArray[1];
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
