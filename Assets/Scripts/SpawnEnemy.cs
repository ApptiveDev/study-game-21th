using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField] GameObject EnemyPrefab;
    float curTime = 0;
    void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            Instantiate(EnemyPrefab);
            EnemyPrefab.transform.position = RandomPosition();
        }
    }

    void Update()
    {
        curTime += Time.deltaTime;
        if (curTime >= 2)
        {
            Spawn();
            curTime = 0;
        }
    }
    void Spawn()
    {
        Instantiate(EnemyPrefab);
        EnemyPrefab.transform.position = RandomPosition();
    }

    Vector3 RandomPosition()
    {
        float x = Random.Range(-20f, 20f);
        float y = Random.Range(-20f, 20f);

        return new Vector3(x, y, 0);
    }
}
