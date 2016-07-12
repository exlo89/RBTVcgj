using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BorderManager : MonoBehaviour {

	public Text warning;
	public static float countdown = 10f;
	private bool startCount = false;
	private PlayerBehavior playerBehavior;

	void Start() {
		playerBehavior = GameObject.Find("player prefab").GetComponent<PlayerBehavior>(); 
	}

	void Update() {
		if (startCount) {
			countdown -= Time.deltaTime;
			warning.text = "You leave the field. \n Turn back or you die in " + Mathf.Round(countdown) + " seconds.";
			if (countdown < 0) {
				playerBehavior.setTimeoutField(true);
				warning.enabled = false;
			}
		}
	}

	void OnTriggerExit2D(Collider2D col) {
		if(col.gameObject.layer == 8) {
			warning.enabled = true;
			startCount = true;
		}
	}

	void OnTriggerEnter2D(Collider2D col) {
		if (col.gameObject.layer == 8) {
			warning.enabled = false;
			startCount = false;
			countdown = 10;
		}
	}
}
