using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] float spawnDelay = 3f;
    [SerializeField] int spawnAmount = 2;
    [SerializeField] GameObject enemyObject;
    private void Start()
    {

    }

    float currentDelay = 0f;

    void Update()
    {
        if (currentDelay < spawnDelay) {
            currentDelay += Time.deltaTime;
        } else {
            for (int i = 0; i < spawnAmount; i++) {
                currentDelay = 0f;
                SpawnObject();
            }
        }
    }

    void SpawnObject() {
        Instantiate(enemyObject, GetRndPos(), Quaternion.identity);
    }

    Vector3 GetRndPos() {
        float x = Random.Range(-5f, 5f);
        float y = Random.Range(-5f, 5f);
        Vector3 rndPos = new Vector3(x, y, 0);
        return rndPos;
    }
}