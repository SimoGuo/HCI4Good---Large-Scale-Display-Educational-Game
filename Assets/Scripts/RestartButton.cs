using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartButton : MonoBehaviour
{
    private void OnMouseDown()
    {
        Restart();
    }

    private void Restart()
    {
        SceneManager.LoadScene("lobby");
    }
}
