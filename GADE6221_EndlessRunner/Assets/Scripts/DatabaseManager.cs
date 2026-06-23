using UnityEngine;
using System;
using System.IO;

public class PlayerData
{
    public string playerName;
    public string password;
    public int highScore;
}


public class DatabaseManager : MonoBehaviour
{
    public static DatabaseManager Instance;


    public PlayerData player;


    private string path;



    void Awake()
    {

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }


        path = Application.persistentDataPath + "/player.json";


        LoadPlayer();

    }




    public void SavePlayer()
    {

        string json =
        JsonUtility.ToJson(player, true);


        File.WriteAllText(path, json);


        Debug.Log("Player Saved");

    }




    public void LoadPlayer()
    {

        if (File.Exists(path))
        {

            string json =
            File.ReadAllText(path);


            player =
            JsonUtility.FromJson<PlayerData>(json);

        }
        else
        {

            player = new PlayerData();

            player.highScore = 0;

        }

    }




    public void UpdateHighScore(int score)
    {

        if (score > player.highScore)
        {

            player.highScore = score;

            SavePlayer();

        }

    }


}

