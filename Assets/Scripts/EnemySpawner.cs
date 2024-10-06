using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemy;

    public float spawnRate = 3f;
    public int maxSpawnCount = 3;

    private int spawnCount = 0;
    private float curTime = 0f;

    // Update is called once per frame
    void Update()
    {
        curTime += Time.deltaTime;

        // 적이 3개보다 적을 때만 생성
        if (spawnCount < maxSpawnCount)
        {
            // spawnRate(초)마다 적을 생성
            if (curTime >= spawnRate)
            {
                Instantiate(enemy);
                curTime = 0f; // 타이머를 초기화해서 다시 spawnRate만큼 기다림
                spawnCount++;
            }
        }
    }
}
