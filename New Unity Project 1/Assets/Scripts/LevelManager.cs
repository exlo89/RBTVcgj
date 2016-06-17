using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelManager : MonoBehaviour {

    [Header("level loadtime")]
    [Tooltip("Set 0 for no load time. Time in seconds")]
    public float autoLoadNextLevelAfter;
    
    void Start() {
        if(autoLoadNextLevelAfter == 0) {
            Debug.Log("Level auto load disable");
        } else {
            Invoke("LoadNextLevel", autoLoadNextLevelAfter);
        }
    }

	public void LoadLevel(string name){
		SceneManager.LoadScene(name);
	}

	public void QuitRequest(){
		Debug.Log ("Quit requested");
		Application.Quit ();
	}

    public void LoadNextLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}
