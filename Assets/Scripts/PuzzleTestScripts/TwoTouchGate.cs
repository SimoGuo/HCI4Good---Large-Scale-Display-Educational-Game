using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoTouchGate : MonoBehaviour
{
    //These booleans will keep track 
    bool hasGroundTouched = false;
    bool hasWindTouched = false;

    bool hasTriggered = false;


    //Vectors for the telportation locations
    [SerializeField] Vector3 teleportPointChar1;
    [SerializeField] Vector3 teleportPointChar2;
    [SerializeField] Vector3 teleportPointCamera;

    

    //Grabs the objects to be triggers/move
    [SerializeField] GameObject player1;
    [SerializeField] GameObject player2;
    [SerializeField] GameObject door;

    //Checks every frame if both charactes have touched
    void Update(){

        //When both characters have touched it should move the door, sets hasTriggered so it doesn't constantly update
        if(hasGroundTouched && hasWindTouched && !hasTriggered){
                
                door.transform.position += new Vector3(0,100,0);

                hasGroundTouched = false;
                hasWindTouched = false;
                hasTriggered = true;
        }
    }


    //On collision checks to see if the collider has a tag of the desired character.
    void OnTriggerEnter(Collider col){
        
        if(col.CompareTag("Ground")){
            hasGroundTouched = true;
            Debug.Log("True");
        }
        
        if(col.CompareTag("Wind")){
            hasWindTouched = true;
            Debug.Log("True");
        }
        Debug.Log("HasTouched");
    }
}
