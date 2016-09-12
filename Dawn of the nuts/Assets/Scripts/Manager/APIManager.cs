using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class APIManager : MonoBehaviour {

	private Text userText;

	void OnLevelWasLoaded(int level) {
		if (level == 1) {
			userText = GameObject.Find("Username").GetComponent<Text>();
			if (GameJolt.API.Manager.Instance.CurrentUser != null) {
				userText.text = "you sign in as " + GameJolt.API.Manager.Instance.CurrentUser.Name.ToString();
			} else {
				userText.text = "You sign in as guest.";
			}
		}
	}

	public void login() {
		if (GameJolt.API.Manager.Instance.CurrentUser == null) {
			GameJolt.UI.Manager.Instance.ShowSignIn((bool success) => {
				if(success) {
					userText = GameObject.Find("Username").GetComponent<Text>();
					userText.text = "you sign in as " + GameJolt.API.Manager.Instance.CurrentUser.Name.ToString(); ;
					Debug.Log("The user " + GameJolt.API.Manager.Instance.CurrentUser.Name.ToString() + " signed in!");
				} 
			});
		}
	}

	public void logout() {
		if (GameJolt.API.Manager.Instance.CurrentUser != null) {
			GameJolt.API.Manager.Instance.CurrentUser.SignOut();
			userText = GameObject.Find("Username").GetComponent<Text>();
			userText.text = "you sign in as guest";
			Debug.Log("logout");
		}

	}
}
