using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HighscoreManager : MonoBehaviour {

	private int currentScore;
    private int highscore;
    private string savedName;
	private string highscoreName;
    private GameObject inputName;
    private GameObject highscoreField;

    private void Start() {
        PlayerPrefsManager.SetHighscore(0);
        highscore = PlayerPrefsManager.GetHighscore();
        savedName = PlayerPrefsManager.GetName();
        Debug.Log(highscore + "      -" + savedName + "-");
    }

    private void OnLevelWasLoaded(int index) {
        if(index == 4) {
            highscoreField = GameObject.Find("Points Textfield");
            inputName = GameObject.Find("Enter Name Field");
            //Debug.Log("level " + index + " wurde geladen und Enter name field wurde auch geladen " + inputName);
            if (currentScore > highscore) {
                highscore = currentScore;
            } else {
                Debug.Log(inputName);
                inputName.SetActive(false);
                highscoreField.GetComponent<Text>().text = highscore + "  " + savedName;
            }
            
        }
    }

    public void setScore(int score) {
        currentScore = score;
    }

    public void setHighscore(string ) {
        Debug.Log("feld fertig bearbeitet");
        inputName = GameObject.Find("Enter Name Field");
        highscoreName = inputName.GetComponent<Text>().text;
        inputName.SetActive(false);
        highscoreField.GetComponent<Text>().text = highscore + "  " + highscoreName;

    }

    private void OnApplicationQuit() {
        PlayerPrefsManager.SetName(highscoreName);
        PlayerPrefsManager.SetHighscore(highscore);
    }
}
