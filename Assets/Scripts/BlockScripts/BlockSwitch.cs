using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSwitch : MonoBehaviour
{
    [SerializeField] GameObject door;
    bool isOpened = false;

    void OnTriggerEnter(Collider col){
        if(!isOpened && col.CompareTag("Cube")){
            isOpened = true;
            door.transform.position += new Vector3(0,100,0);
        }
    }

    void OnTriggerExit(Collider col){
        if(isOpened && col.CompareTag("Cube")){
            isOpened = false;
            door.transform.position += new Vector3(0,-100,0);
        }
    }
}
