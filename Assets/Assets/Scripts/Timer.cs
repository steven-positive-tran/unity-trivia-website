using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] float timeToCompleteQuestion = 15f;
    [SerializeField] float timeToShowCorrectAnswer = 5f;
    
    public bool isAnswerQuestion = false;
    public bool loadNextQuestion = true;
    public float fillFraction;

    float timerValue;

    private void Update()
    {
        UpdateTimer();
    }

    void UpdateTimer()
    {
        timerValue -= Time.deltaTime;

        if(timerValue <= 0)
        {

            if (!isAnswerQuestion)
            {
                loadNextQuestion = true;
                timerValue = timeToCompleteQuestion;
            }
            else
            {
                timerValue = timeToShowCorrectAnswer;
            }

            isAnswerQuestion = !isAnswerQuestion;

        }
        else
        {
            if(isAnswerQuestion)
            {
                fillFraction = timerValue / timeToCompleteQuestion;
            }
            else
            {
                fillFraction = timerValue / timeToShowCorrectAnswer;
            }

        }
    }

    public void CancelTimer()
    {
        timerValue = 0;
    }
}
