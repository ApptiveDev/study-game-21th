using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject enemyObject;

    float curTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Update()
    {
        curTime += Time.deltaTime;
        if (curTime >=1)
        {
            Instantiate(enemyObject);
            enemyObject.transform.position = PickRandomPosition();

            curTime = 0;
        }
    }

    Vector3 PickRandomPosition()
    {
        float x = Random.Range(-3f, 3f);
        float y = Random.Range(-3f, 3f);

        return new Vector3(x, y, 0);
    }
}
