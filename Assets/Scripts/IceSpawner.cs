using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceMaker : MonoBehaviour
{   
    [SerializeField] GameObject iceObject;
    [SerializeField] float spawnDelay = 2.5f;
    float currentDelay = 0f;

    void Update()
    {
        currentDelay += Time.deltaTime;

        if (currentDelay >= spawnDelay) {
            Instantiate(iceObject, transform.position, Quaternion.identity);
            currentDelay = 0f;
        }
    }
}
