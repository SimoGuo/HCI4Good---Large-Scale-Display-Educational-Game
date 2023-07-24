using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarTest : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;

    public healthBar healthBar;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A)) 
        {
            Console.WriteLine("Pressed A");
            TakeDamage(10);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            Console.WriteLine("Pressed S");
            Heal(10);
        }
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.DecreaseHealth(damage);
    }

    void Heal(int healAmount)
    {
        currentHealth += healAmount;
        healthBar.IncreaseHealth(healAmount);
    }
}
