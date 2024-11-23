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
    public float maxGameTime = 2 * 10f;
    [Header("# Player Info")]
    public float health;
    public float maxHealth = 100;
    public int level;
    public int kill;
    public int exp;
    public int[] nextExp = {3, 5, 10, 100, 150, 210, 280, 360, 450}; // 각 레벨의 필요경험치를 보관
    [Header("# Game Object")]
    public Player player;
    public LevelUp uiLevelUp;
    public GameObject uiResult;


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
    public void GameStart()
    {
        health = maxHealth; // 시작할 때 현재 체력 초기화
        isLive = true;
        Time.timeScale = 1;
        gameTime = 0;
        // uiLevelUp.Select(0);
    }

    public void GameOver()
    {
        StartCoroutine(GameOverRoutine());
    }

    IEnumerator GameOverRoutine()
    {
        isLive = false;
        yield return new WaitForSeconds(0.5f);
        uiResult.SetActive(true);
        Stop();
    }

    public void GameRetry()
    {
        SceneManager.LoadScene(0);
    }
    void Update()
    {
        if(!isLive)
            return;

        gameTime += Time.deltaTime;

        if(gameTime > maxGameTime)
        {
            gameTime = maxGameTime;
        }
    }
    public void GetExp(int amount)
    {
        exp += amount;

        if (exp >= nextExp[Mathf.Min(level, nextExp.Length-1)])
        {
            exp -= nextExp[Mathf.Min(level, nextExp.Length-1)]; 
            level++;
            uiLevelUp.Show();
        }
    }

    public void Stop()
    {
        isLive = false;
        Time.timeScale = 0;
    }

    public void Resume()
    {
        isLive = true;
        Time.timeScale = 1;
    }
}
