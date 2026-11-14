using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    [Header("Read only - updated in game.")]

    [Tooltip("Current max health - View only")]
    [SerializeField]
    private int maxHealth;

    [Tooltip("Current health - View only")]
    [SerializeField]
    private int health;

    private PlayerStats stats;

    // Constructor
    public void Setup(PlayerStats pStats)
    {
        stats = pStats;
        maxHealth = (int)stats.GetStatValue("MaxHealth");
        health = maxHealth;
    }

    public void FixedUpdate()
    {
        if (maxHealth != stats.GetStatValue("MaxHealth"))
        {
            // Keeps health being the same percentage of max
            float percent = (float)health / (float)maxHealth;

            // Casting to int to avoid decimal health
            maxHealth = (int)stats.GetStatValue("MaxHealth");
            health = ((int)(maxHealth * percent));
        }
    }
}
