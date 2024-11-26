using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject meleeEnemyPrefab;
    public GameObject rangerEnemyPrefab;
    public float spawnInterval = 3f;
    public float spawnRadius = 10f;
    
    [Range(0f, 1f)] public float rangerSpawnChance = 0.3f;
    private Transform player;


    // 코루틴 구현
    // void Start()
    // {
    //     player = GameManager.instance.player.transform;
    //     StartCoroutine(SpawnEnemies());
    // }

    // IEnumerator SpawnEnemies()
    // {
    //     while(GameManager.instance.isLive)
    //     {
    //         Vector3 spawnPosition = new Vector3(player.position.x + Random.Range(-spawnRadius, spawnRadius), player.position.y + Random.Range(-spawnRadius, spawnRadius), 0);
            
    //         GameObject enemyPrefab = Random.value > 0.5f ? meleeEnemyPrefab : rangerEnemyPrefab;
    //         Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
            
    //         yield return new WaitForSeconds(spawnInterval);
    //     }
    // }
}
