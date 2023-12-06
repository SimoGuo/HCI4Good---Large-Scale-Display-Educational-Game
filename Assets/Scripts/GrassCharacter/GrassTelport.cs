using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassTelport : MonoBehaviour
{

    [SerializeField] GameObject grassPlayer;

    [SerializeField] Vector3 TeleportLocation;
    
    // Start is called before the first frame update
    void OnTriggerEnter(Collider col){
        
        if(col.CompareTag("Grass")){
            grassPlayer.transform.position = TeleportLocation;
        }
        
    }
}
