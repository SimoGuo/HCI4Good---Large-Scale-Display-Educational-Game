using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusBarManager : MonoBehaviour
{
    [SerializeField] HealthBar healthBar;
    [SerializeField] ManaBar manaBar;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.A))
        {
            healthBar.DecreaseHealth(10);
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            healthBar.IncreaseHealth(10);
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            manaBar.DecreaseMana(10);
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            manaBar.IncreaseMana(10);
        }
        if (Input.GetKeyUp(KeyCode.E))
        {
            SetMaxHealth(1000);
        }
        if(Input.GetKeyUp(KeyCode.F)) 
        { 
            SetMaxMana(1000); 
        }
    }

    public void IncreaseHealth(float health)
    {
        healthBar.IncreaseHealth(health);
    }

    public void DecreaseHealth(float health)
    {
        healthBar.DecreaseHealth(health);
    }

    public void IncreaseMana(float mana)
    {
        manaBar.IncreaseMana(mana);
    }

    public void DecreaseMana(float mana)
    {
        manaBar.DecreaseMana(mana);
    }

    public void SetMaxHealth(float maxHealth)
    {
        healthBar.SetMaxHealth(maxHealth);
    }

    public void SetMaxMana(float maxMana)
    {
        manaBar.SetMaxMana(maxMana);
    }
}
