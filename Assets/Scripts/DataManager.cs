using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager instance;

    // Player Info -> 1. 코인 2. 체력 3. 스피드
    public int coins;
    public float maxHealth;
    public float speed;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // 모든 씬에서 유지
            LoadData(); // 게임실행 시 기본값을 불러오기
        }
        else
        {
            Destroy(gameObject); // 중복 생성 방지
        }
    }

    public void SaveData()
    {
        PlayerPrefs.SetInt("Coins", coins);
        PlayerPrefs.SetFloat("MaxHealth", maxHealth);
        PlayerPrefs.SetFloat("Speed", speed);
        PlayerPrefs.Save();
        Debug.Log("Player data saved!");
    }

    public void LoadData()
    {
        coins = PlayerPrefs.GetInt("Coins", 50); // 저장된 데이터가 없으면 1000을 불러오기
        maxHealth = PlayerPrefs.GetFloat("MaxHealth", 100f);
        speed = PlayerPrefs.GetFloat("Speed", 2f);
        Debug.Log("Player data loaded!");
    }
}
