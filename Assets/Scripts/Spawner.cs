using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject circleObject;
    float curTime = 0;
    int num = 0;
    // Start is called before the first frame update

    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        curTime += Time.deltaTime;
        if(curTime >= 1 && num <10)
        {
            MakeRandomEnemy();
            curTime = 0;
            num++;
        }
        
    }
    void MakeRandomEnemy()
    {
        Instantiate(circleObject);
        circleObject.transform.position = PickRandomPosition();
        circleObject.GetComponent<SpriteRenderer>().color = PickRandomColor();
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
