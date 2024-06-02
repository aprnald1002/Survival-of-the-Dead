using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using UnityEngine;

public class GameDataManager : MonoBehaviour
{

    public static GameDataManager instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = FindObjectOfType<GameDataManager>();
            }

            return m_instance;
        }
    }
    
    private static GameDataManager m_instance;
    private JGameClass jgc;
    private PlayerMovement playerMovement;
    
    private string filePath;
    private string fileName;
    
    public int bestScore;
    public int bestWaves;
    public int bestZombie;

    private void Awake()
    {
        fileName = "GameData.json";
        filePath = Path.Combine(Application.dataPath, fileName);
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    private void OnEnable()
    {
        if (File.Exists(filePath))
        {
            Debug.Log("이미 있음");
            string jsonData = File.ReadAllText(filePath);
            jgc = JsonToObject<JGameClass>(jsonData);
        }
        else
        {
            Debug.Log("파일생성");
            jgc = new JGameClass
            {
                bScore = 0,
                bWave = 0,
                bKill = 0
            };
            string jsonData = ObjectToJson(jgc);
            File.WriteAllText(filePath, jsonData);
        }
        bestScore = jgc.bScore;
        bestWaves = jgc.bWave;
        bestZombie = jgc.bKill;
        ChangeValue(-1, "all");
    }
    
    public void SaveTheData()
    {
        jgc = new JGameClass
        {
            bScore = bestScore,
            bWave = bestWaves,
            bKill = bestZombie
        };
    
        string jsonData = ObjectToJson(jgc);
        File.WriteAllText(filePath, jsonData);
    }
    
    string ObjectToJson(JGameClass obj)
    {
        return JsonConvert.SerializeObject(obj, Formatting.Indented);
    }
    
    T JsonToObject<T>(string jsonData)
    {
        return JsonConvert.DeserializeObject<T>(jsonData);
    }

    public void ChangeValue(int value, string valueName)
    {

        if (valueName.Equals("Wave") && value > bestWaves) {
            bestWaves = value;
        } 
        else if (valueName.Equals("Zombie") && bestZombie + 1 > bestZombie) {
            bestZombie += 1;
        } 
        else if (valueName.Equals("Score") && value > bestScore) {
            bestScore = value;
        }
        
        UIManager.instance.UpdateBestText(bestWaves, bestZombie, bestScore);
    }
}

public class JGameClass
{
    public int bScore;
    public int bWave;
    public int bKill;
}
