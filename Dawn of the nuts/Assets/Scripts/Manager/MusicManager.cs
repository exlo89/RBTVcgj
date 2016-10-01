using UnityEngine;
using System.Collections;

[HelpURL("https://exlo-gamedev.tumblr.com/")]
public class MusicManager : MonoBehaviour {

	
	[Header("song container for the game")]
    [Tooltip("Set the quantity of the ingame songs. After this put the songs in the elements. " +
            "Every element represent a scene. In scene one the first element would be load. " +
            "In the second scene the second song and so on.")]
    public AudioClip[] levelMusicChangeArray;

    private AudioSource audioSource;
    private AudioClip currentClip;

    void Start() {
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.volume = PlayerPrefsManager.GetMasterVolume();
		audioSource.clip = levelMusicChangeArray[1];
		audioSource.loop = true;
		audioSource.Play();
    }

    void OnLevelWasLoaded(int level) {
        AudioClip thisLevelMusic = levelMusicChangeArray[level];
        //Debug.Log("Playing clip: " + thisLevelMusic + " on  level " + level);
        if (thisLevelMusic != null && thisLevelMusic != currentClip) {

            audioSource.clip = thisLevelMusic;
            audioSource.loop = true;
            audioSource.Play();
            currentClip = thisLevelMusic;
        }
    }

    public void SetVolume(float volume) {
        audioSource.volume = volume;
    }
}
