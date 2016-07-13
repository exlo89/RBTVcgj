using UnityEngine;
using System.Collections;

public class EnemyTrigger : MonoBehaviour {

	EnemyBehavior enemyBehaviorScript;
    Weapon weaponScript;
	private void Start() {
		enemyBehaviorScript = this.GetComponentInParent<EnemyBehavior>();
        weaponScript = GameObject.Find("weapon").GetComponent<Weapon>();
	}

    private void OnTriggerEnter2D(Collider2D col) {
        switch (col.gameObject.layer){
            case 8://spieler
                weaponScript.ableToFire = false;
                enemyBehaviorScript.enemyHitPlayer();
                break;
            case 11://projektil
                Destroy(col.gameObject);
				enemyBehaviorScript.decreaseHealth(1);
                enemyBehaviorScript.healthPointsEnemy();
                break;
        }
    }

	public void enableCollider(bool x) {
		GetComponent<PolygonCollider2D>().enabled = x;
	}

    private void OnTriggerExit2D(Collider2D col) {
        weaponScript.ableToFire = true;
    }
}
