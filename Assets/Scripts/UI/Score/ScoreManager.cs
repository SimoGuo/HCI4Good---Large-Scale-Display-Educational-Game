using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour {
    private int score;
    [SerializeField] private TextMeshPro scoreText;

    private void Start() {
        scoreText = GetComponent<TextMeshPro>();
    }

    public void EnemyDied() {
        score += 100;
        scoreText.text = score + "";
    }
}