using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuizManagement : MonoBehaviour
{
    public GameObject[] options;
    public int currentQuestions;

    public Pembahasan pembahasanScript;
    public GameObject PembahasanPrefabs;
    public GameObject CanvasPembahasan;

    public Image questionsImage;
    public int jawabanBenar;
    public int jawabanBenarFull;
    public int jawabanSalahFull;
    public Text JawabanBenarTextGame;
    public Text JawabanBenarTextEnd;
    public Text JawabanSalahTextEnd;

    private ScoreController scoreGame;
    private GameManagement gameManagement;
    private QuizSelect quizSelect;
    private Spawner spawner;


    public QnAStudy qnAStudy;
    public GameObject qnAStudyPrefabs;

    private void Start()
    {
        jawabanBenar = 0;
        jawabanBenarFull = 0;
        jawabanSalahFull = 0;
        scoreGame = GetComponent<ScoreController>();
        gameManagement = GetComponent<GameManagement>();
        spawner = FindObjectOfType<Spawner>();
        quizSelect = FindObjectOfType<QuizSelect>();
        if (!GameManagement.GameTutorial)
        {
            qnAStudy = GameObject.Find("Quiz1").GetComponent<QnAStudy>();
            generateQuestions();
        }
        else
        {
            StartCoroutine(FindemStudyQNA());
        }
    }

    IEnumerator FindemStudyQNA()
    {
        yield return new WaitForSeconds(0.2f);
        qnAStudy = FindObjectOfType<QnAStudy>();
        pembahasanScript = FindObjectOfType<Pembahasan>();
        yield return new WaitForSeconds(0.1f);
        generateQuestions();
    }

    private void Update()
    {
        if (!GameManagement.GameTutorial)
        {
            JawabanBenarTextGame.text = "Jawaban Benar = " + jawabanBenar;
            JawabanBenarTextEnd.text = "Jawaban Benar = " + jawabanBenarFull;
            JawabanSalahTextEnd.text = "Jawaban Salah = " + jawabanSalahFull;
            if (spawner.Level1)
            {
                qnAStudy = GameObject.Find("Quiz1").GetComponent<QnAStudy>();
            }
            if (spawner.Level2)
            {
                qnAStudy = GameObject.Find("Quiz2").GetComponent<QnAStudy>();
            }
            if (spawner.Level3)
            {
                qnAStudy = GameObject.Find("Quiz3").GetComponent<QnAStudy>();
            }
            if (spawner.Level4)
            {
                qnAStudy = GameObject.Find("Quiz4").GetComponent<QnAStudy>();
            }
        }
    }
    
    public void correct()
    {
        StartCoroutine(correctAnswer());
    }

    public void wrong()
    {
        StartCoroutine(wrongAnswer());
    }

    public void refreshQuestion()
    {
        generateQuestions();
    }

    public void generateQuestions()
    {
        if (!GameManagement.GameTutorial)
        {
            currentQuestions = Random.Range(0, qnAStudy.QnA.Count);

            questionsImage.sprite = qnAStudy.QnA[currentQuestions].QuestionsImage;
            SetAnswer();
        }
        else
        {
            if (qnAStudy.QnA.Count > 0)
            {
                currentQuestions = Random.Range(0, qnAStudy.QnA.Count);

                questionsImage.sprite = qnAStudy.QnA[currentQuestions].QuestionsImage;
                SetAnswer();
            }
        }
    }

    void SetAnswer()
    {
        if (!GameManagement.GameTutorial)
        {
            for (int i = 0; i < options.Length; i++)
            {
                options[i].GetComponent<AnswerScript>().isCorrect = false;
                options[i].transform.GetChild(0).GetComponent<Image>().sprite = qnAStudy.QnA[currentQuestions].AnswerImage[i];

                if (qnAStudy.QnA[currentQuestions].CorrectAnswer == i + 1)
                {
                    options[i].GetComponent<AnswerScript>().isCorrect = true;
                }
            }
        }
        else
        {
            if (qnAStudy.QnA.Count > 0)
            {
                for (int i = 0; i < options.Length; i++)
                {
                    options[i].GetComponent<AnswerScript>().isCorrect = false;
                    options[i].transform.GetChild(0).GetComponent<Image>().sprite = qnAStudy.QnA[currentQuestions].AnswerImage[i];

                    if (qnAStudy.QnA[currentQuestions].CorrectAnswer == i + 1)
                    {
                        options[i].GetComponent<AnswerScript>().isCorrect = true;
                    }
                }
            }
        }
    }

    IEnumerator correctAnswer ()
    {
        quizSelect.quizCorrect();
        yield return new WaitForSeconds(.5f);
        FindObjectOfType<WeaponsPlayer>().closeChest();
        if (GameManagement.GameTutorial)
        {
            pembahasanScript.BukaUI();
            MunculPrefabsQuiz();
        }
        qnAStudy.QnA.RemoveAt(currentQuestions);

        yield return new WaitForSeconds(.5f);

        if (!GameManagement.GameIsStarted)
        {
            if (spawner.Level1)
            {
                scoreGame.Score += 20;
            }
            if (spawner.Level2)
            {
                scoreGame.Score += 40;
            }
            if (spawner.Level3)
            {
                scoreGame.Score += 60;
            }
            if (spawner.Level4)
            {
                scoreGame.Score += 100;
            }

            jawabanBenar += 1;
            jawabanBenarFull += 1;
            FindObjectOfType<Spawner>().SpawnChest();
        }
        else
        {
            scoreGame.Score += 10;
        }   
    }

    IEnumerator wrongAnswer()
    {
        quizSelect.quizWrong();
        yield return new WaitForSeconds(.5f);
        FindObjectOfType<WeaponsPlayer>().closeChest();
        if (GameManagement.GameTutorial)
        {
            pembahasanScript.BukaUI();
            qnAStudy.QnA.RemoveAt(currentQuestions);
            MunculPrefabsQuiz();
        }
        yield return new WaitForSeconds(.5f);

        if (!GameManagement.GameIsStarted)
        {
            jawabanSalahFull += 1;
            FindObjectOfType<Spawner>().SpawnChest();
        }
    }

    public void ResetJawabanBenar()
    {
        jawabanBenar = 0;
    }

    public void MunculPrefabsQuiz()
    {
        if (qnAStudy.QnA.Count > 0)
        {

        }
        else
        {
            qnAStudy.DestroyObject();
            Instantiate(qnAStudyPrefabs, transform.position, transform.rotation);
            qnAStudy = FindObjectOfType<QnAStudy>();
        }
    }

    public void MunculPrefabPembahasan()
    {
        if (pembahasanScript.PictPembahasanUI.Count > 0)
        {

        }
        else
        {
            pembahasanScript.DestroyObject();
            GameObject pembPre = Instantiate(PembahasanPrefabs, Vector2.zero, Quaternion.identity);
            pembPre.transform.SetParent(CanvasPembahasan.transform, false);
            pembahasanScript = FindObjectOfType<Pembahasan>();
        }
    }
}
