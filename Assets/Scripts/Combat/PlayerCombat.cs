using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public List<AttackTemplate> combo;
    float lastClickedTime;
    float lastComboEnd;
    public int comboCounter;

    //Animation
    Animator animator;

    [SerializeField]
    Weapon weapon;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            Attack();
        }
        ExitAttack();
    }

    void Attack()
    {
        //Delay time between each consecutive combo and make sure the combo counter always less than the combo List size to avoid NullPointer
        if (Time.time - lastComboEnd > 0.2f && comboCounter <= combo.Count)
        {
            //Make sure combo cannot end mid-combo
            CancelInvoke("EndCombo");

            //Delay time between each player input to avoid overlapping 
            if(Time.time - lastClickedTime >= 0.2f)
            {
                animator.runtimeAnimatorController = combo[comboCounter].animatorOveride;
                animator.Play("Attack", 0, 0);

                //Replace this code with other damage mechanics
                weapon.damage = combo[comboCounter].damage;


                comboCounter++;
                lastComboEnd = Time.time;

                if(comboCounter > combo.Count)
                {
                    comboCounter = 0;
                }
            }
        }
    }

    void ExitAttack()
    {
        //Check if the animation currently playing is an "Attack" animation and it has finished at least 90%
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9f && animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            //End the combo after a certain amount of time has passed with no input
            Invoke("EndCombo",1);
        }
    }

    void EndAttack()
    {
        
    }

    //Call this function when the combo ends, add in any effect of the combo ending here
    void EndCombo()
    {
        comboCounter = 0;
        lastComboEnd = Time.time;
    }
}
