using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    public int Score;
    public int MaxScore;
    public int Money;

    public Text TextScore;
    public Text TextMaxScore;

    // Start is called before the first frame update
    void Start()
    {
        Score = 0;
        LoadGame();
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
        //Money = data.MoneyPlayer;
    }

    public void SaveGame()
    {
        SaveSystem.SavePlayerScore(this);
    }
}
