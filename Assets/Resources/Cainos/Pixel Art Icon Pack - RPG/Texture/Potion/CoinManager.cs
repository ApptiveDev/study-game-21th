using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public static CoinManager Instance;
    public int coinAmount = 0;
    private void Awake() {
        if (Instance == null) // CoinManager 인스턴스가 중복되지 않도록 설정
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else
        {
            Destroy(gameObject);
        }
    }

    public void AddCoins(int amount)
    {
        coinAmount += amount;
        Debug.Log("코인 추가: " + amount + "총 보유 코인: " + coinAmount);
    }
    
}
