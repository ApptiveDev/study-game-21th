using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("#Player Info")]
    public int level;
    public int kill;
    public int exp;
    public int[] nextExp = { 5, 10, 15, 20, 25, 30};

    [Header("#Game Object")]
    public Player player;
    public PoolManager pool;
    public LevelUp uiLevelUp;


    private void Awake()
    {
        instance = this;
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
}
