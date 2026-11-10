using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Bullet stats - can both be integrated into stat system if needed
    [Header("The time in seconds before the bullet is destroyed.")]
    [SerializeField]
    float bulletLifeTime = 3.0f;
    [Header("The speed of the bullets.")]
    [SerializeField]
    float bulletSpeed = 500f;


    // The bullet damage
    private float damage = 0;
    // The bullet's owner
    private GameObject owner = null;
    // The bullet's direction
    private Vector2 direction;

    // The bullet's rigidbody
    private Rigidbody2D rb;

    public void Setup(GameObject owner, float damage, Vector2 direction) {
        this.owner = owner;
        this.damage = damage;
        this.direction = direction;
        StartCoroutine(LifeTime());
        rb = GetComponent<Rigidbody2D>();
    }

    private IEnumerator LifeTime() {
        yield return new WaitForSeconds(bulletLifeTime);
        Destroy(gameObject);
    }

    private void FixedUpdate() {
        rb.linearVelocity = direction * Time.fixedDeltaTime * bulletSpeed;
    }
}
