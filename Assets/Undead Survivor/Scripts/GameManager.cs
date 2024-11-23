using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("#Player Info")]
    public int level;
    public int kill;
    public int exp;
    public float maxHp = 100;
    public float curHp;
    public int myMoney;
    public int[] nextExp = { 5, 10, 15, 20, 25, 30};

    [Header("#Game Object")]
    public Player player;
    public PoolManager pool;
    public Spawner spawner;
    public LevelUp uiLevelUp;
    public GameObject uiGameOver;
    public GameObject HUD;
    public WeaponManager weaponManager0;
    public WeaponManager weaponManager1;
    public int money;


    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        curHp = maxHp;
        money = 0;
        Time.timeScale = 0;
    }

    public void GetMoney()
    {
        myMoney += 100;
    }
    public void Stop()
    {
        Time.timeScale = 0;
    }
    public void Resume()
    {
        Time.timeScale = 1;
    }
    public void KillEnemy()
    {
        kill++;
        exp++;
        if (exp == nextExp[level])
        {
            exp = 0;
            level++;
            uiLevelUp.Show();
        }
        if (kill == 10)
        {
            spawner.SpawnBoss();
        }
    }

    public void GameStart()
    {
        Time.timeScale = 1;
    }

    public void GameOver()
    {
        StartCoroutine("GameOverRoutine");
    }

    IEnumerator GameOverRoutine()
    {
        yield return new WaitForSeconds(0.2f);
        uiGameOver.SetActive(true);
        HUD.SetActive(false);
        money += myMoney;
        Stop();
    }

    public void Retry()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }

    public void GoLobby()
    {
        SceneManager.LoadScene("Lobby");
    }
}
