using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {
    public float damage = 10f;
    public int moveSpeed = 100;

    void Update() {
        transform.Translate(Vector3.down * Time.deltaTime * moveSpeed);
        Destroy(this.gameObject, 1);
    }

    public float GetDamage() {
        return damage;
    }

    public void Hit() {
        Destroy(gameObject);
    }
}
