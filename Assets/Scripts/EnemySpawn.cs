using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnInterval = 3f;
    public float spawnRadius = 10f;
    
    private Transform player;
    void Start()
    {
        player = GameManager.instance.player.transform;
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while(GameManager.instance.isLive)
        {
            Vector3 spawnPosition = player.position + (Vector3)(Random.insideUnitCircle * spawnRadius);
            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
