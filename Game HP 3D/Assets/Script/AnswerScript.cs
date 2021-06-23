using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnswerScript : MonoBehaviour
{
    public bool isCorrect = false;
    public QuizManagement quizManagement;

    public Color startColor;
    public Button button;

    private void Start()
    {
        startColor = GetComponent<Image>().color;
        button = GetComponent<Button>();
    }

    public void Answer()
    {
        if (isCorrect)
        {
            StartCoroutine(ResetColor());
            GetComponent<Image>().color = Color.green;
            Debug.Log("Benar");
            quizManagement.correct();
        }
        else
        {
            StartCoroutine(ResetColor());
            GetComponent<Image>().color = Color.red;
            Debug.Log("Salah");
            quizManagement.wrong();
        }
    }

    IEnumerator ResetColor()
    {
        yield return new WaitForSeconds(.4f);
        GetComponent<Image>().color = startColor;
    }
}
