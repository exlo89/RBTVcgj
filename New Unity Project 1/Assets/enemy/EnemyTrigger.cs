using UnityEngine;
using System.Collections;

public class EnemyTrigger : MonoBehaviour {

	private EnemyBehavior x;
	private void Start() {
		x = this.GetComponentInParent<EnemyBehavior>();
	}


    private void OnTriggerEnter2D(Collider2D col)
    {
        switch (col.gameObject.layer)
        {
            case 8://spieler
                Weapon.ableToFire = false;
                x.enemyHitPlayer();
                //InvokeRepeating("enemyHitPlayer", hitTime, hitTime);
                break;
            case 11://projektil
                Destroy(col.gameObject);
				x.decreaseHealth(1);
                x.healthPointsEnemy();
                break;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        //switch (col.gameObject.layer) {
        //case 8://
        //CancelInvoke();
        Weapon.ableToFire = true;
        //break;
        //}
    }
}
