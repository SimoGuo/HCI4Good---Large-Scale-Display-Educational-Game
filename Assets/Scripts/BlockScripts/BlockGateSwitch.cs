using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockGateSwitch : MonoBehaviour
{
    [SerializeField] GameObject door;

    public Transform pressurePlate; 
    
    public float triggerDistance = 3f;
    bool isOpened = false;


    void Update()
    {
        float distanceToPressurePlate = Vector3.Distance(transform.position, pressurePlate.position);

        if (distanceToPressurePlate < triggerDistance)
        {
            OpenGate();
        }

        else{
            CloseGate();
        }
    }

    void OpenGate(){
        if(!isOpened){
            isOpened = true;
            door.transform.position += new Vector3(0,100,0);
        }
    }

    void CloseGate(){
        if(isOpened){
            isOpened = false;
            door.transform.position += new Vector3(0,-100,0);
        }
    }
}