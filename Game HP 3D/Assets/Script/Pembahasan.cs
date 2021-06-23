using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pembahasan : MonoBehaviour
{
    public List<GameObject> PictPembahasanUI;
    public int NumPemb;

    private QuizManagement quizmanagement;

    void Start()
    {
        quizmanagement = FindObjectOfType<QuizManagement>();
        NumPemb = quizmanagement.currentQuestions;
    }

    public void BukaUI()
    {
        NumPemb = quizmanagement.currentQuestions;
        PictPembahasanUI[NumPemb].SetActive(true);
    }

    public void TutupUI()
    {
        if (this.PictPembahasanUI.Count > 0)
        {
            FindObjectOfType<WeaponsPlayer>().forUIButton();
            PictPembahasanUI[NumPemb].SetActive(false);
            PictPembahasanUI.RemoveAt(NumPemb);
        }
    }

    public void NextSoal()
    {
        if (this.PictPembahasanUI.Count > 0)
        {
            FindObjectOfType<WeaponsPlayer>().forUIButton();
            PictPembahasanUI[NumPemb].SetActive(false);
            PictPembahasanUI.RemoveAt(NumPemb);
            FindObjectOfType<WeaponsPlayer>().openChest();
            FindObjectOfType<QuizSelect>().openChest();
        }
        else
        {
            PictPembahasanUI[NumPemb].SetActive(false);
            FindObjectOfType<WeaponsPlayer>().openChest();
            FindObjectOfType<QuizSelect>().openChest();
        }
    }

    public void DestroyObject()
    {
        Destroy(gameObject);
    }
}
