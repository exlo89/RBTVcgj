using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class APIManager : MonoBehaviour {

	

	void OnLevelWasLoaded(int level) {
		Text userText = GameObject.Find("Username").GetComponent<Text>();
		if (level == 1) GameJolt.UI.Manager.Instance.ShowSignIn();

		GameJolt.UI.Manager.Instance.ShowSignIn((bool success) => {
			if (success) {
				userText.text = "You are sign in as " + GameJolt.API.Manager.Instance.CurrentUser.Name.ToString();
				Debug.Log("The user " + GameJolt.API.Manager.Instance.CurrentUser.Name.ToString() + " signed in!");
			} else {
				Debug.Log("The user failed to signed in or closed the window :(");
			}
		});
	}
}
