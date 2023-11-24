/**
* @author: Simo Guo
* This class is to activate pressure plate when the large cube
* is above the pressure plate.
*/
using UnityEngine;

public class CubeController : MonoBehaviour
{
    public Transform pressurePlate;
    public float triggerDistance = 2.5f; 

    void Update()
    {
        float distanceToPressurePlate = Vector3.Distance(transform.position, pressurePlate.position);

        if (distanceToPressurePlate < triggerDistance)
        {
            Debug.Log("Cube triggers pressure plate");
        }
    }
}

