using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole2System : MonoBehaviour
{
    float curTimeBlackHole2;

    void Update()
    {
        curTimeBlackHole2 += Time.deltaTime;
        if (curTimeBlackHole2 >= 2) Destroy(gameObject);
    }
}
