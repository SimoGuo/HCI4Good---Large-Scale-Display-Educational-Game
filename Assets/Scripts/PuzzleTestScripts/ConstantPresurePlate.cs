using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantPresurePlate : MonoBehaviour
{
    [SerializeField] GameObject door;
    bool isOpened = false;

    void OnTriggerEnter(Collider col){
        if(!isOpened){
            isOpened = true;
            door.transform.position += new Vector3(0,100,0);
        }
    }

    void OnTriggerExit(Collider col){
        if(isOpened){
            isOpened = false;
            door.transform.position += new Vector3(0,-100,0);
        }
    }
}
