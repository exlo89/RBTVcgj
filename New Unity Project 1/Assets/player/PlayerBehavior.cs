﻿using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class PlayerBehavior : MonoBehaviour {
	public float speed;
    public float health;
	public Text wave;
    public Text life;
	public Text highscore;
	public Image headImage;
	public Sprite[] headSprites;
	private static bool noMove;
	private bool stop = false;
	private double score;
	private bool timeoutField = false;
	GameObject textWave;

	void Start () {
		textWave = GameObject.Find("Spawner");
		health = 100;
		life.text = health.ToString();
		score = 0;
		highscore.text = score.ToString();
    }

	// Update is called once per frame
	void Update() {

		highscore.text = score.ToString();
		healthAdvice();
 
		if (!noMove) {
			movement();
			wave.GetComponent<Text>().enabled = false;
		} else {
			this.transform.position = new Vector3(0, 0, 0);
			wave.GetComponent<Text>().enabled = true;
			wave.text = "wave " + textWave.GetComponent<EnemySpawner>().getWaveCounter().ToString();
		}

        if (isPlayerDead()) {
			life.text = "0";
			wave.enabled = true;
			highscore.enabled = false;
			noMove = true;
			wave.text = "you have " + score + " points and survived " + (textWave.GetComponent<EnemySpawner>().getWaveCounter()-2) + " waves";
			Invoke("back", 5f);
        }
	}

	public void setTimeoutField(bool x) {
		timeoutField = x;
	}

	//=============================================fertig============================================================

	/// <summary>
	/// verringert die Lebenspunkte
	/// </summary>
	/// <param name="value">um wie viel sollen die Lebenspunkte verringert werden</param>

	public void decreaseHealth(float value) {
		if (!stop) {
			health -= value;
			life.text = health.ToString();
		}
	}

	/// <summary>
	/// Setter für den Highscore
	/// </summary>
	/// <param name="x">Parameter für die Punktezahl, die übergeben werden soll</param>

	public void addScore(double x) {
		score = score + x;
	}

	/// <summary>
	/// Springt zurück zum Hauptmenü und zerstört den aktuellen Musikplayer
	/// </summary>

	private void back() {
		Destroy(GameObject.Find("Persistent Music"));
		SceneManager.LoadScene("01a Start");
	}

	/// <summary>
	/// Setter für die NoMove Variable.
	/// </summary>
	/// <param name="x">true oder false für das Setzen</param>

	public static void setNoMove(bool x) {
		noMove = x;
	}

	/// <summary>
	/// Getter für die NoMove Variable
	/// </summary>
	/// <returns>gibt den aktuellen Zustand zurück (true/false)</returns>

	public static bool getNoMove() {
		return noMove;
	}

	/// <summary>
	/// Prüft ob der Spieler noch lebt
	/// </summary>
	/// <returns>gibt false zurück, wenn der Spieler noch lebt</returns>

	private bool isPlayerDead() {
		if (health <= 0 || timeoutField) {
			return true;
		}
		return false;
	}

	/// <summary>
	/// Bewegung des Spielers. Nur die W,A,S,D und TAB Taste.
	/// </summary>

	private void movement() {

		//==============zurück zum menü=================================

		if (Input.GetKeyDown(KeyCode.Escape)) {
			SceneManager.LoadScene("01a Start");
		}

		//==============links und recht==================================

		if (Input.GetKey(KeyCode.A)) {
			transform.position += Vector3.left * speed * Time.deltaTime;
		} else if (Input.GetKey(KeyCode.D)) {
			transform.position += Vector3.right * speed * Time.deltaTime;
		}

		//==============oben und unten===================================

		if (Input.GetKey(KeyCode.W)) {
			transform.position += Vector3.up * speed * Time.deltaTime;
		} else if (Input.GetKey(KeyCode.S)) {
			transform.position += Vector3.down * speed * Time.deltaTime;
		}
	}

	/// <summary>
	/// Methode die für die Lebensanzeige, in Form von Bildern, zuständig ist.
	/// </summary>

	private void healthAdvice() {
		if (health <= 100 && health >= 80) {
			headImage.sprite = headSprites[0];
		} else if (health < 80 && health >= 60) {
			headImage.sprite = headSprites[1];
		} else if (health < 60 && health >= 40) {
			headImage.sprite = headSprites[2];
		} else if (health < 40 && health >= 20) {
			headImage.sprite = headSprites[3];
		} else if (health < 20 && health > 0) {
			headImage.sprite = headSprites[4];
		} else if (health < 0) {
			stop = true;
			headImage.sprite = headSprites[5];
			life.text = "0";
		}
	}
}

