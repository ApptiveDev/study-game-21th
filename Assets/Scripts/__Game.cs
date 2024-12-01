using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{   /*
    // 싱글톤 패턴 구현
    public static Game Instance;
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }


    [SerializeField] Text[] allCoinsUIText;
    public int Coins = 1000;

    void Start()
    {
        UpdateAllCoinsUIText();
    }
    public void UseCoins (int amount)
    {
        Coins -= amount;
    }

    public bool HasEnoughCoins (int amount)
    {
        return (Coins >= amount);
    }

    public void UpdateAllCoinsUIText()
    {
        for (int i = 0; i < allCoinsUIText.Length; i++)
        {
            allCoinsUIText[i].text = Coins.ToString();
        }
    }
    */
}
