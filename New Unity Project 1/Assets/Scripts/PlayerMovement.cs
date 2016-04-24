using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
	public float speed;
    public float health;
	public Text wave;
    public Text life;
	private static bool noMove;

	void Start () {
        health = 100;
        life.text = health.ToString();
    }

	

	// Update is called once per frame
	void Update() {

		//===========================Bewegen=============================
		if (!noMove) {
			wave.GetComponent<Text>().enabled = false;
			if (Input.GetKey(KeyCode.A)) {
				transform.position += Vector3.left * speed * Time.deltaTime;
			} else if (Input.GetKey(KeyCode.D)) {
				transform.position += Vector3.right * speed * Time.deltaTime;
			}
			if (Input.GetKey(KeyCode.W)) {
				transform.position += Vector3.up * speed * Time.deltaTime;
			} else if (Input.GetKey(KeyCode.S)) {
				transform.position += Vector3.down * speed * Time.deltaTime;
			}
		} else {
			GameObject test = GameObject.Find("Spawner");
			wave.GetComponent<Text>().enabled = true;
			wave.text = "wave " + test.GetComponent<EnemySpawner>().getWaveCounter().ToString();
		}
        //============================Zurück zum Menü====================

        if (Input.GetKeyDown(KeyCode.Escape)) {
            SceneManager.LoadScene("01a Start");
        }

        if (isPlayerDead()) {
            SceneManager.LoadScene("01a Start");
        }
	}

	public static void setNoMove(bool x) {
		noMove = x;
	}

	public static bool getNoMove() {
		return noMove;
	}

    public void decreaseHealth(float value) {
        health -= value;
        life.text = health.ToString();
    }

    private bool isPlayerDead() {
        if(health <= 0) {
            return true;
        }
        return false;
    }

   
    
	
}

