using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;

public class HighscoreManager : MonoBehaviour {

	private int currentScore;
    private int highscore;
    private GameObject inputName;
    private Text highscoreField;


    private void Start() {
        //PlayerPrefsManager.SetHighscore(0);
        //PlayerPrefsManager.SetName("");
        highscore = PlayerPrefsManager.GetHighscore();
    }

    private void OnLevelWasLoaded(int index) {
        if(index == 4) {
            highscoreField = GameObject.Find("Points Textfield").GetComponent<Text>();
            inputName = GameObject.Find("Enter Name Field");
            if (currentScore > highscore) {
                highscore = currentScore;
                highscoreField.text = highscore.ToString();
                inputName.GetComponent<InputField>().onEndEdit.AddListener(delegate { setHighscore(); });
                inputName.GetComponent<InputField>().onValueChanged.AddListener(delegate { changeHighscorefield(); });
            }
            else {
                inputName.SetActive(false);
                highscoreField.text = highscore + "  " + PlayerPrefsManager.GetName();
            }
        }
    }

    public void setScore(int score) {
        currentScore = score;
    }

    private void changeHighscorefield() {
        highscoreField.text = highscore + "  " + inputName.GetComponent<InputField>().text;
    }

    public void setHighscore() {
        PlayerPrefsManager.SetName(inputName.GetComponent<InputField>().text);
        PlayerPrefsManager.SetHighscore(highscore);
        inputName.SetActive(false);
    }
}
