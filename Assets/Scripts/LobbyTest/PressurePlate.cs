using UnityEngine;
using UnityEngine.SceneManagement;

public class PressurePlate : MonoBehaviour
{
    private bool isActivated = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!isActivated)
            {
                SceneManager.LoadScene("Prototype");
                isActivated = true; 
            }
        }
    }
}
