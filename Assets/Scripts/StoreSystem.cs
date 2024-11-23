using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class StoreSystem : MonoBehaviour
{
    public static StoreSystem Instance;

    [SerializeField] private TMP_Text goldText;
    [SerializeField] private TMP_Text hpText;
    [SerializeField] private TMP_Text speedText;
    int gold;
    public int hpLevel;
    public int speedLevel;
    int hpUpgradeCost = 100;
    int speedUpgradeCost = 100;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        gold = PlayerPrefs.GetInt("Gold", 10000);
        hpLevel = PlayerPrefs.GetInt("hpLevel", 1);
        speedLevel = PlayerPrefs.GetInt("speedLevel", 1);
        UpdateUI();
    }

    public void UpgradeHP()
    {
        if (gold >= hpUpgradeCost)
        {
            gold -= hpUpgradeCost;
            hpLevel++;
            SaveData();
            UpdateUI();
        }
    }

    public void UpgradeSpeed()
    {
        if (gold >= speedUpgradeCost)
        {
            gold -= speedUpgradeCost;
            speedLevel++;
            SaveData();
            UpdateUI();
        }
    }

    private void SaveData()
    {
        PlayerPrefs.SetInt("Gold", gold);
        PlayerPrefs.SetInt("hpLevel", hpLevel);
        PlayerPrefs.SetInt("speedLevel", speedLevel);
        PlayerPrefs.Save();
    }

    private void UpdateUI()
    {
        goldText.text = $"Gold : {gold}";
        hpText.text = $"HP : Lv.{hpLevel}";
        speedText.text = $"Speed : Lv.{speedLevel}";
    }

    public void ResetData()
    {
        PlayerPrefs.DeleteAll();
        gold = 10000;
        hpLevel = 1;
        speedLevel = 1;
        SaveData();
        UpdateUI();
    }

    public void LoadLobby()
    {
        SceneManager.LoadScene("Lobby");
    }
}