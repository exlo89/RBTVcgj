using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System.Collections;

public class APIManager : MonoBehaviour {

    private Text userText;

    void OnLevelWasLoaded(int level) {
        switch (level) {
            case 1: startScreen(); break;
            case 4: endScreen(); break;
        }

    }

    void startScreen() {
        userText = GameObject.Find("Username").GetComponent<Text>();
        if (GameJolt.API.Manager.Instance.CurrentUser != null) {
            userText.text = "you sign in as " + GameJolt.API.Manager.Instance.CurrentUser.Name.ToString();
        } else {
            userText.text = "You sign in as guest.";
        }
    }

    void endScreen() {

        string guestName = "guest";
        int scoreValue = this.GetComponent<HighscoreManager>().Score; // The actual score.
        string scoreText = scoreValue + " points"; // A string representing the score to be shown on the website.
        int tableID = 0; // Set it to 0 for main highscore table.
        string extraData = ""; // This will not be shown on the website. You can store any information.

        if(GameJolt.API.Manager.Instance.CurrentUser != null) {
            GameJolt.API.Scores.Add(scoreValue, scoreText, tableID, extraData, (bool success) => {
                Debug.Log(string.Format("Score Add {0}.", success ? "Successful" : "Failed"));
            });
        } else {
            GameJolt.API.Scores.Add(scoreValue, scoreText, guestName, tableID, extraData, (bool success) => {
                Debug.Log(string.Format("Score Add {0}.", success ? "Successful" : "Failed"));
            });
        }

    }

	public void login() {
		if (GameJolt.API.Manager.Instance.CurrentUser == null) {
			GameJolt.UI.Manager.Instance.ShowSignIn((bool success) => {
				if(success) {
					userText = GameObject.Find("Username").GetComponent<Text>();
					userText.text = "You sign in as " + GameJolt.API.Manager.Instance.CurrentUser.Name.ToString(); ;
					Debug.Log("The user " + GameJolt.API.Manager.Instance.CurrentUser.Name.ToString() + " signed in!");
				} 
			});
		}
	}

	public void logout() {
		if (GameJolt.API.Manager.Instance.CurrentUser != null) {
			GameJolt.API.Manager.Instance.CurrentUser.SignOut();
			userText = GameObject.Find("Username").GetComponent<Text>();
			userText.text = "You sign in as guest.";
			Debug.Log("logout");
		}
	}
}
