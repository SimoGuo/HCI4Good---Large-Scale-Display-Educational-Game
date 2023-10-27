using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantPresurePlate : MonoBehaviour
{
    [SerializeField] GameObject door;
    bool isOpened = false;

    //Whenever an object collides with the pressure plate move the desired object away
    void OnTriggerEnter(Collider col){
        if(!isOpened){
            isOpened = true;
            door.transform.position += new Vector3(0,100,0);
        }
    }

    //Whenever the player leaves the pressure plate return the object to its original location
    void OnTriggerExit(Collider col){
        if(isOpened){
            isOpened = false;
            door.transform.position += new Vector3(0,-100,0);
        }
    }
}
