using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
/*
 * This is the pressure plate script to prompt user with dialogue
 * asking if they want to transition BACK into the maze (prototype and Puzzle1Test). This script
 * does not handle leaving the maze, only re-entering.
 *
 * @author: Simo Guo with support and documentation from Madeleine Nykl
 */
public class LevelComplete : MonoBehaviour
{
    private bool isActivated = false;
    public GameObject popupPanel;
    public Button noButton;
    public Button yesButton;

    // Add a variable to store the destination scene
    private string destinationScene;

    void Start()
    {
        popupPanel.SetActive(false);
        noButton.onClick.AddListener(OnNoButtonClick);
        yesButton.onClick.AddListener(OnYesButtonClick);
    }

    // This checks if the character has made contact with the "door" pressure plate
    private void OnTriggerEnter(Collider other)
    {
        if ((other.CompareTag("Ground") || other.CompareTag("Wind")) && !isActivated)
        {
            popupPanel.SetActive(true);
        }

        GameObject door = this.gameObject;
        // Check the tag of the door and set the destination scene accordingly
        if (door.CompareTag("Door1"))
        {
            destinationScene = "Prototype";
        }
        else if (door.CompareTag("Door2"))
        {
            destinationScene = "Puzzle1Test";
        }
        // The comment-out code is for future use
        // else if (door.CompareTag("Door3"))
        // {
        //     destinationScene = "Puzzle2Test";
        // }
        // else if (door.CompareTag("Door4"))
        // {
        //     destinationScene = "Scene4";
        // }
        }
    

    // This method handles which scene is entered
    public void ContinueToScene(string sceneName)
    {
        // TODO: send message to the user when click is successful
        SceneManager.LoadScene(sceneName);
    }

    // This is to hide the dialogue and stay in the current scene
    public void Cancel()
    {
        popupPanel.SetActive(false);
    }

    void OnNoButtonClick() { Cancel(); }
    void OnYesButtonClick() { ContinueToScene(destinationScene); }
}