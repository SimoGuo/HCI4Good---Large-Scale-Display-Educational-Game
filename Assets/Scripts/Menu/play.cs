using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class play : MonoBehaviour
{
    private float startTime;

    public void loadCharacters()
    {
        startTime = Time.time;
        PlayerPrefs.SetFloat("startTime", startTime);
        SceneManager.LoadScene("Integration");
    }

}

