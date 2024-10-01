using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrefab;
    public float moveSpeed = 1f;
    private Transform player;

    public int maxEnemies = 5;

    private static bool enemiesSpawned = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        if (!enemiesSpawned)
        {
            for (int i = 0; i < maxEnemies; i++)
            {
                Vector3 spawnPosition = new Vector3(Random.Range(-10f, 10f), 0, Random.Range(-10f, 10f));
                Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
            }
            enemiesSpawned = true;
        }
    }

    void Update()
    {
        if (player != null)
        {
            Vector3 direction = player.position - transform.position;
            direction.Normalize();

            transform.position += direction * moveSpeed * Time.deltaTime;
        }
    }
}
