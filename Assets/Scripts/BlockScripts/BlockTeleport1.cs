using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockTeleport1 : MonoBehaviour
{
    
    [SerializeField] GameObject cube;

    [SerializeField] Vector3 TeleportLocation;

    public Transform pressurePlate; 
    
    public float triggerDistance = 3f;

    void Update()
    {
        float distanceToPressurePlate = Vector3.Distance(transform.position, pressurePlate.position);

        if (distanceToPressurePlate < triggerDistance)
        {
            Teleport();
        }

    }

    
    void Teleport(){
        
            cube.transform.position = TeleportLocation;
        
    }
}

