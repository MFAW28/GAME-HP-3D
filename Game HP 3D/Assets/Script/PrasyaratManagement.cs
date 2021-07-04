using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PrasyaratManagement : MonoBehaviour
{
    public GameObject MateriPrasyarat;
    public GameObject TestPrasyarat;
    public GameObject HasilTestPrasyarat;

    public GameObject PrasyratPlace;
    public GameObject SoalSyaratPrefabs;

    public int score;
    public Text scoreText;
    public Text textGood;

    private LoadLevel animLoadLevel;

    void Start()
    {
        GoPrasyarat();

        score = 0;

        animLoadLevel = FindObjectOfType<LoadLevel>();
    }

    public void GoPrasyarat()
    {
        MateriPrasyarat.SetActive(true);
        TestPrasyarat.SetActive(false);
        HasilTestPrasyarat.SetActive(false);
    }

    void Update()
    {
        scoreText.text = "" + score;
        if(score > 8)
        {
            textGood.text = "Sempurna";
        }else if(score > 6 && score < 9)
        {
            textGood.text = "Hebat Sekali";
        }
        else
        {
            textGood.text = "Ayo Semangat Lagi";
        }
    }

    //Button-------------------------------------------
    
    public void StartTest()
    {
        FindObjectOfType<AudioManager>().Play("Button");
        FindObjectOfType<AudioManager>().StopPlay("SoundPembukaan");
        MateriPrasyarat.SetActive(false);
        TestPrasyarat.SetActive(true);
        HasilTestPrasyarat.SetActive(false);
    }

    public void EndTest()
    {
        FindObjectOfType<AudioManager>().Play("Button");
        FindObjectOfType<AudioManager>().Play("SoundPembukaan");
        MateriPrasyarat.SetActive(false);
        TestPrasyarat.SetActive(false);
        HasilTestPrasyarat.SetActive(true);
        if(score >= FindObjectOfType<ScoreController>().ScorePrasyarat)
        {
            FindObjectOfType<ScoreController>().ScorePrasyarat = score;
        }
    }

    public void RestartTest()
    {
        FindObjectOfType<AudioManager>().Play("Button");
        FindObjectOfType<ScoreController>().SaveGame();
        StartCoroutine(RestartTestAnim());
    }

    IEnumerator RestartTestAnim()
    {
        animLoadLevel.animLevel.SetTrigger("NextUI");
        GameObject SoalPre = Instantiate(SoalSyaratPrefabs, transform.position, Quaternion.identity) as GameObject;
        SoalPre.transform.parent = PrasyratPlace.transform;
        FindObjectOfType<QuizSyaratManagement>().FindQuizSoal();
        yield return new WaitForSeconds(1f);
        GoPrasyarat();
    }

    public void MenuEndSyarat()
    {
        FindObjectOfType<AudioManager>().Play("Button");
        FindObjectOfType<ScoreController>().SaveGame();
        StartCoroutine(MenuEndAnim());
    }

    IEnumerator MenuEndAnim()
    {
        animLoadLevel.animLevel.SetTrigger("Start");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("MainMenu");
    }
}
