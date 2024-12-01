using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Security;

public class PlayerData
{
    public int coin;
    public int maxHealth;
    public float speed;
}

public class DataManager : MonoBehaviour
{
    public static DataManager instance;

    public PlayerData nowPlayer;

    PlayerData startingPlayer;

    string path;
    string filename = "save";

    private void Awake()
    {
        if (instance == null) {
            instance = this;
        }
        else if (instance != this) {
            Destroy(instance.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);

        path = Application.persistentDataPath + "/";

        LoadData();
    }

    void Start()
    {
        // startingPlayer 변수 초기화 필요
        startingPlayer = new PlayerData()
        {
            coin = 9999,
            maxHealth = 100,
            speed = 3
        };

        if (nowPlayer == null) {
            nowPlayer = startingPlayer;
        }
    }

    public void SaveData()
    {
        string data = JsonUtility.ToJson(nowPlayer);

        File.WriteAllText(path + filename, data);
    }

    public void LoadData()
    {
        string filePath = path + filename;

        // 파일이 존재하는지 확인
        if (File.Exists(filePath))
        {
            string data = File.ReadAllText(filePath);
            nowPlayer = JsonUtility.FromJson<PlayerData>(data);
        }
        else
        {
            // 파일이 없으면 초기 데이터로 저장
            nowPlayer = startingPlayer;
            SaveData();
        }
    }

    public void Reset()
    {
        nowPlayer = startingPlayer;
    }
}
