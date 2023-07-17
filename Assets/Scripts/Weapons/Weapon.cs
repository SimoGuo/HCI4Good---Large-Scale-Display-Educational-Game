using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float damage;

    BoxCollider triggerBox;

    // Start is called before the first frame update
    void Start()
    {
        triggerBox = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        var enemy = other.gameObject.GetComponent<EnemiesBehaviour>();
        if (enemy != null)
        {
            enemy.HP -= damage;

            if(enemy.HP <= 0)
            {
                Destroy(enemy.gameObject);
            }

        }
    }

    public void EnableTriggerBox()
    {
        triggerBox.enabled = true;
    }

    public void DisableTriggerBox()
    {
        triggerBox.enabled = false;
    }
}
