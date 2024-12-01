using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour
{
    public Text coinText;
    public Text healthText;
    public Text speedText;

    void Start() // update 함수들은 dataManager 값을 참조한다
    {
        UpdateCoinText();
        UpdateHealthText();
        UpdateSpeedText();
    }
    void UpdateCoinText()
    {
        if (DataManager.instance != null)
        {
            coinText.text = "Coins: " + DataManager.instance.coins;
        }
    }

    void UpdateHealthText()
    {
        if (DataManager.instance != null)
        {
            healthText.text = "Basic Health: " + DataManager.instance.maxHealth;
        }
    }
    void UpdateSpeedText()
    {
        if (DataManager.instance != null)
        {
            speedText.text = "Basic Speed: " + DataManager.instance.speed;
        }
    }
}
