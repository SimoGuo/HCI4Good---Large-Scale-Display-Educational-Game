using System.Diagnostics;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CharacterSelection : MonoBehaviour, IPointerClickHandler
{
    public Color selectedColor = Color.green; // Assign the selected color in the Inspector

    private Color originalColor;
    private bool isSelected = false;


    public void OnPointerClick(PointerEventData eventData)
    {
        UnityEngine.Debug.Log("Character Clicked: " + gameObject.name);
        ToggleSelection();
    }

    private void ToggleSelection()
    {
        isSelected = !isSelected;

        if (isSelected)
        {
            gameManager.instance.setCurrentPlayer(gameObject); // Notify the gameManager about the selection
        }
        else
        {
            gameManager.instance.setCurrentPlayer(gameObject);
            // Revert to the original color when deselected
            //image.color = originalColor;
        }
    }
}
