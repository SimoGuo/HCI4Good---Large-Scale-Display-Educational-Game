using UnityEngine;

public class character : MonoBehaviour
{
    public float maxHealth = 100;
    public float currentHealth;
    public bool isSelected = false;
    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        gameObject.SetActive(false);
    }
}
