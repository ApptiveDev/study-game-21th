using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowSpawner : MonoBehaviour
{
    [SerializeField] GameObject arrowObject;

    float spawnDelay = 0.5f;

    float currentDelay = 0f;

    private void Update()
    {
        //매 프레임 시간을 잰다.
        currentDelay += Time.deltaTime;
        
        //스폰 시간이 되었을 때
        if(currentDelay >= spawnDelay)
        {
            //화살을 생성한다.
            Instantiate(arrowObject,transform.position,Quaternion.identity);
            currentDelay = 0f; // 시간을 0초로 만든다.
        }
    }
}
