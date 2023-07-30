using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaBar : MonoBehaviour
{
    private float mana;
    [SerializeField] private float maxMana;

    //Control the delay before the backManaBar start to follow the frontManaBar
    private float lerpTimer;

    //Controle the speed of the backManaBar
    [SerializeField] private float delaySpeed;

    public Image frontManaBar;
    public Image backManaBar;



    // Start is called before the first frame update
    void Start()
    {
        mana = maxMana;
    }

    // Update is called once per frame
    void Update()
    {
        //Make sure the current health cannot exceed maxMana and cannot go below 0
        if (mana > maxMana)
        {
            mana = maxMana;
        }
        if (mana < 0)
        {
            mana = 0;
        }
        UpdateStatusBar();
    }

    void UpdateStatusBar()
    {

        //Local variables to keep track of the back/frontManaBar's fill value (These 2 values range from 0 to 1, basically percentage)
        float fillFront = frontManaBar.fillAmount;
        float fillBack = backManaBar.fillAmount;

        //Compare current mana and max mana to determine if the mana value has changed
        float manaFraction = mana / maxMana;

        if (fillBack > manaFraction)
        {
            //Update the frontManaBar to the new mana value (Had to turn to percentage before assigning)
            frontManaBar.fillAmount = manaFraction;
            backManaBar.color = Color.white;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / delaySpeed;
            //Make the transformation start of slow and the speed up
            percentComplete = percentComplete * percentComplete;
            backManaBar.fillAmount = Mathf.Lerp(fillBack, manaFraction, percentComplete);
        }
        if (fillFront < manaFraction)
        {
            //Reverse the above code block so that the frontManaBar follow the backManaBar
            backManaBar.fillAmount = manaFraction;
            backManaBar.color = Color.green;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / delaySpeed;
            //Make the transformation start of slow and the speed up
            percentComplete = percentComplete * percentComplete;
            frontManaBar.fillAmount = Mathf.Lerp(fillFront, manaFraction, percentComplete);
        }
    }

    public void DecreaseMana(float decreaseAmount)
    {
        mana -= decreaseAmount;
        //reset lerpTimer
        lerpTimer = 0f;
    }

    public void IncreaseMana(float increaseAmount)
    {
        mana += increaseAmount;
        //reset lerpTimer
        lerpTimer = 0f;
    }

    public void SetMaxMana(float maxMana)
    {
        this.maxMana = maxMana;
        mana = maxMana;
    }
}
