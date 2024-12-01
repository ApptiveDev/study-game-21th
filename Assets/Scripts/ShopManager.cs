using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShopManager : MonoBehaviour
{   
    public Text coinsText;
    public Text healthPriceText;
    public Text speedPriceText;

    public int[] healthCosts = { 10, 20, 30, 50 };
    public int[] speedCosts = { 10, 20, 30, 50 };

    public float[] healthIncreases = { 20f, 40f, 60f, 80f };
    public float[] speedIncreases = { 1f, 2f, 3f, 4f };

    private int healthLevel = 0;
    private int speedLevel = 0;

    void Start()
    {
        if (DataManager.instance == null)
        {
            Debug.LogError("GameManager instance not found!");
            return;
        }
        UpdateUI();

    }


    public void BuyHealthUpgrade()
    {
        // 각 스탯별 레벨 꽉찼을 때 더이상 구매 x
        if (healthLevel >= healthCosts.Length)
        {
            Debug.Log("Health upgrade maxed out!");
            return;
        }

        int cost = healthCosts[healthLevel];
        if (DataManager.instance.coins >= cost)
        {
            // 코인 차감 및 체력 증가
            DataManager.instance.coins -= cost;
            DataManager.instance.maxHealth += healthIncreases[healthLevel];

            healthLevel++;
            DataManager.instance.SaveData(); // 변경된 데이터 저장
            Debug.Log("Health upgraded!");
            UpdateUI(); // UI 업데이트
        }
        else
        {
            Debug.Log("Not enough coins for Health Upgrade!");
        }
    }

    public void BuySpeedUpgrade()
    {
        if (speedLevel >= speedCosts.Length)
        {
            Debug.Log("Speed upgrade maxed out!");
            return;
        }

        int cost = speedCosts[speedLevel];
        if (DataManager.instance.coins >= cost)
        {
            // 코인 차감 및 속도 증가
            DataManager.instance.coins -= cost;
            DataManager.instance.speed += speedIncreases[speedLevel];

            speedLevel++; 
            DataManager.instance.SaveData(); 
            Debug.Log("Speed upgraded!");
            UpdateUI(); // UI 업데이트
        }
        else
        {
            Debug.Log("Not enough coins for Speed Upgrade!");
        }
    }

    void UpdateUI()
    {
        // 현재 코인 수 업데이트
        coinsText.text = "Coins: " + DataManager.instance.coins;

        // 체력 강화 가격 텍스트 업데이트
        if (healthLevel < healthCosts.Length)
        {
            healthPriceText.text = "Price: " + healthCosts[healthLevel];
        }
        else
        {
            healthPriceText.text = "Maxed Out"; // 최대 강화 시
        }

        // 속도 강화 가격 텍스트 업데이트
        if (speedLevel < speedCosts.Length)
        {
            speedPriceText.text = "Price: " + speedCosts[speedLevel];
        }
        else
        {
            speedPriceText.text = "Maxed Out"; // 최대 강화 시
        }

        if (coinsText == null) Debug.LogError("Coins Text is not assigned!");
        if (healthPriceText == null) Debug.LogError("Health Price Text is not assigned!");
        if (speedPriceText == null) Debug.LogError("Speed Price Text is not assigned!");
    }

}
