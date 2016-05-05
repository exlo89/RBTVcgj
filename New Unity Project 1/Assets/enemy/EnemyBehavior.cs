using UnityEngine;
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
	private GameObject player;
    private float Range;
	private float damage;
	private AudioSource audioSource;
	private bool isCol;
	

	void Start () { 
		audioSource = GetComponent<AudioSource>();
		audioSource.volume = PlayerPrefsManager.GetMasterVolume();
		sounds(normal);
		player = GameObject.FindGameObjectWithTag("Player");
		switch (kindOfEnemy) {
			case 1: //schnellen gegner 2 health
				health = 2;
				damage = 10;
				break;
			case 2: //zombie gegner 3 health
				health = 3;
				damage = 20;
				break;
			case 3: // boss 10 health
				health = 10;
				damage = 40;
				hitTime = 3;
				break;
		}
	}
	
	// Update is called once per frame
	void Update () {
        GameObject target = GameObject.FindGameObjectWithTag("Player");
        Debug.DrawLine(transform.position, target.transform.position);

		// der gegner bewegt sich auf den spieler zu

		Range = Vector2.Distance(transform.position, target.transform.position);
        if (Range >= 0.5f) {
               transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
        }
    }

    void OnTriggerEnter2D(Collider2D col) {
		switch (col.gameObject.layer) {
			case 8://spieler
				player.GetComponentInChildren<Weapon>().setFire(false);
				enemyHitPlayer();
				InvokeRepeating("enemyHitPlayer", hitTime, hitTime);
				break;
			case 11://projektil
				Destroy(col.gameObject);
				health--;
				healthPointsEnemy();
				break;
		}
    }

	void OnTriggerExit2D(Collider2D col) {
		CancelInvoke();
		switch (col.gameObject.layer) {
			case 8://
				player.GetComponentInChildren<Weapon>().setFire(true);
				break;
		}
	}

	void healthPointsEnemy() {
		if(health <= 0) {
			Debug.Log("Tot");
			gameObject.GetComponent<SpriteRenderer>().sprite = deadPicture;
			sounds(dead);
			Invoke("enemyDestroy", 1f);
		} else {
			sounds(hitEnemy);
			gameObject.GetComponent<SpriteRenderer>().sprite = hitPicture;
			Invoke("enemyNormalPicture",switchTime);
		}
		
	}

	void enemyDestroy() {
		switch (kindOfEnemy) {
			case 1: player.GetComponent<PlayerBehavior>().setScore(100); break;
			case 2: player.GetComponent<PlayerBehavior>().setScore(150); break;
			case 3: player.GetComponent<PlayerBehavior>().setScore(500); break;
		}
		Destroy(this.gameObject);
	}

	void enemyNormalPicture() {
		gameObject.GetComponent<SpriteRenderer>().sprite = normalPicture;
		sounds(normal);
	}

    private void enemyHitPlayer() {
		sounds(hitPlayer);
		player.GetComponent<PlayerBehavior>().decreaseHealth(damage);
    }

	private void sounds (AudioClip x){
		audioSource.clip = x;
		audioSource.Play();
	}
}
