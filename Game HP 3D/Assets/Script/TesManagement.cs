using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TesManagement : MonoBehaviour
{
    public GameObject TesAwal;
    public GameObject TesIsi;
    public GameObject TesAkhir;

    public int score;
    public Text scoreText;
    public Text textGood;

    private LoadLevel animLoadLevel;

    void Start()
    {
        FindObjectOfType<AudioManager>().Play("SoundPembukaan");
        TesAwal.SetActive(true);
        TesIsi.SetActive(false);
        TesAkhir.SetActive(false);

        score = 0;

        animLoadLevel = FindObjectOfType<LoadLevel>();
    }

    void Update()
    {
        scoreText.text = "" + score;
        if (score >= 80)
        {
            textGood.text = "Sempurna";
        }
        else if (score >= 60 && score < 80)
        {
            textGood.text = "Hebat Sekali";
        }
        else if(score < 60)
        {
            textGood.text = "Ayo Semangat Lagi";
        }
    }

    //Button-------------------------------------------
    public void BackToMenu()
    {
        FindObjectOfType<AudioManager>().Play("Button");
        FindObjectOfType<AudioManager>().StopPlay("SoundPembukaan");
        StartCoroutine(BackToMenuAnim());
    }

    IEnumerator BackToMenuAnim()
    {
        animLoadLevel.animLevel.SetTrigger("Start");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("MainMenu");
    }

    public void StartTest()
    {
        FindObjectOfType<AudioManager>().Play("Button");
        FindObjectOfType<AudioManager>().StopPlay("SoundPembukaan");
        TesAwal.SetActive(false);
        TesIsi.SetActive(true);
        TesAkhir.SetActive(false);
    }

    public void EndTest()
    {
        FindObjectOfType<AudioManager>().Play("Button");
        FindObjectOfType<AudioManager>().Play("SoundPembukaan");
        TesAwal.SetActive(false);
        TesIsi.SetActive(false);
        TesAkhir.SetActive(true);
    }

    public void RestartTest()
    {
        FindObjectOfType<AudioManager>().Play("Button");
        StartCoroutine(RestartTestAnim());
    }

    IEnumerator RestartTestAnim()
    {
        animLoadLevel.animLevel.SetTrigger("Start");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("TesLatihan");
    }
}
