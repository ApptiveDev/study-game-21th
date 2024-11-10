using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    /*
     * GameManager
     *├── UIManager: UI 업데이트 관련 메서드 및 속성
     *├── PrefabManager: 프리팹 로드 및 제공 메서드
     *└── PlayerManager: 플레이어 관련 데이터 및 제어
     *
     * 나중에 위 디자인처럼 바꿔야 할듯. 게임 매니저가 하는게 너무 많음.
     */

    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();
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
        }
        else if (_instance != this)
        {
            Destroy(gameObject);  // 중복된 인스턴스는 삭제
        }
    }

    // Player-related
    private GameObject player;
    private WeaponSpawner weaponSpawner;

    // Prefabs
    private GameObject expPrefab;
    private GameObject arrowPrefab;
    private GameObject magneticFieldPrefab;
    private GameObject explosiveBottlePrefab;
    private GameObject explosionPrefab;

    // UI
    private Text levelText;
    private Slider expSlider;
    private GameObject upgradeWindow;

    void Start()
    {
        Init();
    }

    private void Init()
    {
        player = GameObject.FindWithTag("Player");
        weaponSpawner = GameObject.Find("WeaponSpawner").GetComponent<WeaponSpawner>();
        LoadUI();
        LoadPrefabs();
    }
    private void LoadUI()
    {
        levelText = GameObject.Find("LevelText").GetComponent<Text>();
        if (levelText == null)
        {
            Debug.LogError("LevelText isn't Found");
        }

        expSlider = GameObject.Find("ExpSlider").GetComponent<Slider>();
        if (expSlider == null)
        {
            Debug.LogError("ExpSlider isn't Found");
        }

        upgradeWindow = GameObject.Find("UpgradeWindow");
        if (upgradeWindow == null)
        {
            Debug.LogError("UpgradeWindow isn't Found");
        }
    }
    private void LoadPrefabs()
    {
        expPrefab = Resources.Load<GameObject>("Prefabs/ExpPrefab");
        if (expPrefab == null)
        {
            Debug.LogError("No expPrefab in Resources.");
        }

        arrowPrefab = Resources.Load<GameObject>("Prefabs/Weapons/ArrowPrefab");
        if (arrowPrefab == null)
        {
            Debug.LogError("No arrowPrefab in Resources.");
        }

        magneticFieldPrefab = Resources.Load<GameObject>("Prefabs/Weapons/MagneticField");
        if (magneticFieldPrefab == null)
        {
            Debug.LogError("No magneticFieldPrefab in Resources.");
        }

        explosiveBottlePrefab = Resources.Load<GameObject>("Prefabs/Weapons/ExplosiveBottle");
        if (explosiveBottlePrefab == null)
        {
            Debug.LogError("No explosiveBottlePrefab in Resources.");
        }

        explosionPrefab = Resources.Load<GameObject>("Prefabs/Weapons/ExplosionPrefab");
        if (explosionPrefab == null)
        {
            Debug.LogError("No explosionPrefab in Resources.");
        }
    }
    
    // getter
    public GameObject GetPlayer(){ return player; }
    public WeaponSpawner GetWeaponSpawener() { return weaponSpawner; }
    public GameObject GetExpPrefab(){ return expPrefab; }
    public GameObject GetArrowPreafb() { return arrowPrefab; }
    public GameObject GetMagneticFieldPrefab() { return magneticFieldPrefab; }
    public GameObject GetExplosiveBottlePrefab() { return explosiveBottlePrefab; }
    public GameObject GetExplosionPrefab() { return explosionPrefab; }

    // 정보은닉을 위해 게임매니저가 게임오브젝트를 멤버변수로 갖고 있더라도, 해당 오브젝트의 데이터에 직접 접근하지 않음.
    // 나중에 매니저 따로 나눠서 기능 분리 예정
    public void UpdateLevelText(int level)
    {
        levelText.text = "Level: " + level.ToString();
    }
    public void UpdateExpSlider(float exp)
    {
        expSlider.value = exp;
    }
    public void OpenUpgradeWindow()
    {
        Weapon[] weapons = weaponSpawner.GetRandomWeapons(3); // 무작위 무기 선택
        upgradeWindow.gameObject.SetActive(true);
        upgradeWindow.GetComponent<UpgradeWindow>().ShowUpgradeOptions(weapons);
        Time.timeScale = 0;
    }
    public void UpgradeWeapon(Weapon weapon)
    {
        weapon.Upgrade();
        // 선택된 무기 업그레이드 처리 로직
        Debug.Log($"{weapon.WeaponName}이(가) 업그레이드되었습니다.");
        HideUpgradeWindow();
    }
    public void HideUpgradeWindow()
    {
        upgradeWindow.SetActive(false);   
        Time.timeScale = 1;              
    }
}

