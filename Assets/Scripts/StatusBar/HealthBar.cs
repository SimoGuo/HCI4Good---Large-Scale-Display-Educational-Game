using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private float health;
    [SerializeField] private float maxHealth;

    //Control the delay before the backHealthBar start to follow the frontHealthBar
    private float lerpTimer;

    //Controle the speed of the backHealthBar
    [SerializeField] private float delaySpeed;

    public Image frontHealthBar;
    public Image backHealthBar;



    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        //Make sure the current health cannot exceed maxHealth and cannot go below 0
        if (health > maxHealth)
        {
            health = maxHealth;
        }
        if (health < 0)
        {
            health = 0;
        }
        UpdateStatusBar();
    }

    void UpdateStatusBar()
    {

        //Local variables to keep track of the back/frontHealthbar's fill value (These 2 values range from 0 to 1, basically percentage)
        float fillFront = frontHealthBar.fillAmount;
        float fillBack = backHealthBar.fillAmount;

        //Compare current health and max health to determine if the health value has changed
        float healthFraction = health / maxHealth;

        if (fillBack > healthFraction)
        {
            //Update the frontHealthbar to the new health value (Had to turn to percentage before assigning)
            frontHealthBar.fillAmount = healthFraction;
            backHealthBar.color = Color.white;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / delaySpeed;
            //Make the transformation start of slow and the speed up
            percentComplete = percentComplete * percentComplete;
            backHealthBar.fillAmount = Mathf.Lerp(fillBack, healthFraction, percentComplete);
        }
        if (fillFront < healthFraction)
        {
            //Reverse the above code block so that the frontHealthbar follow the backHealthbar
            backHealthBar.fillAmount = healthFraction;
            backHealthBar.color = Color.green;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / delaySpeed;
            //Make the transformation start of slow and the speed up
            percentComplete = percentComplete * percentComplete;
            frontHealthBar.fillAmount = Mathf.Lerp(fillFront, healthFraction, percentComplete);
        }
    }

    public void DecreaseHealth(float damage)
    {
        health -= damage;
        //reset lerpTimer
        lerpTimer = 0f;
    }

    public void IncreaseHealth(float healAmount)
    {
        health += healAmount;
        //reset lerpTimer
        lerpTimer = 0f;
    }

    public void SetMaxHealth(float maxHealth)
    {
        this.maxHealth = maxHealth;
        health = maxHealth;
    }
}
