using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BorderManager : MonoBehaviour {

	public Text warning;
	public static float countdown = 10f;
	private bool startCount = false;

	private void Update() {
		if (startCount) {
			countdown -= Time.deltaTime;
		}
	}

	public void OnTriggerStay2D(Collider2D col) {
		Debug.Log("kollidiert mit " + col);
		if (col.gameObject.layer == 8) {
			Debug.Log("das isz der spieler");
			warning.text = "You leave the field. \n Turn back or you die in " + countdown + " seconds.";
			
		}
	}

}
