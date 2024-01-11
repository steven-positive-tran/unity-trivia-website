using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class Quiz : MonoBehaviour
{
    [Header("Questions")] 
    [SerializeField] TextMeshProUGUI questionText;
    [SerializeField] List<QuestionSO> questions = new List<QuestionSO>();
    QuestionSO currentQuestion; 
   
    [Header("Answers")]
    [SerializeField] GameObject[] answerButtons;
    int correctAnswerIndex;
    bool hasAnsweredEarly;

    [Header("Button Colors")]
    [SerializeField] Sprite defaultAnswerSprite;
    [SerializeField] Sprite correctAnswerSprite;

    [Header("Timer")]
    [SerializeField] Image timerImage;
    Timer timer;

    [Header("Scoring")]
    [SerializeField] TextMeshProUGUI scoreText;
    ScoreKeeper scorekeeper;

    [Header("ProgressBar")]
    [SerializeField] Slider progressBar;

    public bool isComplete;


    // Start is called before the first frame update

    void Awake()
    {
        timer = FindObjectOfType<Timer>();
        scorekeeper = FindObjectOfType<ScoreKeeper>();
    }

    void Start()
    {
        progressBar.maxValue = questions.Count;
        progressBar.value = 0;
        isComplete = false;

        timer.loadNextQuestion = true;
        scoreText.text = "Score: 0%";
    }


    private void Update()
    {
        timerImage.fillAmount = timer.fillFraction; 
        if(timer.loadNextQuestion)
        {
            if (progressBar.value == progressBar.maxValue)
            {
                isComplete = true;
                return;
            }

            hasAnsweredEarly = false;
            GetNextQuestion();
            timer.loadNextQuestion = false;
        }
        else if (!hasAnsweredEarly && !timer.isAnswerQuestion)
        {
            DisplayAnswer(-1);
            SetButtonState(false);
        }
    }

    public void SetQuestions(List<QuestionSO> quizQuestions)
    {
        questions = quizQuestions;
    }

    private void DisplayQuestion()
    {
        questionText.text = currentQuestion.GetQuestion();

        for (int i = 0; i < answerButtons.Length; i++)
        {
            TextMeshProUGUI buttonText = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = currentQuestion.GetAnswer(i);
        }

    }

    void GetNextQuestion()
    {
        if (questions.Count > 0)
        {
            SetButtonState(true);
            SetDefaultButtonSprites();
            GetRandomQuestion();
            DisplayQuestion();
            progressBar.value++;
            scorekeeper.IncrementQuestionsSeen();
        }
    }

    private void GetRandomQuestion()
    {
        int index = UnityEngine.Random.Range(0, questions.Count); 
        currentQuestion = questions[index];

        if (questions.Contains(currentQuestion))
        {
            questions.Remove(currentQuestion);
        }       
    }

    private void SetDefaultButtonSprites()
    {
        for(int i = 0; i < answerButtons.Length; ++i)
        {
           Image buttonText = answerButtons[i].GetComponent<Image>();
           buttonText.sprite = defaultAnswerSprite;
        }
    }

    public void OnAnswerSelected(int index)
    {
        //answerButtons[index].GetComponent<AudioSource>().time = 0.05f;
        answerButtons[index].GetComponent<AudioSource>().Play();
        hasAnsweredEarly = true;
        DisplayAnswer(index);
        SetButtonState(false);
        timer.CancelTimer();
    }

    private void DisplayAnswer(int index)
    {

        if (index == currentQuestion.GetCorrectAnswerIndex())
        {
            questionText.text = "Correct!";
            Image buttonText = answerButtons[index].GetComponent<Image>();
            buttonText.sprite = correctAnswerSprite;
            scorekeeper.IncrementCorrectAnswers();
        }
        else
        {
            correctAnswerIndex = currentQuestion.GetCorrectAnswerIndex();
            questionText.text = "Incorrect, the correct answer is " + currentQuestion.GetAnswer(correctAnswerIndex);
            Image buttonText = answerButtons[correctAnswerIndex].GetComponent<Image>();
            buttonText.sprite = correctAnswerSprite;
        }

        scoreText.text = "Score: " + scorekeeper.CalculateScore() + "%";
    }

    void SetButtonState(bool state)
    {
        for(int i = 0; i < answerButtons.Length;i++)
        {
            Button button = answerButtons[i].GetComponent<Button>();
            button.interactable = state;
        }
    }


}
