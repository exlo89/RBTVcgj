using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class APIManager : MonoBehaviour {

	private bool isSignIn;
	private string userName;
	private Text userText;

	void Awake() {
		isSignIn = false;
		userName = "you sign in as ";
	}

	//void OnLevelWasLoaded(int level) {
	//	if (level == 1) {
	//		userText = GameObject.Find("Username").GetComponent<Text>();
	//		userText.text = userName;
	//		if (!isSignIn) {
	//			GameJolt.UI.Manager.Instance.ShowSignIn((bool success) => {
	//				if (success) {
	//					userName += GameJolt.API.Manager.Instance.CurrentUser.Name.ToString();
	//					userText.text = userName;
	//					isSignIn = true;
	//					Debug.Log("The user " + GameJolt.API.Manager.Instance.CurrentUser.Name.ToString() + " signed in!");
	//				} else {
	//					Debug.Log("The user failed to signed in or closed the window :(");
	//				}
	//			});
	//		}
	//	}
	//}

	public void login() {
		if (GameJolt.API.Manager.Instance.CurrentUser == null) {
			GameJolt.UI.Manager.Instance.ShowSignIn();
		}
	}

	public void logout() {
		if (GameJolt.API.Manager.Instance.CurrentUser != null) {
			GameJolt.API.Manager.Instance.CurrentUser.SignOut();
		}

	}
}
