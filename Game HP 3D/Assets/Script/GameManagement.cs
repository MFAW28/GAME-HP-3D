using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManagement : MonoBehaviour
{
    public int countEnemy;
    public int countChest;

    public static bool GameTutorial;
    public static bool GameIsStarted;
    public static bool GameIsPaused;
    public static bool GameWin;
    public static bool GameLose;

    //pause in midgame
    [SerializeField] private GameObject ExitUI;

    //gameEnd
    private ScoreController scoreGame;
    public static bool GameEnd;
    [SerializeField] private GameObject DeathUI;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text maxScoreText;
    [SerializeField] private Text EndTextUI;

    private LoadLevel animLoadLevel;
    private Spawner spawner;

    void Start()
    {
        FindObjectOfType<AudioManager>().Play("SoundGame");
        GameIsStarted = true;
        GameTutorial = false;
        GameIsPaused = false;
        GameEnd = false;
        GameWin = false;
        GameLose = false;
        countEnemy = 0;

        scoreGame = FindObjectOfType<ScoreController>();
        ExitUI.SetActive(false);
        DeathUI.SetActive(false);
        MaterialController material = FindObjectOfType<MaterialController>();
        material.LoadPlayerData();
        scoreGame.LoadGame();

        animLoadLevel = FindObjectOfType<LoadLevel>();
        spawner = FindObjectOfType<Spawner>();
    }

    void Update()
    {
        if(GameEnd == true)
        {
            GameIsPaused = true;
            if(GameWin){
                EndTextUI.text = "KAMU MENANG";
            }
            if(GameLose){
                EndTextUI.text = "KAMU KALAH";
            }
            if(scoreGame.Score > scoreGame.MaxScore)
            {
                
                scoreGame.MaxScore = scoreGame.Score;
            }
            //scoreGame.Money += scoreGame.Score;
            DeathUI.SetActive(true);
            scoreGame.ifWinGame();
        }


        scoreText.text = "" + scoreGame.Score;
        maxScoreText.text = "" + scoreGame.MaxScore;
    }

    public void openDoor()
    {
        FindObjectOfType<AudioManager>().Play("Button");
        GameIsStarted = false;
    }

    public void ExitButton()
    {
        FindObjectOfType<AudioManager>().Play("Button");
        FindObjectOfType<Player>().offMove();
        ExitUI.SetActive(true);
        ExitUI.GetComponent<Animator>().SetBool("EndAnim", true);
        GameIsPaused = true;
    }

    public void ResumeGame()
    {
        FindObjectOfType<AudioManager>().Play("Button");
        StartCoroutine(ResumeGameAnim());
    }

    IEnumerator ResumeGameAnim(){
        ExitUI.GetComponent<Animator>().SetBool("EndAnim", false);
        yield return new WaitForSeconds(.5f);
        ExitUI.SetActive(false);
        GameIsPaused = false;
    }

    public void RestartGame()
    {
        FindObjectOfType<AudioManager>().Play("Button");
        FindObjectOfType<AudioManager>().StopPlay("SoundGame");
        if (GameEnd)
        {
            scoreGame.SaveGame();
        }
        StartCoroutine(RestartGameAnim());
    }

    IEnumerator RestartGameAnim()
    {
        animLoadLevel.animLevel.SetTrigger("Start");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ExitGame()
    {
        FindObjectOfType<AudioManager>().Play("Button");
        FindObjectOfType<AudioManager>().StopPlay("SoundGame");
        if (GameEnd)
        {
            scoreGame.SaveGame();
        }
        StartCoroutine(ExitGameAnim());
    }

    IEnumerator ExitGameAnim()
    {
        animLoadLevel.animLevel.SetTrigger("Start");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("MainMenu");
    }

    public void EndThisGame()
    {
        FindObjectOfType<AudioManager>().Play("Button");
        GameEnd = true;
        GameWin = true;
    }
}
