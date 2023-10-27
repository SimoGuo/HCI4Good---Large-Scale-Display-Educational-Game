/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialComplete : MonoBehaviour
{
    
    bool hasGroundTouched = false;
    bool hasWindTouched = false;

    [SerializeField] GameObject player1;


    void Start(){

    }

    void Update(){

        if(hasGroundTouched && hasWindTouched){
                player1.transform.position += new Vector3(0,100,0);
        }
    }


    void OnTriggerEnter(Collider col){

        if(col.GameObject.tag == "Ground"){
            hasGroundTouched = true;
        }
        
        if(col.GameObject.tag == "Wind"){
            hasGroundTouched = true;
        }
    }
} */
