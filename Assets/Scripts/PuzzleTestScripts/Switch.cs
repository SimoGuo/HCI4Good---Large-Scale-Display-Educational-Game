using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{

    [SerializeField] GameObject door;
    [SerializeField] GameObject secondDoor;
    bool isTriggered = false;

    void OnTriggerEnter(Collider col){
        if(!isTriggered){
            isTriggered = true;
            door.transform.position += new Vector3(0,100,0);
            secondDoor.transform.position += new Vector3(0,100,0);
        }
        
        else{
            isTriggered = false;
            door.transform.position -= new Vector3(0,100,0);
            secondDoor.transform.position -= new Vector3(0,100,0);
        }
    }
    
}