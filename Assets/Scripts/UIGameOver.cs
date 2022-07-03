using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIGameOver : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;

    [SerializeField] ScoreKeeper scoreKeeper;

    void Awake()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }

    void Start()
    {
        DisplayFinalScore();
    }

    private void DisplayFinalScore()
    {
        scoreText.text = "You Scored:\n" + scoreKeeper.GetScore();
    }
}
