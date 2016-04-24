using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
	public float speed;
    public float health;
	public Text wave;
    public Text life;
	public Text highscore;
	public Image headImage;
	public Sprite[] characterSprites;
	public Sprite[] headSprites;
	private static bool noMove;
	private SpriteRenderer currentSprite;
	private bool stand;
	private bool isAnima;
	private bool stop = false;
	private double score;
	GameObject textWave;

	void Start () {
		textWave = GameObject.Find("Spawner");
		health = 100;
		score = 0;
		isAnima = false;
		currentSprite = gameObject.GetComponent<SpriteRenderer>();
		life.text = health.ToString();
		highscore.text = score.ToString();
    }

	

	// Update is called once per frame
	void Update() {



		int spriteA;
		int spriteB;
		int spriteC;
		Transform weapon = gameObject.transform.FindChild("arrow");
		float grad = weapon.rotation.eulerAngles.z;
		if (grad > 45 && grad <= 135) {
			spriteA = 7;
			spriteB = 8;
			spriteC = 6;
		} else if (grad > 135 && grad <= 225) {
			spriteA = 4;
			spriteB = 5;
			spriteC = 3;
		} else if (grad > 225 && grad <= 315) {
			spriteA = 10;
			spriteB = 11;
			spriteC = 9;
		} else {
			spriteA = 1;
			spriteB = 2;
			spriteC = 0;
		}

		highscore.text = score.ToString();

		if (health <= 100 && health >= 80) {
			headImage.sprite = headSprites[0];
		} else if (health < 80 && health >= 60) {
			headImage.sprite = headSprites[1];
		} else if (health < 60 && health >= 40) {
			headImage.sprite = headSprites[2];
		} else if (health < 40 && health >= 20) {
			headImage.sprite = headSprites[3];
		} else if (health < 20 && health >0) {
			headImage.sprite = headSprites[4];
		} else if (health < 0) {
			stop = true;
			headImage.sprite = headSprites[5];
			life.text = "0";
		}

		//===========================Bewegen=============================
		if (!noMove) {
			wave.GetComponent<Text>().enabled = false;
			if (Input.GetKey(KeyCode.A)) {
				transform.position += Vector3.left * speed * Time.deltaTime;
				animationSprite(spriteA, spriteB, spriteC);
			} else if (Input.GetKey(KeyCode.D)) {
				transform.position += Vector3.right * speed * Time.deltaTime;
				animationSprite(spriteA, spriteB, spriteC);
			}

			if (Input.GetKey(KeyCode.W)) {
				transform.position += Vector3.up * speed * Time.deltaTime;
				if (!(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))) {
					animationSprite(spriteA, spriteB, spriteC);
				}
			} else if (Input.GetKey(KeyCode.S)) {
				transform.position += Vector3.down * speed * Time.deltaTime;
				if (!(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))) {
					animationSprite(spriteA, spriteB, spriteC);
				}
			}

			if (!(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))) {
				stand = true;
			} else {
				stand = false;
			}

		} else {
			currentSprite.sprite = characterSprites[0]; //stehsprite
			
			wave.GetComponent<Text>().enabled = true;
			wave.text = "wave " + textWave.GetComponent<EnemySpawner>().getWaveCounter().ToString();
		}
        //============================Zurück zum Menü====================

        if (Input.GetKeyDown(KeyCode.Escape)) {
            SceneManager.LoadScene("01a Start");
        }

        if (isPlayerDead()) {
			life.text = "0";
			wave.enabled = true;
			highscore.enabled = false;
			noMove = true;
			wave.text = "you have " + score + " points and survived " + (textWave.GetComponent<EnemySpawner>().getWaveCounter()-1) + " waves";
			Invoke("back", 5f);

        }
	}

	public void setScore(double x) {
		score = score +  x;
	}

	private void back() {
		Destroy(GameObject.Find("Persistent Music"));
		SceneManager.LoadScene("01a Start");
	}

	private void animationSprite(int a, int b, int c) {
		if (stand) {
			currentSprite.sprite = characterSprites[c];
		} else {
			if (isAnima) {
				StartCoroutine(anima(a, 0.5f));
			}else {
				StartCoroutine(anima(b, 0.5f));
			}
		}
	}

	IEnumerator anima(int x, float delaytime) {
		//Debug.Log("animation gestartet mit   " + x);
		yield return new WaitForSeconds(delaytime);
		currentSprite.sprite = characterSprites[x];
		if (isAnima) {
			isAnima = false;
		} else {
			isAnima = true;
		}
	}
	public static void setNoMove(bool x) {
		noMove = x;
	}

	public static bool getNoMove() {
		return noMove;
	}

	public void decreaseHealth(float value) {
		if (!stop) {
			health -= value;
			life.text = health.ToString();
		}
		
	}

    private bool isPlayerDead() {
        if(health <= 0) {
            return true;
        }
        return false;
    }

   
    
	
}

