using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<UIManager>();
                DontDestroyOnLoad(_instance.gameObject);
            }
            return _instance;
        }
    }

    protected virtual void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else if (_instance != this)
        {
            Destroy(gameObject);  // 중복된 인스턴스는 삭제
        }
    }

    // UI
    private Text levelText;
    private Text moneyText;
    private Slider expSlider;
    private Slider hpSlider;
    private GameObject upgradeWindow;
    private GameObject gameOverPanel;

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Main")
        {
            InitMainScene();
        }
        else if (scene.name == "Title")
        {
            InitTitleScene();
        }
    }

    private void InitMainScene()
    {
        levelText = GameObject.Find("LevelText").GetComponent<Text>();
        if (levelText == null)
        {
            Debug.LogError("LevelText isn't Found");
        }

        moneyText = GameObject.Find("MoneyText").GetComponent<Text>();
        if (moneyText == null)
        {
            Debug.LogError("moneyText isn't Found");
        }

        expSlider = GameObject.Find("ExpSlider").GetComponent<Slider>();
        if (expSlider == null)
        {
            Debug.LogError("ExpSlider isn't Found");
        }

        hpSlider = GameObject.Find("HpSlider").GetComponent<Slider>();
        if (hpSlider == null)
        {
            Debug.LogError("HpSlider isn't Found");
        }

        upgradeWindow = GameObject.Find("UpgradeWindow");
        if (upgradeWindow == null)
        {
            Debug.LogError("UpgradeWindow isn't Found");
        }

        gameOverPanel = GameObject.Find("GameOverPanel");
        if (gameOverPanel == null)
        {
            Debug.LogError("GameOverPanel isn't Found");
        }
    }

    private void InitTitleScene()
    {
        // 타이틀 씬에서 필요한 초기화 코드 작성
    }
    public void UpdateLevelText(int level)
    {
        levelText.text = "Level: " + level.ToString();
    }
    public void UpdateMoneyText(int money)
    {
        moneyText.text = "Money: " + money.ToString();
    }
    public void UpdateExpSlider(float exp)
    {
        expSlider.value = exp;
    }
    public void UpdateHpSlider(float hp)
    {
        hpSlider.value = hp;
    }
    public void OpenUpgradeUI(Weapon[] weapons)
    {
        upgradeWindow.gameObject.SetActive(true);
        upgradeWindow.GetComponent<UpgradeWindow>().ShowUpgradeOptions(weapons);
        GameManager.Instance.GamePause();
    }

    public void HideUpgradeUI()
    {
        upgradeWindow.SetActive(false);
        GameManager.Instance.GameResume();
    }

    public void OpenGameOverPanel()
    {
        gameOverPanel.SetActive(true);
    }
}
