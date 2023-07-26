using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnManager : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject[] characterPrefab; // Assign the character prefabs in the Inspector
    public float minX = -10f;
    public float maxX = 10f;
    public float minY = 0f;
    public float maxY = 5f;
    public float minZ = -10f;
    public float maxZ = 10f;

    private void Start()
    {   
        // Check if the current scene is not the character selection scene
        if (SceneManager.GetActiveScene().name != "charSelection")
        {
            // Spawn the selected character at the spawn point
            SpawnSelectedCharacter();
        }
    }

    public void OnNextButtonClick()
    {
        // Get the list of selected characters from the gameManager
        List<GameObject> selectedPlayers = gameManager.instance.currentPlayer;

        // Check if there are any selected characters
        if (selectedPlayers.Count > 0)
        {
            // Load the game scene and pass the list of selected character names as parameters
            List<string> selectedCharacterNames = new List<string>();
            foreach (GameObject player in selectedPlayers)
            {
                selectedCharacterNames.Add(player.name);
            }
            PlayerPrefs.SetString("SelectedCharacterNames", string.Join(",", selectedCharacterNames));
            SceneManager.LoadScene("gameDemo", LoadSceneMode.Single);
        }
        else
        {
            UnityEngine.Debug.LogError("No character selected. Please select a character before pressing 'Next'.");
        }
    }

    /*    public void OnNextButtonClick()
        {
            List<GameObject> selectedPlayers = gameManager.instance.selectedPlayers;

            // Get the selected character name from the gameManager
            string selectedCharacterName = gameManager.instance.currentPlayer.name;

            // Find the index of the selected character in the gameManager array
            int selectedIndex = -1;
            for (int i = 0; i < gameManager.instance.players.Length; i++)
            {
                if (gameManager.instance.players[i].name == selectedCharacterName)
                {
                    selectedIndex = i;
                    break;
                }
            }

            // Load the game scene and pass the selected character's index as a parameter
            if (selectedIndex != -1)
            {
                PlayerPrefs.SetInt("SelectedCharacterIndex", selectedIndex);
                SceneManager.LoadScene("gameDemo", LoadSceneMode.Single);
            }
            else
            {
                UnityEngine.Debug.LogError("Selected character not found in the gameManager array!");
            }
        }*/

    /*    private void SpawnSelectedCharacter()
        {
            UnityEngine.Debug.Log("spawn Selected ethod called");
            // Get the selected character's index from the PlayerPrefs
            int selectedIndex = PlayerPrefs.GetInt("SelectedCharacterIndex", 0);

            // Check if the index is valid
            if (selectedIndex >= 0 && selectedIndex < characterPrefab.Length)
            {
                // Instantiate the selected character prefab at the spawn point
                Instantiate(characterPrefab[selectedIndex], spawnPoint.position, Quaternion.identity);
            }
            else
            {
                UnityEngine.Debug.LogError("Invalid selected character index!");
            }
        }*/

    private void SpawnSelectedCharacter()
    {
        UnityEngine.Debug.Log("SpawnSelectedCharacters method called");
        // Get the list of selected characters from the PlayerPrefs
        string[] selectedCharacterNames = PlayerPrefs.GetString("SelectedCharacterNames", "").Split(',');

        // Check if there are any selected characters
        if (selectedCharacterNames.Length > 0)
        {
            // Iterate through the list of selected character names
            for (int i = 0; i < selectedCharacterNames.Length; i++)
            {
                string selectedCharacterName = selectedCharacterNames[i];

                // Find the index of the selected character prefab in the characterPrefabs array
                int prefabIndex = -1;
                for (int j = 0; j < characterPrefab.Length; j++)
                {
                    if (characterPrefab[j].name == selectedCharacterName)
                    {
                        prefabIndex = j;
                        break;
                    }
                }

                // Check if the index is valid
                if (prefabIndex != -1)
                {
                    float randomX = UnityEngine.Random.Range(minX, maxX);
                    float randomY = UnityEngine.Random.Range(minY, maxY);
                    float randomZ = UnityEngine.Random.Range(minZ, maxZ);
                    Vector3 spawnPosition = new Vector3(randomX, randomY, randomZ);
                    // Get a random spawn point index
                    /*int spawnPointIndex = Random.Range(0, spawnPoints.Length);*/

                    // Instantiate the selected character prefab at the random spawn point
                    Instantiate(characterPrefab[prefabIndex], spawnPosition, Quaternion.identity);
                }
                else
                {
                    UnityEngine.Debug.LogError("Selected character prefab not found for character: " + selectedCharacterName);
                }
            }
        }
        else
        {
            UnityEngine.Debug.LogError("No selected characters found in PlayerPrefs.");
        }
    }
}
