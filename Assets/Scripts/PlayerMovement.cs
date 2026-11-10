using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Tooltip("The player stats object.")]
    [SerializeField]
    private PlayerStats stats;

    [Tooltip("The bullet prefab.")]
    [SerializeField]
    private GameObject bulletPrefab;

    // The rigidbody
    private Rigidbody2D rb;

    // The current attack cooldown
    private float attackCooldown = 0;
    // If attack is held
    private bool attacking = false;

    // Start is called before the first frame update
    void Start()
    {
        stats = ScriptableObject.Instantiate(stats);
        stats.Setup();
        rb = GetComponent<Rigidbody2D>();
        Debug.Log(stats.GetStatValue("Speed"));
    }

    void FixedUpdate()
    {
        // Lowers the cooldown of the attacks
        if (attackCooldown > 0)
        {
            attackCooldown -= Time.deltaTime;
        } if (attacking && attackCooldown <= 0) {
            FireBullet();
        }
    }

    private void OnMove(InputValue value)
    {
        // Get input direction
        Vector2 dir = value.Get<Vector2>().normalized;

        //Debug.Log(dir);

        //Debug.Log(Time.fixedDeltaTime * stats.GetStatValue("Speed"));

        rb.linearVelocity = dir * Time.fixedDeltaTime * stats.GetStatValue("Speed");
    }

    // Set up autofire
    private void OnFire(InputValue value) {
        attacking = value.Get<float>() == 1;
    }

    private void FireBullet() {
        if (attackCooldown <= 0) {
            // Debug player damage print
            Debug.Log(stats.GetStatValue("Damage"));

            // 1 over to make the number more intuitive, as then the attack speed is
            // Number of attacks per second
            attackCooldown = 1 / stats.GetStatValue("AttackSpeed");

            GameObject b = Instantiate(bulletPrefab);
            b.transform.position = transform.position;

            // Rotating to mouse
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 rotation = mousePos - transform.position;
            float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

            b.transform.rotation = Quaternion.Euler(0f, 0f, rotZ);

            if (b.GetComponent<Bullet>() != null) { 
                Bullet bulletScript = b.GetComponent<Bullet>();
                bulletScript.Setup(gameObject, stats.GetStatValue("Damage"), rotation);
            }

        }
    }

    private void ApplyModifier(string statName, StatModifier mod)
    {
        mod = ScriptableObject.Instantiate(mod);

        stats.ApplyModifier(statName, mod);

        if (!mod.IsPermanent())
        {
            StartCoroutine(StatDuration(statName, mod));
        }
    }

    // Stats are scriptableobjects and can't run coroutines
    private IEnumerator StatDuration(string statName, StatModifier mod)
    {
        yield return new WaitForSeconds(mod.GetDuration());
        stats.RemoveModifier(statName, mod);
        print(stats.GetStatValue("Speed"));
    }
}
