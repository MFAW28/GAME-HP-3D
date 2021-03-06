using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManagement : MonoBehaviour
{
    public int countEnemy;
    public int countChest;

    public static bool GamePlay;
    public static bool GameTutorial;
    public static bool GameIsStarted;
    public static bool GameIsPaused;
    public static bool GameWin;
    public static bool GameLose;

    [SerializeField] private TimerController timerController;
    public Text TimerText;

    //pause in midgame
    [SerializeField] private GameObject ExitUI;

    //gameEnd
    private ScoreController scoreGame;
    public static bool GameEnd;
    public GameObject BtnNextLevel;
    [SerializeField] private GameObject DeathUI;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text maxScoreText;
    [SerializeField] private Text EndTextUI;
    [SerializeField] private GameObject GiftText;

    private LoadLevel animLoadLevel;
    private Spawner spawner;

    void Start()
    {
        
        if (GamePlay)
        {
            FindObjectOfType<AudioManager>().Play("SoundGame");
            timerController = this.GetComponent<TimerController>();
            scoreGame = FindObjectOfType<ScoreController>();
            GameIsStarted = true;
            GameTutorial = false;
            GameIsPaused = false;
            GameEnd = false;
            GameWin = false;
            GameLose = false;
            countEnemy = 0;

            ExitUI.SetActive(false);
            DeathUI.SetActive(false);
            MaterialController material = FindObjectOfType<MaterialController>();
            material.LoadPlayerData();

            animLoadLevel = FindObjectOfType<LoadLevel>();
            spawner = FindObjectOfType<Spawner>();
            if (!GameIsStarted)
            {
                BtnNextLevel.SetActive(false);
                GiftText.SetActive(false);
            }
        }
    }

    void Update()
    {
        if (GamePlay)
        {
            if (!GameIsStarted)
            {
                TimerText.text = timerController.time.ToString(@"mm\:ss");
                if (timerController.currentTime <= 0)
                {
                    GameEnd = true;
                }
            }

            if (GameEnd == true)
            {
                timerController.StopTimer();
                GameIsPaused = true;
                if (GameWin)
                {
                    BtnNextLevel.SetActive(true);
                    scoreGame.ifWinGame();
                    GiftText.SetActive(true);
                    EndTextUI.text = "KAMU MENANG";
                    if(scoreGame.LevelGame == 1)
                    {
                        GiftText.GetComponent<Text>().text = "Kamu mendapatkan senjata Pistol";
                    }else if(scoreGame.LevelGame == 2)
                    {
                        GiftText.GetComponent<Text>().text = "Kamu mendapatkan senjata Sihir";
                    }
                    else if (scoreGame.LevelGame == 3)
                    {
                        GiftText.GetComponent<Text>().text = "Kamu mendapatkan semua warna pakaian di Kostum Pemain";
                    }
                }
                if (GameLose)
                {
                    BtnNextLevel.SetActive(false);
                    scoreGame.ifLoseGame();
                    GiftText.SetActive(false);
                    EndTextUI.text = "KAMU KALAH";
                }
                if (scoreGame.Score > scoreGame.MaxScore)
                {

                    scoreGame.MaxScore = scoreGame.Score;
                }
                //scoreGame.Money += scoreGame.Score;
                DeathUI.SetActive(true);
            }

            scoreText.text = "" + scoreGame.Score;
            maxScoreText.text = "" + scoreGame.MaxScore;
        }
    }

    public void openDoor()
    {
        FindObjectOfType<AudioManager>().Play("Button");
        timerController.StartTimer();
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

    public void NextLevel(string LevelName)
    {
        FindObjectOfType<AudioManager>().Play("Button");
        scoreGame.SaveGame();
        StartCoroutine(NextLevelAnim(LevelName));
    }

    IEnumerator NextLevelAnim(string LevelName)
    {
        animLoadLevel.animLevel.SetTrigger("Start");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(LevelName);
    }

    public void EndThisGame()
    {
        FindObjectOfType<AudioManager>().Play("Button");
        GameEnd = true;
        GameWin = true;
    }
}
