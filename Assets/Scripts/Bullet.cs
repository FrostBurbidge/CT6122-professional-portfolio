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

    public void Setup(GameObject owner, float damage) {
        this.owner = owner;
        this.damage = damage;
        StartCoroutine(LifeTime());
    }

    private IEnumerator LifeTime() {
        yield return new WaitForSeconds(bulletLifeTime);
        Destroy(this);
    }
}
