using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HighscoreManager : MonoBehaviour {
    
	public Text inputField;

	private int score;
	private string saveString;

	private void checkHighscore() {
		if (score > PlayerPrefsManager.GetHighscore()) {

		}
	}

    public void setScore(int x) {
        score = x;
    }

	public void saveHighscore(int highscore) {
		PlayerPrefsManager.SetHighscore(highscore);
	}
	

}
