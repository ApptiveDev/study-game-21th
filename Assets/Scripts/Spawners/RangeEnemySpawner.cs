using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeEnemySpawner : MonoBehaviour
{
    float spawnDelay;
    int spawnAmount = 1;
    [SerializeField] GameObject RangeEnemyObject;
    void Start()
    {
        spawnDelay = Random.Range(3f, 5f); 
    }

    float currentDelay = 0f;

    void Update()
    {
        if (currentDelay < spawnDelay) {
            currentDelay += Time.deltaTime;
        } else{
            for (int i = 0; i < spawnAmount; i++) {
                currentDelay = 0f;
                SpawnRangeEnemy();
            }
        }
    }

    void SpawnRangeEnemy() {
        Instantiate(RangeEnemyObject, GetRndPos(), Quaternion.identity);
    }

    Vector3 GetRndPos() {
        float x = Random.Range(-5f, 5f);
        float y = Random.Range(-5f, 5f);
        Vector3 rndPos = new Vector3(x, y, 0);
        return rndPos;
    }
}
