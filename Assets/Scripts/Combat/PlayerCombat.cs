using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public List<AttackTemplate> combo;
    float lastClickedTime;
    float lastComboEnd;
    public int comboCounter;

    // Animation

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        testController = GetComponent<TestController>();
        characterScript = GetComponent<character>();
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the character is selected before allowing combat input
    }

    void Attack()
    {
        // Delay time between each consecutive combo and make sure the combo counter is always less than the combo List size to avoid NullPointer
            {
                animator.runtimeAnimatorController = combo[comboCounter].animatorOveride;
                animator.Play("Attack", 0, 0);

                // Hard coded damage value for now
                //Replace this code with other damage mechanics

                comboCounter++;
                lastComboEnd = Time.time;

                if (comboCounter > combo.Count)
                {
                    comboCounter = 0;
                }
            }
        }
    }

    void ExitAttack()
    {
        // Check if the animation currently playing is an "Attack" animation and it has finished at least 90%
        }
    }

    void EndAttack()
    {

    void EndCombo()
    {
        comboCounter = 0;
        lastComboEnd = Time.time;
    }
}
