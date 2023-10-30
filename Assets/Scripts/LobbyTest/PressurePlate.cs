using UnityEngine;
using UnityEngine.SceneManagement;

public class PressurePlate : MonoBehaviour
{
    private bool isActivated = false;
    public GameObject popupPanel; 
    
    void Start()
    {
        
        popupPanel.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!isActivated)
            {
                popupPanel.SetActive(true);
            }
        }
    }

    [ContextMenu("ContinueToScene")]
    public void ContinueToScene()
    {
        SceneManager.LoadScene("Prototype");
    }

    [ContextMenu("Cancel")]
    public void Cancel()
    {
        popupPanel.SetActive(false);
    }
}
