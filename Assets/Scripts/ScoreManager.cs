using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    public static int bestScore;
    public static string playerName="you";
    public Text scoreText;

    string jsonPath;

    public InputField inputField;

    [HideInInspector]
    public BestPlayerData bestPlayer;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        jsonPath = Application.persistentDataPath + "/bestScore.json";

        
        SetScoreText();
    }

    private void SetScoreText()
    {
        bestPlayer = ReadFromJson();
        scoreText.text = $"Best Score:{bestPlayer.name}:{bestPlayer.bestScore}";
    }

    [System.Serializable]
    public class BestPlayerData
    {
        public string name;
        public int bestScore;


        public BestPlayerData(string name,int bestScore)
        {
            this.name = name;
            this.bestScore = bestScore;
        }

        public BestPlayerData()
        {
            name = "Who Are You?";
            bestScore = 0;
        }
    }

    public void SetPlayerName() => playerName = inputField.text;

    public void WriteToJson()
    {
        BestPlayerData bestPlayerData = new BestPlayerData(playerName,bestScore);


        string json = JsonUtility.ToJson(bestPlayerData);
        File.WriteAllText(jsonPath, json);
    }

    public BestPlayerData ReadFromJson()
    {
        if (File.Exists(jsonPath))
        {
            string jsonText = File.ReadAllText(jsonPath);
            BestPlayerData bestPlayerData = JsonUtility.FromJson<BestPlayerData>(jsonText);
            return bestPlayerData;
        }
        return new BestPlayerData();
    }

}
