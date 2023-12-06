using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevelGrassMetal : MonoBehaviour
{

    //These booleans will keep track 
    bool hasMetalTouched = false;
    bool hasGrassTouched = false;

    //grabs the game objects
    [SerializeField] GameObject player1;
    [SerializeField] GameObject player2;
    [SerializeField] GameObject camera;

    //Vectors for the telportation locations
    [SerializeField] Vector3 teleportPointChar1;
    [SerializeField] Vector3 teleportPointChar2;
    [SerializeField] Vector3 teleportPointCamera;


    

    //Every frame check to see if both characters have touched the object, if do move them to desired area
    void Update(){
        
        //Moves characters, then resets the flags so they aren't repeatadly teleported
        if(hasGrassTouched && hasMetalTouched){
                player1.transform.position =  (teleportPointChar1);
                player2.transform.position = (teleportPointChar2);
                camera.transform.position = (teleportPointCamera);

                hasGrassTouched = false;
                hasMetalTouched = false;
        }
    }


    //On collision checks to see if the collider has a tag of the desired character.
    void OnTriggerEnter(Collider col){
        
        if(col.CompareTag("Metal")){
            hasMetalTouched = true;
            Debug.Log("True");
        }
        
        if(col.CompareTag("Grass")){
            hasGrassTouched = true;
            Debug.Log("True");
        }
        Debug.Log("HasTouched");
    }
}
