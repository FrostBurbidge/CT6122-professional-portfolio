using TMPro;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    [Tooltip("Health text display object")]
    [SerializeField]
    private TextMeshProUGUI healthText;

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
        UpdateText();
    }

    // Fixed update so it runs less times
    public void FixedUpdate()
    {
        if (maxHealth != stats.GetStatValue("MaxHealth"))
        {
            // Keeps health being the same percentage of max
            float percent = (float)health / (float)maxHealth;

            // Casting to int to avoid decimal health
            maxHealth = (int)stats.GetStatValue("MaxHealth");
            health = ((int)(maxHealth * percent));
            UpdateText();
        }
    }

    // Take damage function
    public void TakeDamage(float damage)
    {
        float armor = stats.GetStatValue("Armor");
        damage -= armor;
        // Preventing complete damage immunity
        if (damage <= 0)
        {
            damage = 1;
        }
        
        health -= (int)damage;
        UpdateText();
    }

    // Updates the healthtext if it exists
    private void UpdateText()
    {
        if (healthText != null)
        {
            healthText.text = health + " / " + maxHealth;
        }
    }
}
