using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Tooltip("The player stats object.")]
    [SerializeField]
    private PlayerStats stats;

    // The rigidbody
    private Rigidbody2D rb;

    // The current attack cooldown
    private float attackCooldown = 0;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(stats.GetStatValue("Speed"));
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        // Lowers the cooldown of the attacks
        if (attackCooldown > 0)
        {
            attackCooldown -= Time.deltaTime;
        }
    }

    public void OnMove(InputValue value)
    {
        // Get input direction
        Vector2 dir = value.Get<Vector2>();

        //Debug.Log(dir);

        //Debug.Log(dir * Time.deltaTime * stats.GetStatValue("Speed"));

        rb.linearVelocity = dir * Time.deltaTime * stats.GetStatValue("Speed");
    }

    public void OnFire(InputValue value)
    {
        if (attackCooldown <= 0)
        {
            // Debug player damage print
            Debug.Log(stats.GetStatValue("Damage"));

            // 1 over to make the number more intuitive, as then the attack speed is
            // Number of attacks per second
            attackCooldown = 1/stats.GetStatValue("AttackSpeed");
        }
    }
}
