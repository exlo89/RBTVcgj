using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class APIManager : MonoBehaviour {

	public GameObject inputName;
	private Text userText;
	private string guestName = "guest";

	void Update() {
		if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)) {
			inputName.SetActive(true);
			Debug.Log("Test");



			//userText = GameObject.Find("Username").GetComponent<Text>();
			//userText.text = "You sign in as " + guestName + " (guest) . Press enter for change the name.";
		}
	}


	void OnLevelWasLoaded(int level) {
		if (level == 1) {
			userText = GameObject.Find("Username").GetComponent<Text>();
			inputName.SetActive(false);
			//inputName = GameObject.Find("GuestName");
			if (GameJolt.API.Manager.Instance.CurrentUser != null) {
				userText.text = "you sign in as " + GameJolt.API.Manager.Instance.CurrentUser.Name.ToString();
			} else {
				userText.text = "You sign in as guest. Press enter for change the name.";
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
			userText.text = "You sign in as guest. Press enter for change the name.";
			Debug.Log("logout");
		}
	}
}
