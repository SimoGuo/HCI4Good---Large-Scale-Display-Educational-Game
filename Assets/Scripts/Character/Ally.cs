using UnityEngine;
using System.Collections;

public class Ally : MonoBehaviour
{
    public float health = 100f;
    public float defense = 30f;
    public float movementSpeed = 8f;
    public float buffDuration = 0f; // Duration of the Bless buff

    private float originalDefense;
    private float originalMovementSpeed;

    private void Start()
    {
        // Store the original values to reset them after the Bless buff expires
        originalDefense = defense;
        originalMovementSpeed = movementSpeed;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0f)
        {
            Die();
        }
    }

    private void Die()
    {
        // Implement what happens when the ally dies
        Destroy(gameObject);
    }

    public void Heal(float amount)
    {
        health += amount;
        // Ensure health doesn't exceed the maximum value (if needed).
    }

    public void ApplyBuff(float attackBuff, float defenseBuff, float speedBuff)
    {
        // Apply the Bless buff to the ally.
        defense *= defenseBuff;
        movementSpeed *= speedBuff;

        // Start a coroutine to reset the buffs after a certain duration.
        StartCoroutine(ResetBuffsAfterDuration());
    }

    private IEnumerator ResetBuffsAfterDuration()
    {
        yield return new WaitForSeconds(buffDuration);

        // Reset the buffs to their original values.
        defense = originalDefense;
        movementSpeed = originalMovementSpeed;
    }
}
