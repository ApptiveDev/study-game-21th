using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] GameObject circleObject;
    void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            float x = Random.Range(-3f, 3f);
            float y = Random.Range(-3f, 3f);

            Vector3 newPosition = new Vector3(x, y, 0);

            float r = Random.Range(0, 1f);
            float g = Random.Range(0, 1f);
            float b = Random.Range(0, 1f);

            Color newColor = new Color(r, g, b);

            Instantiate(circleObject, newPosition, Quaternion.identity);
            circleObject.GetComponent<SpriteRenderer>().color = newColor;
        }
    }


    void Update()
    {
        float curTime = 0;
        curTime += Time.deltaTime;
        Time.deltaTime;
    }
}