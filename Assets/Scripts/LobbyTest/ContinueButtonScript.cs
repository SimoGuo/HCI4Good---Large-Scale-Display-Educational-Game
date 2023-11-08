using UnityEngine;
using UnityEngine.SceneManagement;

public class ContinueButtonScript : MonoBehaviour
{
    public void OnContinueButtonClick()
    {
        SceneManager.LoadScene("Prototype");
    }
}
