using System;
using System.Collections;
using UnityEngine;
using TMPro;

public class gameover : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timeText;
    private int countScore = 17; // This is demo. After the game finishes, this value will be updated.
    private int jumpCount = 0;

    private float endTime;
    private float startTime;
    private float totalTime;


    private void OnEnable() {
        countScore = PlayerPrefs.GetInt("Score");
    }

    void Start()
    {
        endTime = Time.time;
        StartCoroutine(JumpObjectRoutine());
    }

    void JumpObject()
    {
        if (jumpCount < 6)
        {
            StartCoroutine(JumpRoutine());
            jumpCount++;
        }
    }

    private string FormatTime(float timeInSeconds)
    {
        int minutes = Mathf.FloorToInt(timeInSeconds / 60);
        int seconds = Mathf.FloorToInt(timeInSeconds % 60);

        // Convert the values to strings and add leading zeros if needed
        string formattedMinutes = minutes.ToString("00");
        string formattedSeconds = seconds.ToString("00");

        return formattedMinutes + ":" + formattedSeconds;
    }

    IEnumerator JumpRoutine()
    {
        float originalY = transform.position.y;
        float jumpHeight = 50f;
        float jumpDuration = 0.25f;

        for (int i = 0; i < 5; i++)
        {
            Vector3 jumpPos = new Vector3(transform.position.x, originalY + jumpHeight, transform.position.z);
            transform.position = jumpPos;
            yield return new WaitForSeconds(jumpDuration);

            transform.position = new Vector3(transform.position.x, originalY, transform.position.z);
            yield return new WaitForSeconds(jumpDuration);
        }

        startTime = PlayerPrefs.GetFloat("startTime");
        totalTime = Mathf.RoundToInt(endTime - startTime);
        Debug.Log("Total Time: " + totalTime);
        string formattedTime = FormatTime(totalTime);
        timeText.text = formattedTime;
        StartCoroutine(IncrementScore(countScore));
    }

    IEnumerator IncrementScore(int targetScore)
    {
        int currentScore = 0;
        int incrementStep = 1;
        float incrementDelay = 0.001f;

        while (currentScore < targetScore)
        {
            currentScore += incrementStep;
            scoreText.text = currentScore.ToString();
            yield return new WaitForSeconds(incrementDelay);
        }

        // Ensure the final score matches the target score
        scoreText.text = targetScore.ToString();
    }

    IEnumerator JumpObjectRoutine()
    {
        yield return new WaitForEndOfFrame();
        JumpObject();
    }
}

