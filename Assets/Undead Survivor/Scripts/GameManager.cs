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


    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        curHp = maxHp;
        Time.timeScale = 0;
    }

    public void GetExp()
    {
        exp++;
        if(exp == nextExp[level])
        {
            exp = 0;
            level++;
            uiLevelUp.Show();
        }
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
        if(kill == 10)
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
        uiGameOver.SetActive(true);
        HUD.SetActive(false);
        Stop();
        //StartCoroutine("GameOverRoutine");
    }

    IEnumerable GameOverRoutine()
    {
        yield return new WaitForSeconds(0.5f);
        
    }

    public void Retry()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }
}
