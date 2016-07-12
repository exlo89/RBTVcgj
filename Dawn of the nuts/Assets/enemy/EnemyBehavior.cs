﻿using UnityEngine;
using System.Collections;

public class EnemyBehavior : MonoBehaviour {

    public float speed;
	public float switchTime;
    public float hitTime;
	public int kindOfEnemy;
	public Sprite hitPicture;
	public Sprite deadPicture;
	public Sprite normalPicture;
	public AudioClip hitPlayer;
	public AudioClip hitEnemy;
	public AudioClip normal;
	public AudioClip dead;

	private int health;
	PlayerBehavior playerBehavior;
	GameObject target;
    private float Range;
	private float damage;
	private AudioSource audioSource;
	private SpriteRenderer spriteRenderer;
	private PolygonCollider2D polyCollider;
	private bool isDead = false;
	

	private void Start () {
        audioSource = GetComponent<AudioSource>();
		spriteRenderer = GetComponent<SpriteRenderer>();
		polyCollider = GetComponent<PolygonCollider2D>();
		audioSource.volume = PlayerPrefsManager.GetMasterVolume();
		playSounds(normal);
		playerBehavior = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehavior>();
		target = GameObject.FindGameObjectWithTag("Player");

		switch (kindOfEnemy) {
			case 1: //schnellen gegner 2 health
				health = 2;
				damage = 5;
				break;
			case 2: //zombie gegner 3 health
				health = 3;
				damage = 10;
				break;
			case 3: // boss 5 health
				health = 5;
				damage = 30;
				hitTime = 2;
				break;
		}
	}

	// Update is called once per frame
	private void Update () {
		if (!isDead && !(PlayerBehavior.getNoMove())) {
			movement();
		}
    }

	private void OnTriggerEnter2D(Collider2D col) {
		if (kindOfEnemy == 3) {
			switch (col.gameObject.layer) {
				case 8://spieler
					Weapon.ableToFire = false;
					enemyHitPlayer();
					break;
				case 11://projektil
					Destroy(col.gameObject);
					decreaseHealth(1);
					healthPointsEnemy();
					break;
			}
		}
	}

	private void OnTriggerExit2D(Collider2D col) {
		Weapon.ableToFire = true;
	}

	private void OnLevelWasLoaded(int index) {
		if(index == 2) {
			Weapon.ableToFire = true;
		}
	}

	public void healthPointsEnemy() {
		if(health <= 0) {
			isDead = true;
			spriteRenderer.sprite = deadPicture;
			polyCollider.enabled = false;
			if (kindOfEnemy !=3) {
				GetComponentInChildren<EnemyTrigger>().enableCollider(false);
			}
			if(kindOfEnemy == 3) {
				audioSource.pitch = 1f;
			}
			playSounds(dead);
			Invoke("enemyDisabled", 1f);
		} else {
			playSounds(hitEnemy);
			spriteRenderer.sprite = hitPicture;
			Invoke("enemyNormalPicture",switchTime);
		}
	}

	private void enemyNormalPicture() {
		if (!isDead) {
			spriteRenderer.sprite = normalPicture;
			playSounds(normal);
		}
	}

    public void decreaseHealth(int x) {
		health -= x;
    }

	//=========================================================fertig===========================================================

	/// <summary>
	/// Bewegung des Gegners. Er bewegt sich auf den Spieler zu.
	/// </summary>

	private void movement() {
		Debug.DrawLine(transform.position, target.transform.position);
		Range = Vector2.Distance(transform.position, target.transform.position);
		if (Range >= 0.5f) {
			transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
		}
	}

	/// <summary>
	/// Spielt den Soundeffekt ab.
	/// </summary>
	/// <param name="x">Soundclip</param>

	private void playSounds(AudioClip x) {
		audioSource.clip = x;
		audioSource.Play();
	}

	/// <summary>
	/// Der Gegner trifft den Spieler.
	/// </summary>

	public void enemyHitPlayer() {
		playSounds(hitPlayer);
		playerBehavior.decreaseHealth(damage);
	}

	/// <summary>
	/// Gegner wird ausgeblendet und die Punkte werden dem Score gugeschrieben.
	/// </summary>

	private void enemyDisabled() {
		spriteRenderer.enabled = false;
		switch (kindOfEnemy) {
			case 1: playerBehavior.addScore(100); break;
			case 2: playerBehavior.addScore(150); break;
			case 3: playerBehavior.addScore(500); break;
		}
		Invoke("enemyDestroy", 3f);
	}

	/// <summary>
	/// Gegner wird zerstörrt und die Punkte werden den Score zugeordnet.
	/// </summary>

	private void enemyDestroy() {
		Destroy(this.gameObject);
	}
}
