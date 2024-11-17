using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; 
    public Transform player; 
    public float spawnInterval = 10f; 
    private bool isSpawning = true; 

    void Start()
    {
        StartCoroutine(SpawnEnemies()); 
    }

    
    IEnumerator SpawnEnemies()
    {
        while (isSpawning)
        {
            SpawnEnemy(); // Àû »ý¼º
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnEnemy()
    {
        
        GameObject enemy = Instantiate(enemyPrefab, GetRandomPosition(), Quaternion.identity);
        enemy.GetComponent<EnemyAI>().player = player; 
    }

    Vector2 GetRandomPosition()
    {
        
        return new Vector2(Random.Range(-20f, 20f), Random.Range(-20f, 20f));
    }
}




