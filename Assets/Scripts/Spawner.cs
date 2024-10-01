using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    GameObject circleObject;

    float curTime = 0;
    void Start()
    {
        for (int i = 0; i < 10; i++) {
            Instantiate(circleObject); 
            circleObject.transform.position = PickRandomPosition();
            circleObject.GetComponent<SpriteRenderer>().color = pickRandomColor();
        }
    }

    void Update()
    {
        curTime += Time.deltaTime;
        if (curTime > 1) {
            MakeRandomCircle();
            curTime = 0;
        }
    }

    Vector3 PickRandomPosition() {
        float x = Random.Range(-3f, 3f);
        float y = Random.Range(-3f, 3f);

        return new Vector3(x, y, 0);
    }

    Color pickRandomColor() {
        float r = Random.Range(0, 1f);
        float g = Random.Range(0, 1f);
        float b = Random.Range(0, 1f);

        return new Color(r, g, b);
    }

    void MakeRandomCircle() {
        Instantiate(circleObject);
        circleObject.transform.position = PickRandomPosition();
        circleObject.GetComponent<SpriteRenderer>().color = pickRandomColor();
    }
}
