using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthBar : MonoBehaviour
{
    public Slider slider;

    public void SetMaxHealth(int maxHealth)
    {
        slider.maxValue = maxHealth;
        slider.value = maxHealth;
    }

    public void Sethealth(int health)
    {
        slider.value = health;
    }

    public int getHealth()
    {
        return (int)slider.value;
    }

    public void IncreaseHealth(int health)
    {
        slider.value += health;
    }

    public void DecreaseHealth(int health)
    {
        slider.value -= health;
    }
}
