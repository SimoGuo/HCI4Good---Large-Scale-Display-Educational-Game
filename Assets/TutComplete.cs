using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutComplete : MonoBehaviour
{
    bool hasGroundTouched = false;
    bool hasWindTouched = false;
    private Transform target;

    private Transform target2;

    [SerializeField] GameObject player1;
    [SerializeField] GameObject player2;
    [SerializeField] GameObject camera;


    void Start(){
        target = GameObject.FindGameObjectWithTag("Ground").transform; 
        target2= GameObject.FindGameObjectWithTag("Wind").transform;
    }

    void Update(){

        if(hasGroundTouched && hasWindTouched){
                player1.transform.position = new Vector3(105,4,-15);
                player2.transform.position = new Vector3(100,2,-15);
                camera.transform.position = new Vector3(100,60,-5);

                hasGroundTouched = false;
                hasWindTouched = false;
        }
    }


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
