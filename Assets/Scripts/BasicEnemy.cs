using UnityEngine;

public class BasicEnemy : MonoBehaviour
{
    [Tooltip("If the enemy should attack.")]
    [SerializeField]
    bool attacking = false;

    [Tooltip("The player stats object.")]
    [SerializeField]
    private PlayerStats stats;

    [Tooltip("The bullet prefab.")]
    [SerializeField]
    private GameObject bulletPrefab;

    // The health component
    private HealthComponent health;

    // The current attack cooldown
    private float attackCooldown = 0;

    // Start is called before the first frame update
    void Start()
    {
        stats = ScriptableObject.Instantiate(stats);
        stats.Setup();
        health = GetComponent<HealthComponent>();
        if (GetComponent<HealthComponent>())
        {
            health.Setup(stats);
        }
    }

    void FixedUpdate()
    {
        if (attacking)
        {
            // Lowers the cooldown of the attacks
            if (attackCooldown > 0)
            {
                attackCooldown -= Time.deltaTime;
            }
            if (attackCooldown <= 0)
            {
                FireBullet();
            }
        }
    }

    private void FireBullet()
    {
        if (attackCooldown <= 0)
        {
            // 1 over to make the number more intuitive, as then the attack speed is
            // Number of attacks per second
            attackCooldown = 1 / stats.GetStatValue("AttackSpeed");

            GameObject b = Instantiate(bulletPrefab);
            b.transform.position = transform.position;

            b.transform.rotation = Quaternion.Euler(0f, 0f, 0f);

            if (b.GetComponent<Bullet>() != null)
            {
                Bullet bulletScript = b.GetComponent<Bullet>();
                bulletScript.Setup(gameObject, stats.GetStatValue("Damage"), Vector2.down);
            }

        }
    }
}
