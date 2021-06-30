using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int MaxScorePlayer;
    public int NumMaterialPlayer;
    public int MoneyPlayer;

    public int DataLevel;

    public PlayerData(MaterialController materialController)
    {
        NumMaterialPlayer = materialController.NumofMaterial;
    }

    public PlayerData(ScoreController scoreController)
    {
        MaxScorePlayer = scoreController.MaxScore;
        MoneyPlayer = scoreController.Money;
    }
}
