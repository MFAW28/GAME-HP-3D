using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem 
{
    public static void SavePlayer (MaterialController materialController)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/player.saver";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(materialController);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static PlayerData LoadPlayer(MaterialController materialController)
    {
        string path = Application.persistentDataPath + "/player.saver";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();

            return data;
        }
        else
        {
            //Debug.LogError("save file not found in " + path);
            
            return null;
        }
    }


    public static void SavePlayerScore(ScoreController scoreController)
    {
        BinaryFormatter formatterScore = new BinaryFormatter();

        string pathScore = Application.persistentDataPath + "/playerScore.saver";
        FileStream streamScore = new FileStream(pathScore, FileMode.Create);

        PlayerData dataScore = new PlayerData(scoreController);

        formatterScore.Serialize(streamScore, dataScore);
        streamScore.Close();
    }

    public static PlayerData LoadPlayerScore(ScoreController scoreController)
    {
        string pathScore = Application.persistentDataPath + "/playerScore.saver";
        if (File.Exists(pathScore))
        {
            BinaryFormatter formatterScore = new BinaryFormatter();
            FileStream streamScore = new FileStream(pathScore, FileMode.Open);

            PlayerData dataScore = formatterScore.Deserialize(streamScore) as PlayerData;
            streamScore.Close();

            return dataScore;
        }
        else
        {
            //Debug.LogError("save file not found in " + pathScore);
            return null;
        }
    }
}
