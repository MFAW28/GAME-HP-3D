using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QnAStudy : MonoBehaviour
{

    public List<QuizAndAnswer> QnA;
    public GameObject[] options;
    public int currentQuestions;

    public void DestroyObject()
    {
        Destroy(gameObject);
    }
}
