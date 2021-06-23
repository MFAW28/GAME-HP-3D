using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuizTesManagement : MonoBehaviour
{
    public List<QnATes> QnA;
    public GameObject[] options;
    public int currentQuestions;
    public Text scoreSoalText;
    public Text TimerText;

    public Image questionsImage;

    private TesManagement tesManagement;
    private TimerController timerController;

    private void Start()
    {
        tesManagement = GetComponent<TesManagement>();
        timerController = GetComponent<TimerController>();
        generateQuestions();
    }

    private void Update()
    {
        scoreSoalText.text = "" + QnA[currentQuestions].ScoreTes;
        TimerText.text = timerController.time.ToString(@"mm\:ss");

        if(timerController.currentTime <= 0)
        {
            tesManagement.EndTest();
        }
    }

    public void correct()
    {
        FindObjectOfType<AudioManager>().Play("Button");
        tesManagement.score += QnA[currentQuestions].ScoreTes;
        QnA.RemoveAt(currentQuestions);
        generateQuestions();
    }

    public void wrong()
    {
        FindObjectOfType<AudioManager>().Play("Button");
        QnA.RemoveAt(currentQuestions);
        generateQuestions();
    }

    public void refreshQuestion()
    {
        FindObjectOfType<AudioManager>().Play("Button");
        generateQuestions();
    }

    void generateQuestions()
    {
        if (QnA.Count > 0)
        {
            currentQuestions = Random.Range(0, QnA.Count);

            questionsImage.sprite = QnA[currentQuestions].QuestionsImageTes;
            SetAnswer();
        }
        else
        {
            timerController.StopTimer();
            tesManagement.EndTest();
        }
    }

    void SetAnswer()
    {
        for (int i = 0; i < options.Length; i++)
        {
            options[i].GetComponent<TesAnswerScript>().isCorrect = false;
            options[i].transform.GetChild(0).GetComponent<Image>().sprite = QnA[currentQuestions].AnswerTextTes[i];

            if (QnA[currentQuestions].CorrectAnswerTes == i + 1)
            {
                options[i].GetComponent<TesAnswerScript>().isCorrect = true;
            }
        }
    }
}

