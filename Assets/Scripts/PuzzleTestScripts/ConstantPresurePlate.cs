using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantPresurePlate : MonoBehaviour
{
    [SerializeField] GameObject door;
    bool isOpened = false;

    void OnTriggerEnter(Collider col)
    {
        if (!isOpened)
        {
            isOpened = true;
            door.transform.position += new Vector3(0, 100, 0);
            FlipSwitch();
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (isOpened)
        {
            isOpened = false;
            door.transform.position += new Vector3(0, -100, 0);
            FlipSwitch();
        }
    }

    void FlipSwitch()
    {
        //Rotates the ConstantPresurePlate object (this object) 180 degrees horizontally, 
        //and vertically, giving it the inverted appearance in-game
        transform.Rotate(Vector3.right, 180f);
        transform.Rotate(Vector3.up, 180f);

    }
}
