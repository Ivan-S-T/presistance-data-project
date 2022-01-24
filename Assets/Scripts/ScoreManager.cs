using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    public static int bestScore;
    public string playerName;
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

        bestPlayer = ReadFromJson();
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
        BestPlayerData bestPlayerData = new BestPlayerData(name,bestScore);


        string json = JsonUtility.ToJson(bestPlayerData);
        File.WriteAllText(jsonPath, json);
    }

    public BestPlayerData ReadFromJson()
    {
        if (File.Exists(jsonPath))
        {
            BestPlayerData bestPlayerData = JsonUtility.FromJson<BestPlayerData>(jsonPath);
            return bestPlayerData;
        }
        return new BestPlayerData();
    }

}
