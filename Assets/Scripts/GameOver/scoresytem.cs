using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreSystem : MonoBehaviour
{
   public int score = 0;
    

   public TMPro.TextMeshProUGUI scoreText;

    // Method to increase the score

    public void IncreaseScore(int points)
    {
        score += points;

        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = score.ToString();
        }
    }

}
