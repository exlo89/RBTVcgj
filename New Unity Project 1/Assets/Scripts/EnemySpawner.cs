using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

    public GameObject enemyPrefab;
    public float speed = 5f;
    public float spawnDelay = 0.5f;

	// Use this for initialization
	void Start () {
        spawnWave();
    }
    // Update is called once per frame
    void Update() {
    
        if(AllMembersDead()) {
            spawnWave();
        }
    }

    void SpawnEnemys() {
        foreach(Transform child in transform) {
            GameObject enemy = Instantiate(enemyPrefab, child.position, Quaternion.identity) as GameObject;
            enemy.transform.parent = child;
        }
    }

    void spawnWave() {
        Transform freePosition = NextFreePosition();
        if(freePosition) {
            GameObject enemy = Instantiate(enemyPrefab, freePosition.position, Quaternion.identity) as GameObject;
            enemy.transform.parent = freePosition;
        }
        if(NextFreePosition()) {
            Invoke("spawnWave", spawnDelay);
        }
    }

    Transform NextFreePosition() {
        foreach(Transform childPositionGameObject in transform) {
            if(childPositionGameObject.childCount == 0) {
                return childPositionGameObject.transform;
            }
        }
        return null;
    }

    bool AllMembersDead() {
        foreach(Transform childPositionGameObject in transform) {
            if(childPositionGameObject.childCount > 0) {
                return false;
            } 
        }
        return true;
    }
}
