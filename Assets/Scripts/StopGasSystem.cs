using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopGasSystem : MonoBehaviour
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
