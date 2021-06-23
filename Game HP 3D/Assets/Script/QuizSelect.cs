using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuizSelect : MonoBehaviour
{
    [SerializeField] private GameObject quizUI;

    [SerializeField] private GameObject quizBenar;
    [SerializeField] private GameObject quizSalah;

    [SerializeField] private Text textTimerQuiz;

    private bool timeToAnswer;
    private float timerAnswer;
    public float maxTimerAnswer = 120;

    private QuizManagement quizQuestions;
    private GameManagement gameManagement;
    private PlayerAttack attackPlayerScript;
    private ScoreController scoreGame;
    private playerhealth healthPlayer;

    
    void Start()
    {
        quizQuestions = FindObjectOfType<QuizManagement>();
        gameManagement = FindObjectOfType<GameManagement>();
        attackPlayerScript = this.GetComponent<PlayerAttack>();
        healthPlayer = this.GetComponent<playerhealth>();
        scoreGame = FindObjectOfType<ScoreController>();
        quizUI.SetActive(false);

        timeToAnswer = false;
        timerAnswer = maxTimerAnswer;
    }

    // Update is called once per frame
    void Update()
    {
        if(timeToAnswer == true && timerAnswer > 0)
        {
            timerAnswer -= Time.deltaTime;
        }
        if (timeToAnswer == true && timerAnswer <= 0)
        {
            GetComponent<WeaponsPlayer>().closeChest();
        }
        int timerQuiz = (int)Mathf.Round(timerAnswer);
        textTimerQuiz.text = "" + timerQuiz;
    }

    public void openChest()
    {
        FindObjectOfType<AudioManager>().Play("Button");
        quizQuestions.generateQuestions();
        quizUI.SetActive(true);
        if (!GameManagement.GameTutorial)
        {
            timerAnswer = maxTimerAnswer;
            timeToAnswer = true;
        }
        else
        {
            quizQuestions.MunculPrefabPembahasan();
        }
    }

    public void quizCorrect()
    {
        FindObjectOfType<AudioManager>().Play("Button");
        GameObject obj = Instantiate(quizBenar, new Vector3(0, 0, 0), Quaternion.identity);
        obj.transform.SetParent(quizUI.transform, false);
        healthPlayer.healthPlayer += 30;
        Destroy(obj, .5f);
    }

    public void quizWrong()
    {
        FindObjectOfType<AudioManager>().Play("Button");
        GameObject obj = Instantiate(quizSalah, new Vector3(0, 0, 0), Quaternion.identity);
        obj.transform.SetParent(quizUI.transform, false);
        Destroy(obj, .5f);
    }
}
