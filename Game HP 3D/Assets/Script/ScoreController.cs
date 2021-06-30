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

    public Text TextScore;
    public Text TextMaxScore;
    
    private LocateLevel LevelthisGame;
    

    // Start is called before the first frame update
    void Start()
    {
        Score = 0;
        LoadGame();
        LevelthisGame = GetComponent<LocateLevel>();
    }

    private void Update()
    {
        TextScore.text = "Skor : " + Score;
        TextMaxScore.text = "Maks Skor : " + MaxScore;
    }

    public void LoadGame()
    {
        PlayerData data = SaveSystem.LoadPlayerScore(this);
        MaxScore = data.MaxScorePlayer;
        LevelGame = data.DataLevel;
        //Money = data.MoneyPlayer;
    }

    public void ifWinGame(){
        if(LevelGame < LevelthisGame.Level){
            LevelGame = LevelthisGame.NextLevel;
        }
        else
        {
            LevelGame = LevelthisGame.Level;
        }
    }

    public void SaveGame()
    {
        SaveSystem.SavePlayerScore(this);
    }
}
