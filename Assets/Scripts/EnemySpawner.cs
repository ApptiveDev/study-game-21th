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
        if (currentDelay < spawnDelay) { // 현재 지연 시간이 설정된 생성 지연 시간보다 작으면
            currentDelay += Time.deltaTime; // 지연 시간 증가시킴
        } else {
            for (int i = 0; i < spawnAmount; i++) {
                currentDelay = 0f; // 지연 시간 초기화
                SpawnObject(); // 적 개체 생성
            }
        }
    }

    void SpawnObject() { // 적 개체 생성 함수
        Instantiate(enemyObject, GetRndPos(), Quaternion.identity);
    }

    Vector3 GetRndPos() { // 랜덤 위치 반환 함수
        float x = Random.Range(-5f, 5f);
        float y = Random.Range(-5f, 5f);
        Vector3 rndPos = new Vector3(x, y, 0);
        return rndPos;
    }
}