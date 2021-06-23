using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PraAnswerScript : MonoBehaviour
{
    public bool isCorrect = false;
    public QuizSyaratManagement quizSyaratManagement;

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
            GetComponent<Image>().color = Color.green;
            Debug.Log("Benar");
            quizSyaratManagement.correct();
            StartCoroutine(ResetColor());
        }
        else
        {
            GetComponent<Image>().color = Color.red;
            Debug.Log("Salah");
            quizSyaratManagement.wrong();
            StartCoroutine(ResetColor());
        }
    }

    IEnumerator ResetColor()
    {
        yield return new WaitForSeconds(.5f);
        GetComponent<Image>().color = startColor;
    }
}
