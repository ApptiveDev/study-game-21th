using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowSpawner : MonoBehaviour

{
    [SerializeField] GameObject arrowObject;

    float spawnDelay = 2f;

    float currentDelay = 0f;

    private void Update() {
        // 매 프레임 시간을 잼
        currentDelay += Time.deltaTime; // deltaTime: 프레임 간의 시간 차이

        // 스폰 시간이 되었을 때
        if(currentDelay >= spawnDelay) {
            Instantiate(arrowObject, transform.position, Quaternion.identity); // 화살을 생성
            currentDelay = 0f;
        }
    }
}
