using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GuestNameInput : MonoBehaviour {

	Text userText;
	public static string guestName = "guest";

	public void guestNameEnter() {
		guestName = GameObject.Find("Text").GetComponent<Text>().text;
		userText = GameObject.Find("Username").GetComponent<Text>();
		userText.text = "You sign in as " + guestName + " (guest)";
		this.gameObject.SetActive(false);
		Debug.Log(guestName);
	}
}
