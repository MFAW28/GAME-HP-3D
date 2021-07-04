using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    public int Score;
    public int MaxScore;
    public int Money;
    public int LevelGame;
    public int ScorePrasyarat;

    public Text TextScore;
    public Text TextMaxScore;
    
    private LocateLevel LevelthisGame;

    private void Awake()
    {
        LoadGame();
    }
    // Start is called before the first frame update
    void Start()
    {
        if (!GameManagement.GameTutorial)
        {
            LevelthisGame = GetComponent<LocateLevel>();
        }
    }

    private void Update()
    {
        if (GameManagement.GamePlay)
        {
            TextScore.text = "Skor : " + Score;
            TextMaxScore.text = "Maks Skor : " + MaxScore;
        }
    }

    public void LoadGame()
    {
        PlayerData data = SaveSystem.LoadPlayerScore(this);
        Score = data.ScoreGame;
        MaxScore = data.MaxScorePlayer;
        LevelGame = data.DataLevel;
        ScorePrasyarat = data.ScorePrasyarat;
        //Money = data.MoneyPlayer;
    }

    public void ifWinGame(){
        LevelGame = LevelthisGame.NextLevel;
    }

    public void ifLoseGame()
    {
        LevelGame = LevelthisGame.Level;
    }

    public void SaveGame()
    {
        SaveSystem.SavePlayerScore(this);
    }
}
