using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthBar : MonoBehaviour
{
    public Slider frontHealthBarSlider;
    public Slider backHealthBarSlider;
    private float currentHealth;
    [SerializeField] private float maxHealth;
    private float lerpTimer;
    [SerializeField] private float chipSpeed;

    private ColorBlock color = new ColorBlock();

    private void Start()
    {
        currentHealth = maxHealth;
        SetMaxHealth(maxHealth);
    }
    public void UpdateUI()
    {
        Debug.Log(currentHealth);

        //Every value is coded in fraction from 0 to 1 because maxHealth is not always 100
        float fillFrontHealthBar = frontHealthBarSlider.value;
        float fillBackHealthBar = backHealthBarSlider.value;
        float healthFraction = currentHealth / maxHealth;
        
        if (fillBackHealthBar / backHealthBarSlider.maxValue > healthFraction)
        {
            frontHealthBarSlider.value = healthFraction*maxHealth;
            color.normalColor = Color.blue;
            backHealthBarSlider.colors = color;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete = percentComplete * percentComplete;
            backHealthBarSlider.value = Mathf.Lerp(fillBackHealthBar* backHealthBarSlider.maxValue, healthFraction*maxHealth, percentComplete);
        }
        if(fillBackHealthBar /  backHealthBarSlider.maxValue < healthFraction)
        {
            color.normalColor = Color.green;
            backHealthBarSlider.colors = color;
            backHealthBarSlider.value = healthFraction*maxHealth;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete = percentComplete * percentComplete;
            frontHealthBarSlider.value = Mathf.Lerp(fillFrontHealthBar*frontHealthBarSlider.maxValue, healthFraction*maxHealth, percentComplete);
        }
    }
    public void SetMaxHealth(float maxHealth)
    {
        frontHealthBarSlider.maxValue = maxHealth;
        frontHealthBarSlider.value = maxHealth;
        backHealthBarSlider.maxValue = maxHealth;
        backHealthBarSlider.value = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        lerpTimer = 0f;
        UpdateUI();
    }

    public void Heal (float heal)
    {
        currentHealth += heal;
        lerpTimer = 0f;
        UpdateUI();
    }
}
