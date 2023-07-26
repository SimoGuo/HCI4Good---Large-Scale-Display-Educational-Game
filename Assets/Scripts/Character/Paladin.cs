using UnityEngine;

public class Paladin : MonoBehaviour
{
    // Paladin stats
    public float attackPower = 20f;
    public float defense = 50f;
    public float movementSpeed = 1f;

    // Ability cooldowns
    private float slashCooldown = 1f;
    private float healCooldown = 5f;
    private float blessCooldown = 8f;

    private float nextSlashTime = 0f;
    private float nextHealTime = 0f;
    private float nextBlessTime = 0f;

    // Abilities
    private Transform target;
    private bool isHealing = false;

    private void Update()
    {
        // Check for player input to trigger abilities
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Slash();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Heal();
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Bless();
        }
    }

    private void Slash()
    {
        // Check if slash is on cooldown
        if (Time.time < nextSlashTime)
            return;

        // Find the enemy with the highest HP
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject targetEnemy = null;
        float maxHP = 0f;
        float slashRange = 2.0f; // Adjust the range as per your requirements

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy <= slashRange)
            {
                Enemy enemyScript = enemy.GetComponent<Enemy>();
                if (enemyScript != null && enemyScript.health > maxHP)
                {
                    targetEnemy = enemy;
                    maxHP = enemyScript.health;
                }
            }
        }

        // Perform the slash action if an enemy is found within range
        if (targetEnemy != null)
        {
            float slashDamage = attackPower;

            // Apply damage to the target enemy
            Enemy targetEnemyScript = targetEnemy.GetComponent<Enemy>();
            if (targetEnemyScript != null)
            {
                targetEnemyScript.TakeDamage(slashDamage);
            }

            // Set the cooldown for the next slash
            nextSlashTime = Time.time + slashCooldown;
        }
    }


    private void Heal()
    {
        // Check if heal is on cooldown
        if (Time.time < nextHealTime)
            return;

        // Find the ally with the highest defense (assuming allies have a script with "defense" variable)
        GameObject[] allies = GameObject.FindGameObjectsWithTag("Ally");
        GameObject targetAlly = null;
        float maxDefense = 0f;

        foreach (GameObject ally in allies)
        {
            Ally allyScript = ally.GetComponent<Ally>();
            if (allyScript != null && allyScript.defense > maxDefense)
            {
                targetAlly = ally;
                maxDefense = allyScript.defense;
            }
        }

        // Perform the heal action if an ally is found
        if (targetAlly != null)
        {
            float healAmount = defense * 0.5f;

            Ally targetAllyScript = targetAlly.GetComponent<Ally>();
            if (targetAllyScript != null)
            {
                targetAllyScript.Heal(healAmount);
            }

            // Set the cooldown for the next heal
            nextHealTime = Time.time + healCooldown;
        }
    }

    private void Bless()
    {
        // Check if bless is on cooldown
        if (Time.time < nextBlessTime)
            return;

        // Find all allies (assuming allies have a script with "buffDuration" variable)
        GameObject[] allies = GameObject.FindGameObjectsWithTag("Ally");

        // Apply the Bless buff to all allies
        foreach (GameObject ally in allies)
        {
            Ally allyScript = ally.GetComponent<Ally>();
            if (allyScript != null)
            {
                // Apply buff to attack power, defense, and speed
                allyScript.ApplyBuff(1.2f, 1.2f, 0.8f);
                allyScript.buffDuration = 10f; // Adjust the duration as needed
            }
        }

        // Set the cooldown for the next bless
        nextBlessTime = Time.time + blessCooldown;
    }
}
