using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour {


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
	private GameObject player;
    private float Range;
	private float damage;
	private bool isCol = false;
	private bool isDying = false;
	private AudioSource audioSource;
	private bool playerIshit = false;


	// Use this for initialization
	void Start () {
		soundsNormal(normal);
		switch (kindOfEnemy) {
			case 1: //schnellen gegner 2 health
				health = 1;
				damage = 10;
				break;
			case 2: //zombie gegner 3 health
				health = 2;
				damage = 20;
				break;
			case 3: // boss 10 health
				health = 9;
				damage = 40;
				hitTime = 3;
				break;
		}
		player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
        GameObject target = GameObject.FindGameObjectWithTag("Player");
        Debug.DrawLine(transform.position, target.transform.position);

        Range = Vector2.Distance(transform.position, target.transform.position);
        if (Range >= 0.5f && !isDying) {
               transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
        }
    }

    void OnTriggerEnter2D(Collider2D col) {
		//Debug.Log(col.gameObject.layer);
		switch (col.gameObject.layer) {
			case 8://spieler
				playerIshit = true;
				player.GetComponentInChildren<Weapon>().setFire(false); 
				enemyHitPlayer();
				InvokeRepeating("enemyHitPlayer", hitTime, hitTime);
				break;
			case 11://projektil
				Debug.Log("lebenspunkt " + health);
				Debug.Log("is col " + isCol);
				Debug.Log("is dying " + isDying);
				/*if (isCol && !isDying) {
					isCol = false;
				}*/
				Destroy(col.gameObject);
				healthPointsEnemy();
				break;
		}
    }

	void healthPointsEnemy() {
		if(health == 0 && !isCol) {
			Debug.Log("Tot");
			gameObject.GetComponent<SpriteRenderer>().sprite = deadPicture;
			//isCol = true;
			/*
			if(!isCol && isDying) {
				Invoke("enemyDestroy", switchTime);
			}*/
			sounds(dead);
			isDying = true;
			Invoke("enemyDestroy", 1.5f);
			
		} else if(!isCol && !isDying){
			sounds(hitEnemy);
			Debug.Log("Getroffen");
			gameObject.GetComponent<SpriteRenderer>().sprite = hitPicture;
			isCol = true;
			Invoke("enemyNormalPicture",switchTime);
		}
		
	}

	void enemyDestroy() {
		switch (kindOfEnemy) {
			case 1: player.GetComponent<PlayerMovement>().setScore(100); break;
			case 2: player.GetComponent<PlayerMovement>().setScore(150); break;
			case 3: player.GetComponent<PlayerMovement>().setScore(500); break;
		}
		Destroy(this.gameObject);
		isDying = false;
		isCol = false;
	}

	void enemyNormalPicture() {
	
		gameObject.GetComponent<SpriteRenderer>().sprite = normalPicture;
		health--;
		isCol = false;
	}

    void OnTriggerExit2D(Collider2D col) {
		//Debug.Log(col.gameObject.layer);
		
		switch (col.gameObject.layer) {
			case 8://
				player.GetComponentInChildren<Weapon>().setFire(true);
				playerIshit = false;
				//Debug.Log("verlassen");
				break;
		}
		CancelInvoke();
    }

    private void enemyHitPlayer() {
		sounds(hitPlayer);
		player.GetComponent<PlayerMovement>().decreaseHealth(damage);
    }

	private void sounds (AudioClip x){
		audioSource = GetComponent<AudioSource>();
		audioSource.volume = PlayerPrefsManager.GetMasterVolume();
		audioSource.clip = x;
		audioSource.Play();
	}

	private void soundsNormal(AudioClip x) {
		audioSource = GetComponent<AudioSource>();
		audioSource.volume = PlayerPrefsManager.GetMasterVolume();
		audioSource.loop = true;
		audioSource.clip = x;
		audioSource.Play();
	}
}
