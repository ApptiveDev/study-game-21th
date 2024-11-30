using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    [Header("# Game Control")]
    public bool isLive;
    public float gameTime;
    public float maxGameTime; // 유니티 상에서 지정
    [Header("# Player Info")]
    public float health;
    public float maxHealth = 100;
    public int level;
    public int kill;
    public int exp;
    public int[] nextExp = {3, 5, 10, 100, 150, 210, 280, 360, 450}; // 각 레벨의 필요경험치를 보관
    [Header("# Game Object")]
    public PoolManager pool;
    public Player player;
    public LevelUp uiLevelUp; // 레벨업 무기창 
    public Result uiResult;
    public GameObject enemyCleaner;
    public GameObject hudObject;


    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // 에러 해결! 씬 로딩 순서 문제 -> 시작할 때 OnEnable() 순서 바로잡아주면서? 해결
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded; // 씬 로드 이벤트에 메서드 추가
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; // 씬 로드 이벤트에서 메서드 제거
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "InGame") // "InGame" 씬이 로드되었을 때
            GameStart(); // GameStart 함수 호출
    }
   
    public void GameStart()
    {
        health = maxHealth; // 시작할 때 현재 체력 초기화
        uiLevelUp.Select(1); // 기본 무기
        Resume();
    }
        
    public void GameOver()
    {
        StartCoroutine(GameOverRoutine());
    }

    IEnumerator GameOverRoutine() // 0.5초 딜레이 후 ui 결과창 띄우기
    {
        isLive = false;
        yield return new WaitForSeconds(0.5f);
        uiResult.gameObject.SetActive(true);
        uiResult.Lose();
        Stop();
    }

    public void GameVictory()
    {
        StartCoroutine(GameVictoryRoutine());
    }

    IEnumerator GameVictoryRoutine() // 0.5초 딜레이 후 ui 결과창 띄우기
    {
        isLive = false;
        enemyCleaner.SetActive(true);

        yield return new WaitForSeconds(0.5f);

        uiResult.gameObject.SetActive(true);
        uiResult.Win();
        Stop();
    }
    public void GameRetry() // 유니티 상 Button Onclick 함수에 연결
    {
        ResetGameManager(); // GameManager 상태 초기화
            
        //SceneManager.LoadScene("InGame"); // LoadScene () -> 인덱스 숫자도 가능
    }
    void Update()
    {
        if(!isLive) // 죽었을 때 
            return;

        gameTime += Time.deltaTime;

        if(gameTime > maxGameTime){
            gameTime = maxGameTime;
            GameVictory(); // 이겼을 때 enemy 정리하면서 승리
        }
    }

    public void GetExp(int expValue)
    {
        if (!isLive) // 게임이 진행 중이 아니면 종료
            return;
    
        exp += expValue; // 전달된 경험치 값을 추가
    
        // 최대 레벨 초과 방지 및 레벨 업 처리
        if (exp >= nextExp[Mathf.Min(level, nextExp.Length - 1)])
        {
            exp = 0; // 현재 경험치 초기화
            level++; // 레벨 증가
            uiLevelUp.Show(); // 레벨업 UI 표시
        }
    }
    /*
    public void GetExp()
    {
        if(!isLive)
            return;
            
        exp++;

        // 최대 레벨 초과 시 인덱스 넘치는 오류 방지 레벨 10 12 일케되도 최대 레벨만 적용
        if(exp == nextExp[Mathf.Min(level, nextExp.Length-1)]){
            level++;
            exp = 0;
            uiLevelUp.Show();
        }
    }
    */
    public void Stop() // 게임 시간 관련 함수 -> 멈춤
    {
        isLive = false;
        Time.timeScale = 0;
    }

    public void Resume() // 게임 시간 관련 함수 -> 지속
    {
        isLive = true;
        Time.timeScale = 1;
    }
    private void ResetGameManager()
    {
        SceneManager.LoadScene("InGame");
        // GameManager 초기화
        health = maxHealth;
        exp = 0;
        level = 1;
        kill = 0;
        gameTime = 0;
        Resume();
    }
}


