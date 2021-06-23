using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TesAnswerScript : MonoBehaviour
{
    public bool isCorrect = false;
    public QuizTesManagement quizTesManagement;

    public Button button;

    private void Start()
    {
        button = GetComponent<Button>();
    }

    public void Answer()
    {
        if (isCorrect)
        {
            StartCoroutine(ResetColor());
            GetComponent<Image>().color = Color.green;
            Debug.Log("Benar");
            quizTesManagement.correct();
        }
        else
        {
            StartCoroutine(ResetColor());
            GetComponent<Image>().color = Color.green;
            Debug.Log("Salah");
            quizTesManagement.wrong();
        }
    }

    IEnumerator ResetColor()
    {
        yield return new WaitForSeconds(.2f);
        GetComponent<Image>().color = Color.white;
    }
}
