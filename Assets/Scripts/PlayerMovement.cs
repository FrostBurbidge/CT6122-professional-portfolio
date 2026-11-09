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

    // Temporary testing field remove later
    [SerializeField]
    private StatModifier initialAdd;

    // Start is called before the first frame update
    void Start()
    {
        stats = ScriptableObject.Instantiate(stats);
        stats.Setup();
        rb = GetComponent<Rigidbody2D>();
        ApplyModifier("Speed", initialAdd);
        Debug.Log(stats.GetStatValue("Speed"));
    }

    void FixedUpdate()
    {
        // Lowers the cooldown of the attacks
        if (attackCooldown > 0)
        {
            attackCooldown -= Time.deltaTime;
        }
    }

    private void OnMove(InputValue value)
    {
        // Get input direction
        Vector2 dir = value.Get<Vector2>();

        //Debug.Log(dir);

        //Debug.Log(dir * Time.deltaTime * stats.GetStatValue("Speed"));

        rb.linearVelocity = dir * Time.deltaTime * stats.GetStatValue("Speed");
    }

    private void OnFire(InputValue value)
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

    private void ApplyModifier(string statName, StatModifier mod)
    {
        mod = ScriptableObject.Instantiate(mod);

        stats.ApplyModifier(statName, mod);

        if (!mod.IsPermanent())
        {
            StartCoroutine(StatDuration(mod));
        }
    }

    private IEnumerator StatDuration(StatModifier mod)
    {
        yield return new WaitForSeconds(mod.GetDuration());
        // REMOVE THE STAT 
    }
}
