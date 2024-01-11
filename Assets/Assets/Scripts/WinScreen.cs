using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WinScreen : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI finalScoreText;
    ScoreKeeper scoreKeeper;

    void Awake()
    {
        scoreKeeper = FindAnyObjectByType<ScoreKeeper>();
        
    }


    public void ShowFinalScore()
    {
        finalScoreText.text = $"Congratulations!\nYour Score is {scoreKeeper.CalculateScore()}%";

    }
}
