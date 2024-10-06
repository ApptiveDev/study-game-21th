using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject enemyObject;


    float curTime = 0;
    int enemyCount = 0;

    private void Start()
    {
       
    }

    private void Update()
    {

        curTime += Time.deltaTime;
        if (curTime >= 1 && enemyCount < 5)
        {
            MakeRandomEnemy();
            curTime = 0;
            ++enemyCount;
        }
    }

    void MakeRandomEnemy() 
    {
        Instantiate(enemyObject);
        enemyObject.transform.position = PickRandomPosition();
        enemyObject.GetComponent<SpriteRenderer>().color = PickRandomColor();
    }

    Vector3 PickRandomPosition() 
    {
        float x = Random.Range(-3f, 3f);
        float y = Random.Range(-3f, 3f);

        return new Vector3(x, y, 0);
    }

    Color PickRandomColor() 
    {
        float r = Random.Range(0, 1f);
        float g = Random.Range(0, 1f);
        float b = Random.Range(0, 1f);

        return new Color(r, g, b);
    }
}
