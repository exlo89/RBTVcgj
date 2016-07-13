using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {
    public float fireRate = 0.5f;
    public float damage = 10;
    public LayerMask whatToHit;
    public Transform projectile;
	public bool ableToFire = true;

    PlayerBehavior playerBehaviorScript;

    void Start() {
        playerBehaviorScript = GameObject.Find("player prefab").GetComponent<PlayerBehavior>();
    }

	// Update is called once per frame
	void Update () {
		calculateRotation();
		if (!(playerBehaviorScript.getNoMove()) && ableToFire) {
			if (Input.GetKeyDown(KeyCode.Mouse0)) {
				InvokeRepeating("Fire", 0.000001f, fireRate);
			}
		}
		if (Input.GetKeyUp(KeyCode.Mouse0) || playerBehaviorScript.getNoMove()) {
			CancelInvoke("Fire");
		}
	}

	public void setAbleToFire(bool x) {
		ableToFire = x;
	}

	void calculateRotation() {
		Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
		difference.Normalize();
		float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Euler(0f, 0f, rotZ + 90);	
	}

   void Fire() {
        Instantiate(projectile, transform.position, transform.rotation);
    }
}
