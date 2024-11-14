using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private GameObject chasingEnemyPrefab;
    private GameObject rangeEnemyPrefab;
    private GameObject slimeBossPrefab;

    private float chasingSpawnPeriod = 1f;
    private float rangeSpawnPeriod = 1.5f;

    private void Start()
    {
        chasingEnemyPrefab = GameManager.Instance.GetChasingEnemyPrefab();
        rangeEnemyPrefab = GameManager.Instance.GetRangeEnemyPrefab();
        slimeBossPrefab = GameManager.Instance.GetSlimeBossPrefab();

        StartCoroutine(SpawnChasingEnemyPeriodically());
        StartCoroutine(SpawnRangeEnemyPeriodically());
    }

    IEnumerator SpawnChasingEnemyPeriodically()
    {
        while (true)
        {
            yield return new WaitForSeconds(chasingSpawnPeriod);
            MakeEnemy(chasingEnemyPrefab);
        }
    }

    IEnumerator SpawnRangeEnemyPeriodically()
    {
        while (true)
        {
            yield return new WaitForSeconds(rangeSpawnPeriod);
            MakeEnemy(rangeEnemyPrefab);
        }
    }
    private void MakeEnemy(GameObject enemyPrefab)
    {
        GameObject enemy = Instantiate(enemyPrefab);
        enemy.transform.position = PickRandomPosition();
        enemy.GetComponent<SpriteRenderer>().color = PickRandomColor();
    }

    private Vector3 PickRandomPosition() // 랜덤한 위치(벡터3)을 반환한다.
    {
        float x = Random.Range(-3f, 3f);
        float y = Random.Range(-3f, 3f);

        return new Vector3(x, y, 0);
    }

    private Color PickRandomColor() // 랜덤한 색깔을 반환한다.
    {
        float r = Random.Range(0, 1f);
        float g = Random.Range(0, 1f);
        float b = Random.Range(0, 1f);

        return new Color(r, g, b);
    }

    public void SpawnSlimeBoss()
    {
        MakeEnemy(slimeBossPrefab);
    }
}
