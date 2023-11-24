using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{

    //grabs the two objects to use as doors
    [SerializeField] GameObject door;
    [SerializeField] GameObject secondDoor;
    bool isTriggered = false;


    //When a collider interacts with this switch it will change the scene, one door will appear while the other disapears
    //uses a boolean to keep track of which state the objects are in.
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