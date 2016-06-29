using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

    public GameObject enemyPrefab1;
	public GameObject enemyPrefab2;
	public GameObject enemyPrefab3;
	public float dropRate1;
	public float dropRate2;
	public float dropRate3;
    public float speed = 5f;
    public float spawnDelay = 0.5f;
	private float startPositionZ;
	private int counter = 0;
	private int waveCounter = 1;

	// Use this for initialization
	void Start () {
		PlayerBehavior.setNoMove(true);
        spawnWave();
    }
    // Update is called once per frame
    void Update() {
    
        if(AllMembersDead()) {
            spawnWave();
        }
    }

    void spawnWave() {
		PlayerBehavior.setNoMove(true);
        Transform freePosition = NextFreePosition();
        if(freePosition) {
			GameObject enemy = Instantiate(chooseEnemy(), new Vector3(freePosition.position.x, freePosition.position.y, startPositionZ), Quaternion.identity) as GameObject;
			enemy.transform.parent = freePosition;
			counter++;
		}
        if(NextFreePosition()) {
            Invoke("spawnWave", spawnDelay);
        }

		if (counter == 10) {
			counter = 0;
			waveCounter++;
			PlayerBehavior.setNoMove(false);
		}
		
    }

	private GameObject chooseEnemy() {
		if(Random.Range(0f,1f) <= dropRate3) {
			startPositionZ = enemyPrefab3.transform.position.z;
			return enemyPrefab3;
		} else if(Random.Range(0f, 1f) <= dropRate2) {
			startPositionZ = enemyPrefab2.transform.position.z;
			return enemyPrefab2;
		} else {
			startPositionZ = enemyPrefab1.transform.position.z;
			return enemyPrefab1;
		}
	} 

	public int getWaveCounter() {
//		Debug.Log("test " + waveCounter);
		return waveCounter;
	}
    Transform NextFreePosition() {
        foreach(Transform childPositionGameObject in transform) {
            if(childPositionGameObject.childCount == 0) {
                return childPositionGameObject.transform;
            }
        }
        return null;
    }

    private bool AllMembersDead() {
        foreach(Transform childPositionGameObject in transform) {
            if(childPositionGameObject.childCount > 0) {
                return false;
            } 
        }
        return true;
    }
}
