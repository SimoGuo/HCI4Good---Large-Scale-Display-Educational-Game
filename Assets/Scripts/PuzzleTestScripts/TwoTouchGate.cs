using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoTouchGate : MonoBehaviour
{
    bool hasGroundTouched = false;
    bool hasWindTouched = false;
    bool hasTriggered = false;
    private Transform target;

    private Transform target2;

    [SerializeField] GameObject player1;
    [SerializeField] GameObject player2;
    [SerializeField] GameObject door;


    void Start(){
        target = GameObject.FindGameObjectWithTag("Ground").transform; 
        target2= GameObject.FindGameObjectWithTag("Wind").transform;
    }

    void Update(){

        if(hasGroundTouched && hasWindTouched && !hasTriggered){
                
                door.transform.position += new Vector3(0,100,0);

                hasGroundTouched = false;
                hasWindTouched = false;
                hasTriggered = true;
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
