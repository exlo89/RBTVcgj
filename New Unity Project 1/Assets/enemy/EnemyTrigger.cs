using UnityEngine;
using System.Collections;

public class EnemyTrigger : MonoBehaviour {

	private EnemyBehavior enemyBehaviorScript;
	private void Start() {
		enemyBehaviorScript = this.GetComponentInParent<EnemyBehavior>();
	}

    private void OnTriggerEnter2D(Collider2D col) {
        switch (col.gameObject.layer){
            case 8://spieler
                Weapon.ableToFire = false;
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
        Weapon.ableToFire = true;
    }
}
