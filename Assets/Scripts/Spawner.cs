using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject slime;

    private float curTime = 0;
    private const float SPAWN_DELAY = 1f;

    void Update()
    {
        curTime += Time.deltaTime;
        if (curTime >= SPAWN_DELAY)
        {
            Instantiate(slime, new Vector3(
                    Random.Range(-5, 5),
                    Random.Range(-5, 5),
                    0
                ), Quaternion.identity);
            curTime = 0;
        }
    }
}
