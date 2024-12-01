using UnityEngine;

public class GameDataManager : MonoBehaviour
{
    public static GameDataManager Instance;

    private void Awake()
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
    }

    public void SaveData(int gold, int hpLevel, int speedLevel)
    {
        PlayerPrefs.SetInt("Gold", gold);
        PlayerPrefs.SetInt("hpLevel", hpLevel);
        PlayerPrefs.SetInt("speedLevel", speedLevel);
        PlayerPrefs.Save();
    }

    public void LoadData(out int gold, out int hpLevel, out int speedLevel)
    {
        gold = PlayerPrefs.GetInt("Gold", 10000);
        hpLevel = PlayerPrefs.GetInt("hpLevel", 1);
        speedLevel = PlayerPrefs.GetInt("speedLevel", 1);
    }

    public int GetHpLevel()
    {
        return PlayerPrefs.GetInt("hpLevel", 1);
    }

    public int GetSpeedLevel()
    {
        return PlayerPrefs.GetInt("speedLevel", 1);
    }
}
