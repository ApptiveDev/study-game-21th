using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    float curTime = 0f;
    float spawnPeriod = 1f;

    private void Start()
    {

    }

    private void Update()
    {
        SpawnEnemyPeriodically();
    }

    void SpawnEnemyPeriodically()
    {
        curTime += Time.deltaTime;
        if (curTime >= spawnPeriod)
        {
            MakeRandomEnemy();
            curTime = 0;
        }
    }

    void MakeRandomEnemy()
    {
        Instantiate(enemyPrefab);
        enemyPrefab.transform.position = PickRandomPosition();
        enemyPrefab.GetComponent<SpriteRenderer>().color = PickRandomColor();
    }

    Vector3 PickRandomPosition() // 랜덤한 위치(벡터3)을 반환한다.
    {
        float x = Random.Range(-3f, 3f);
        float y = Random.Range(-3f, 3f);

        return new Vector3(x, y, 0);
    }

    Color PickRandomColor() // 랜덤한 색깔을 반환한다.
    {
        float r = Random.Range(0, 1f);
        float g = Random.Range(0, 1f);
        float b = Random.Range(0, 1f);

        return new Color(r, g, b);
    }
}
