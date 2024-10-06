using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowSpawner : MonoBehaviour
{
    [SerializeField] private GameObject arrow;

    private float curTime = 0;
    private const float SPAWN_DELAY = 1f;

    void Update()
    {
        curTime += Time.deltaTime;
        if (curTime >= SPAWN_DELAY)
        {
            GameObject game = Instantiate(arrow, transform.position, Quaternion.identity);
            curTime = 0;
        }
    }
}
