using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject circleObject;
    float curTime = 0;
    // Start is called before the first frame update
    void Start()
    {
        //for(int i = 0; i < 10; i++)
        //{
        //    float x = Random.Range(-3f, 3f);
        //    float y = Random.Range(-3f, 3f);
            
        //    Vector3 newPosition = new Vector3(x, y, 0);

        //    float r = Random.Range(0, 1f);
        //    float g = Random.Range(0, 1f);
        //    float b = Random.Range(0, 1f);

        //    Color newColor = new Color(r, g, b);

        //    Instantiate(circleObject,newPosition,Quaternion.identity);
        //    circleObject.GetComponent<SpriteRenderer>().color = newColor;
        //}
    }

    //코루틴 << 
    // Update is called once per frame
    void Update()
    {
        curTime += Time.deltaTime;
        if(curTime >= 1)
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
            curTime = 0;
        }
        //1초당 한번씩 만들고싶어요
    }
}
