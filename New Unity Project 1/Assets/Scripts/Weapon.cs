﻿using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {
    public float fireRate = 0f;
    public float damage = 10;
    public LayerMask whatToHit;
    public Transform bullet;
    float timeToFire = 0f;
    Transform firePoint;
    void Awake() {
        firePoint = transform.FindChild("FirePoint");
        if (firePoint == null) {

        }
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        difference.Normalize();
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ + 90);

        //======================Feuer====================================
        if (Input.GetKeyDown(KeyCode.Mouse0)) {
            InvokeRepeating("Fire", 0.000001f, fireRate);
        }
        if (Input.GetKeyUp(KeyCode.Mouse0)) {
            CancelInvoke("Fire");
        }

    }

   void Fire() {
        Vector2 mousePosition = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        Vector2 firePointPosition = new Vector2(firePoint.position.x, firePoint.position.y);
        RaycastHit2D hit = Physics2D.Raycast(firePointPosition, mousePosition - firePointPosition, 100f, whatToHit);

        Instantiate(bullet, firePoint.position, firePoint.rotation);

        Debug.DrawLine(firePointPosition, (mousePosition - firePointPosition) * 100);
        if(hit.collider != null) {
            Debug.DrawLine(firePointPosition, hit.point, Color.red);
            Debug.Log("Getroffen ein " + hit.collider.name);
        }
    }
}