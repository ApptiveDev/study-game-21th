using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoint;
    public float spawnTime = 0.5f;
    public RectTransform bossText;
    float timer;
    private bool bossAppeared;
    private void Awake()
    {
        //자식 point의 transform 가져옴
        spawnPoint = GetComponentsInChildren<Transform>();
    }
    void Update()
    {
        timer += Time.deltaTime;

        if(timer > spawnTime)
        {
            Spawn();
            timer = 0;
        }
    }

    void Spawn()
    {
        GameObject enemy = null;
        int enemyId = Random.Range(0, 2);
        if(enemyId == 0)
        {
            enemy = GameManager.instance.pool.Get(0);
        } else
        {
            enemy = GameManager.instance.pool.Get(4);
        }
        
        enemy.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position;
    }
    public void SpawnBoss()
    {
        StartCoroutine(ShowBossText());
        GameObject enemy = GameManager.instance.pool.Get(5);
        enemy.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position;
    }

    IEnumerator ShowBossText()
    {
        bossText.localScale = Vector3.one;
        yield return new WaitForSeconds(2f);
        bossText.localScale = Vector3.zero;
    }
}
