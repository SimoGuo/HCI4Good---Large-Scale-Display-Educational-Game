using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockTeleport : MonoBehaviour
{

    [SerializeField] GameObject cube;

    [SerializeField] Vector3 TeleportLocation;
    
    
    // Start is called before the first frame update
    void OnTriggerEnter(Collider col){
        
        if(col.CompareTag("Cube")){
            cube.transform.position = TeleportLocation;
        }
        
    }
}
