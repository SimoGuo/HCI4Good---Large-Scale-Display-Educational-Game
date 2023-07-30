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
        else if (Input.GetKeyUp(KeyCode.S))
        {
            healthBar.IncreaseHealth(10);
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            manaBar.DecreaseMana(10);
        }
        else if (Input.GetKeyUp(KeyCode.D))
        {
            manaBar.IncreaseMana(10);
        }
    }
}
