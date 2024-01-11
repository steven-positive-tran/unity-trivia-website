using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] Canvas menu;
    [SerializeField] Quiz quiz;
    [SerializeField] WinScreen winScreen;
    [SerializeField] QuestionSO[] allPossibleQuestions;
    [SerializeField] GameObject BGM;

    void Start()
    {
        menu.gameObject.SetActive(true);
        quiz.gameObject.SetActive(false);
        winScreen.gameObject.SetActive(false);
    }
    void Update()
    {
        if (quiz.isComplete)
        {
            quiz.gameObject.SetActive(false);
            winScreen.gameObject.SetActive(true);
            winScreen.ShowFinalScore();
        }
        
    }
    public void EasyQuiz()
    {
        
        List<QuestionSO> createdQuiz = GetListOfQuestions(5);
        quiz.SetQuestions(createdQuiz);
        StartQuiz();
    }

    public void MediumQuiz()
    {
        List<QuestionSO> createdQuiz = GetListOfQuestions(10);
        quiz.SetQuestions(createdQuiz);
        StartQuiz();
    }

    public void HardQuiz()
    {
        List<QuestionSO> createdQuiz = GetListOfQuestions(15);
        quiz.SetQuestions(createdQuiz);
        StartQuiz();
    }

    public void OnReplayLevel()
    {
        gameObject.GetComponent<AudioSource>().Play();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public List<QuestionSO> GetListOfQuestions(int numberofQuestions)
    {
        List <QuestionSO> quiz = new List<QuestionSO>();
        List <int> indexArray = new List<int>();
        int random;

        for (int i = 0; i < numberofQuestions; i++)
        {   
            do
            {
                random = Random.Range(0, allPossibleQuestions.Length);
            }
            while(indexArray.Contains(random));

            indexArray.Add(random);
            quiz.Add(allPossibleQuestions[random]);
        }

        return quiz;
    }

    private void StartQuiz()
    {
        gameObject.GetComponent<AudioSource>().Play();
        menu.gameObject.SetActive(false);
        quiz.gameObject.SetActive(true);
        BGM.GetComponent<AudioSource>().Play();

    }
}
