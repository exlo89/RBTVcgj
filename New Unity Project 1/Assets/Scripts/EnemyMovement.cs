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
	private int health;
	private GameObject player;
    private float Range;
	private float damage;
	private bool isCol = false;
	private bool isDying = false;


	// Use this for initialization
	void Start () {
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
        if (Range >= 0.5f && !PlayerMovement.getNoMove() && !isDying) {
               transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
        }
    }

    void OnTriggerEnter2D(Collider2D col) {
		//Debug.Log(col.gameObject.layer);
		switch (col.gameObject.layer) {
			case 8://spieler
				enemyHitPlayer();
				InvokeRepeating("enemyHitPlayer", hitTime, hitTime);
				break;
			case 11://projektil
				Destroy(col.gameObject);
				healthPointsEnemy(kindOfEnemy);
				break;
		}
    }

	void healthPointsEnemy(int enemy) {
		if(health <= 0 && !isCol) {
			//Debug.Log("Tot");
			gameObject.GetComponent<SpriteRenderer>().sprite = deadPicture;
			isCol = true;
			isDying = true;
			Invoke("enemyDestroy", switchTime);
			
		} else if(!isCol){
			health--;
			//Debug.Log("Getroffen");
			gameObject.GetComponent<SpriteRenderer>().sprite = hitPicture;
			isCol = true;
			Invoke("enemyNormalPicture",switchTime);
		}
		
	}

	void enemyDestroy() {
		Destroy(this.gameObject);
		isDying = false;
		isCol = false;
	}

	void enemyNormalPicture() {
		gameObject.GetComponent<SpriteRenderer>().sprite = normalPicture;
		isCol = false;
	}

    void OnTriggerExit2D(Collider2D col) {
        CancelInvoke();
    }

    private void enemyHitPlayer() {
        player.GetComponent<PlayerMovement>().decreaseHealth(damage);
    }
}
