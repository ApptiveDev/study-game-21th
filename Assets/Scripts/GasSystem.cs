using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasSystem : MonoBehaviour
{
    float curTimeStopGas;
    void Update()
    {
        curTimeStopGas += Time.deltaTime;

        if (curTimeStopGas >= 0.5f)
        {
            Destroy(gameObject);
        }
    }
}
