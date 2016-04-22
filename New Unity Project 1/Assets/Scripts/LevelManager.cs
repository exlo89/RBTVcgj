﻿using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {
    [Header("Level Ladezeit")]
    [Tooltip("Bei 0 kein automatisches laden. Zeit in Sekunden")]
    public float autoLoadNextLevelAfter;
    
    void Start() {
        if(autoLoadNextLevelAfter == 0) {
            Debug.Log("Level auto load disable");
        } else {
            Invoke("LoadNextLevel", autoLoadNextLevelAfter);
        }
    }
	public void LoadLevel(string name){
		Debug.Log ("New Level load: " + name);
		Application.LoadLevel (name);
	}

	public void QuitRequest(){
		Debug.Log ("Quit requested");
		Application.Quit ();
	}

    public void LoadNextLevel() {
        Application.LoadLevel(Application.loadedLevel + 1);
    }
}
