using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using System;

public class SelectionWindowManager : MonoBehaviour
{
	/* ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ */
	// 선택지 관련
	public GameObject levelUpUI;
	public Button optionButton1;
	public Button optionButton2;
	public Button optionButton3;
    public const int OptionToSelect = 3;
	public Text descriptionText;

	private enum LevelUpOption
	{
		AttackSpeed,
		AttackRange,
		AddCircleWeapon,
		AddBombWeapon
	}

	private List<LevelUpOption> availableOptions;

    // Start is called before the first frame update
    public static SelectionWindowManager Instance {  get; private set; }
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        HideLevelUpPopup();
    }
    public void ShowLevelUpPopup()
    {
        Time.timeScale = 0.0f;
        this.gameObject.SetActive(true);
    }

    public void HideLevelUpPopup()
    {
        Time.timeScale = 1.0f;
        this.gameObject.SetActive(false);  
    }

    /* ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ */
    void DisplayRandomOptions()
    {
        availableOptions = new List<LevelUpOption>
        {
            LevelUpOption.AttackSpeed,
            LevelUpOption.AttackRange,
            LevelUpOption.AddCircleWeapon,
            LevelUpOption.AddBombWeapon
        };
		
		List<LevelUpOption> selectedOptions = new();

		while(selectedOptions.Count < OptionToSelect)
		{
			int randomIndex = UnityEngine.Random.Range(0, availableOptions.Count);
			if(!selectedOptions.Contains(availableOptions[randomIndex]))
			{
				selectedOptions.Add(availableOptions[randomIndex]);
			}
		}
		// 버튼 연결
		ConfigureButton(optionButton1, selectedOptions[0]);
        ConfigureButton(optionButton2, selectedOptions[1]);
        ConfigureButton(optionButton3, selectedOptions[2]);
	}

	// 버튼 연결
    void ConfigureButton(Button button, LevelUpOption option)
    {
        button.onClick.RemoveAllListeners();
        switch (option)
        {
            case LevelUpOption.AttackSpeed:
                button.onClick.AddListener(OnAttackSpeedSelected);
                button.GetComponentInChildren<TMP_Text>().text = "MORE ATTACK SPEED!(0.1s)";
                break;

            case LevelUpOption.AttackRange:
                button.onClick.AddListener(OnAttackRangeSelected);
                button.GetComponentInChildren<TMP_Text>().text = "MORE LONG BASIC WEAPON!(0.1m)";
                break;

            case LevelUpOption.AddCircleWeapon:
                button.onClick.AddListener(OnCircleWeaponSelected);
                button.GetComponentInChildren<TMP_Text>().text = "ADD CIRCLE WEAPON!";
                break;

            case LevelUpOption.AddBombWeapon:
                button.onClick.AddListener(OnBombWeaponSelected);
                button.GetComponentInChildren<TMP_Text>().text = "ADD BOMB WEAPON!";
                break;
        }
    }

	// 여기 weaponManager로 다 수정해야함. 
    void OnAttackSpeedSelected()
    {
		WeaponManager.Instance.AddAttackSpeed(0.1f);
        HideLevelUpPopup();
    }

    void OnAttackRangeSelected()
    {
		WeaponManager.Instance.AddAttackRange(0.2f);
        HideLevelUpPopup();
    }

    void OnCircleWeaponSelected()
    {
		WeaponManager.Instance.CreateCircle(GameMgr.Instance.GetPlayer());
		//WeaponManager.Instance.StartCoroutine(WeaponManager.Instance.CircleUpdate( GameMgr.Instance.GetPlayer() ));
        HideLevelUpPopup();
    }

    void OnBombWeaponSelected()
    {
        WeaponManager.Instance.BombCall();
        HideLevelUpPopup();
    }

    void Start()
    {
		DisplayRandomOptions();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
