using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.WSA;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; 
    private float elapsedTime = 0f; // 경과 시간
    public float coinIncreaseInterval = 1f; // 코인 증가 간격(초)
    public int coinPerInterval = 10; // 간격마다 추가되는 코인의 수
    public int totalCoins = 0; // 전체 코인 수

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

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "InGame")
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= coinIncreaseInterval)
            {
                elapsedTime -= coinIncreaseInterval;
                AddCoins(coinPerInterval);
            }
        }
    }

    public void AddCoins(int amount)
    {
        totalCoins += amount;
        Debug.Log("코인 추가: " + amount + ", 총 코인: " + totalCoins);
    }

    public bool SpendCoins(int amount)
    {
        if (totalCoins >= amount)
        {
            totalCoins -= amount;
            Debug.Log("코인 사용: " + amount + ", 남은 코인: " + totalCoins);
            return true;
        }
        Debug.Log("코인이 부족합니다.");
        return false;
    }
}
