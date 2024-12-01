using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Security;

[System.Serializable]
public class PlayerData
{
    public int maxHealth;
    public int speed;
    public int[] Level;
}

public class DataManager : MonoBehaviour
{
    public static DataManager instance;

    public PlayerData startingPlayer;

    public PlayerData nowPlayer = new PlayerData();

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

    public void SaveData()
    {
        string data = JsonUtility.ToJson(nowPlayer);

        File.WriteAllText(path + filename, data);
    }

    public void LoadData()
    {
        string data = File.ReadAllText(path + filename);

        if (data == null) {
            data = JsonUtility.ToJson(startingPlayer);

            File.WriteAllText(path + filename, data);
        }

        nowPlayer = JsonUtility.FromJson<PlayerData>(data);
        
    }
}
