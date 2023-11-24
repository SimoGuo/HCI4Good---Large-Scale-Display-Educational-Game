using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{

    [SerializeField] GameObject door;
    bool isOpened = false;

    //When a collider enters it will grab the door and move it upwards out of the way
    void OnTriggerEnter(Collider col){
        if(!isOpened){
            isOpened = true;
            door.transform.position += new Vector3(0,100,0);
        }
        
    }
    
}
