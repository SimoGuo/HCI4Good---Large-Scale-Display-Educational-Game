using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
/*
 * This is the pressure plate script to prompt user with dialogue
 * asking if they want to transition BACK into the maze (prototype). This script
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
    }

    void OnNoButtonClick() { Cancel();}
    void OnYesButtonClick(){ ContinueToScene(); }

    // This method handles which scene is entered, in this case it is the main maze
    public void ContinueToScene()
    {
        //TODO: send message to user when click is successful, but scene is loading as there is a delay
        SceneManager.LoadScene("Prototype");
    }

    // This is to hide the dialogue and stay in the current scene
    public void Cancel()
    {
        popupPanel.SetActive(false);
    }
}
