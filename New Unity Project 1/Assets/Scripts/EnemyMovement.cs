using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour {


    public float speed;
    public float hitTime;
    private GameObject player;
    private float Range;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
        GameObject target = GameObject.FindGameObjectWithTag("Player");
        Debug.DrawLine(transform.position, target.transform.position);

        Range = Vector2.Distance(transform.position, target.transform.position);
        if (Range >= 0.5f) {
               transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
        }
    }

    void OnTriggerEnter2D(Collider2D col) {
        enemyHitPlayer();
        InvokeRepeating("enemyHitPlayer", hitTime, hitTime);
    }

    void OnTriggerExit2D(Collider2D col) {
        CancelInvoke();
    }

    private void enemyHitPlayer() {
        player.GetComponent<PlayerMovement>().decreaseHealth(10);
    }
}
