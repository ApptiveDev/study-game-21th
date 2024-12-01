using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StoreSystem : MonoBehaviour
{
    [SerializeField] private TMP_Text goldText;
    [SerializeField] private TMP_Text hpText;
    [SerializeField] private TMP_Text speedText;

    int gold;
    public int hpLevel;
    public int speedLevel;

    int hpUpgradeCost = 100;
    int speedUpgradeCost = 100;

    private void Start()
    {
        GameDataManager.Instance.LoadData(out gold, out hpLevel, out speedLevel);
        UpdateUI();
    }

    public void UpgradeHP()
    {
        if (gold >= hpUpgradeCost)
        {
            gold -= hpUpgradeCost;
            hpLevel++;
            GameDataManager.Instance.SaveData(gold, hpLevel, speedLevel);
            UpdateUI();
        }
    }

    public void UpgradeSpeed()
    {
        if (gold >= speedUpgradeCost)
        {
            gold -= speedUpgradeCost;
            speedLevel++;
            GameDataManager.Instance.SaveData(gold, hpLevel, speedLevel);
            UpdateUI();
        }
    }

    private void UpdateUI()
    {
        goldText.text = $"Gold : {gold}";
        hpText.text = $"HP : Lv.{hpLevel}";
        speedText.text = $"Speed : Lv.{speedLevel}";
    }

    public void ResetData()
    {
        gold = 10000;
        hpLevel = 1;
        speedLevel = 1;
        GameDataManager.Instance.SaveData(gold, hpLevel, speedLevel);
        UpdateUI();
    }

    public void LoadLobby()
    {
        SceneManager.LoadScene("Lobby");
    }
}
