using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuizSyaratManagement : MonoBehaviour
{
    public QnAPrasyarat1 QnAsyarat;
    public GameObject[] options;
    public int currentQuestions;

    public Image questionsImage;
    public GameObject quizUI;
    public GameObject quizBenar;
    public GameObject quizSalah;

    private PrasyaratManagement prasyaratManagement;

    private void Start()
    {
        prasyaratManagement = GetComponent<PrasyaratManagement>();
        FindQuizSoal();
        generateQuestions();
    }

    public void FindQuizSoal()
    {
        QnAsyarat = FindObjectOfType<QnAPrasyarat1>();
    }

    public void correct()
    {
        FindObjectOfType<AudioManager>().Play("Button");
        StartCoroutine(correctAnswer());
        prasyaratManagement.score += 1;
        QnAsyarat.QnA.RemoveAt(currentQuestions);
    }

    public void wrong()
    {
        FindObjectOfType<AudioManager>().Play("Button");
        StartCoroutine(wrongAnswer());
        QnAsyarat.QnA.RemoveAt(currentQuestions);
    }

    public void refreshQuestion()
    {
        FindObjectOfType<AudioManager>().Play("Button");
        generateQuestions();
    }

    void generateQuestions()
    {
        if (QnAsyarat.QnA.Count > 0)
        {
            currentQuestions = Random.Range(0, QnAsyarat.QnA.Count);

            questionsImage.sprite = QnAsyarat.QnA[currentQuestions].QuestionsImagePrasyarat;
            SetAnswer();
        }
        else
        {
            prasyaratManagement.EndTest();
            QnAsyarat.DestroyObject();
        }
    }

    void SetAnswer()
    {
        for (int i = 0; i < options.Length; i++)
        {
            options[i].GetComponent<PraAnswerScript>().isCorrect = false;
            options[i].transform.GetChild(0).GetComponent<Text>().text = "" + QnAsyarat.QnA[currentQuestions].AnswerText[i];

            if (QnAsyarat.QnA[currentQuestions].CorrectAnswerPrasyarat == i + 1)
            {
                options[i].GetComponent<PraAnswerScript>().isCorrect = true;
            }
        }
    }

    IEnumerator correctAnswer()
    {
        GameObject obj = Instantiate(quizBenar, new Vector3(0, 0, 0), Quaternion.identity);
        obj.transform.SetParent(quizUI.transform, false);
        Destroy(obj, .6f);
        yield return new WaitForSeconds(.6f);
        generateQuestions();
    }

    IEnumerator wrongAnswer()
    {
        GameObject obj = Instantiate(quizSalah, new Vector3(0, 0, 0), Quaternion.identity);
        obj.transform.SetParent(quizUI.transform, false);
        Destroy(obj, .6f);
        yield return new WaitForSeconds(.6f);
        generateQuestions();
    }

    IEnumerator GameOver()
    {
        yield return new WaitForSeconds(.5f);
    }
}
