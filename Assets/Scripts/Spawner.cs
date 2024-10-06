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

    Vector3 PickRandomPosition() // ������ ��ġ(����3)�� ��ȯ�Ѵ�.
    {
        float x = Random.Range(-3f, 3f);
        float y = Random.Range(-3f, 3f);

        return new Vector3(x, y, 0);
    }

    Color PickRandomColor() // ������ ������ ��ȯ�Ѵ�.
    {
        float r = Random.Range(0, 1f);
        float g = Random.Range(0, 1f);
        float b = Random.Range(0, 1f);

        return new Color(r, g, b);
    }
}
