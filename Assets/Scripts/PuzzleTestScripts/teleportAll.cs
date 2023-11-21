using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teleportAll : MonoBehaviour
{
    //grabs the game objects
    [SerializeField] GameObject player1;
    [SerializeField] GameObject player2;
    [SerializeField] GameObject camera;

    //Vectors for the telportation locations
    [SerializeField] Vector3 teleportPointChar1;
    [SerializeField] Vector3 teleportPointChar2;
    [SerializeField] Vector3 teleportPointCamera;
    
    void OnTriggerEnter(Collider col){
                player1.transform.position =  (teleportPointChar1);
                player2.transform.position = (teleportPointChar2);
                camera.transform.position = (teleportPointCamera);
    }
}
