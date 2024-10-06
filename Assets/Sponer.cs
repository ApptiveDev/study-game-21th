using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sponer : MonoBehaviour
{
    // Start is called before the first frame update
    public float timer;
    public float waitingTime;
    public GameObject enemy;
    public void createEnemy()
    {
        if (timer > waitingTime)
            try
            {
                Vector3 MyPosition = transform.position;
                Instantiate(enemy, new Vector3(MyPosition.x, MyPosition.y, MyPosition.z), Quaternion.identity);
                timer = 0.0f;
            }
            catch (UnassignedReferenceException)
            {
            }
        else
        {
            timer += Time.deltaTime;
        }

    }
    void Start()
    {
        waitingTime = 2.0f;
    }

    // Update is called once per frame
    void Update()
    {
        createEnemy();
    }
}
