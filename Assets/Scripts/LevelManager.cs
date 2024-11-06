using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public int startLevel = 1; // 시작 레벨
    public int endLevel = 10; // 끝 레벨
    public int experienceCapIncrease = 50; // 레벨별 경험치 요구량 증가량

    private int currentLevel;
    private int currentExperience;
    private int experienceCap; // 현재 레벨에서의 경험치 최대값
    void Start()
    {
        currentLevel = startLevel;
        currentExperience = 0;
        experienceCap = experienceCapIncrease; // 초기 경험치 한도
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.E)) // 확인용 임시코드
        {
            AddExperience(20);
        }
    }

    public void AddExperience(int amount)
    {
        if (currentLevel < endLevel) // 최고 레벨에 도달하지 않았다면
        {
            currentExperience += amount;
            Debug.Log("경험치 획득! 현재 경험치: " + currentExperience);

            if (currentExperience >= experienceCap) // 경험치가 현재 한도를 넘었을 때
            {
                LevelUp();
            }
        }
        else
        {
            Debug.Log("최고 레벨에 도달했습니다.");
        }

    }

    private void LevelUp()
    {
        currentLevel++;
        currentExperience -= experienceCap; // 레벨업 시 경험치 잔여량을 유지
        experienceCap += experienceCapIncrease;  // 다음 레벨의 경험치 한도 증가

        Debug.Log("레벨 업! 현재 레벨: " + currentLevel);
    
        StatManager.Instance.ShowWeaponSelection();
    }
}
