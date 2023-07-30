using System.Collections.Generic;
using UnityEngine;

public class gameManager : MonoBehaviour
{
    public static gameManager instance;

    public GameObject[] players;

    public List<GameObject> currentPlayer = new List<GameObject>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {

    }

    public void setCurrentPlayer(GameObject player)
    {
        if (currentPlayer.Contains(player))
        {
            currentPlayer.Remove(player);
        }
        else
        {
            currentPlayer.Add(player);
        }
    }

    public void ClearSelectedPlayers()
    {
        currentPlayer.Clear();
    }
}
