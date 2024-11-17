using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public float timer;
    public float waitingTime;
    public GameObject abc;
    public void createEnemy()
    {
        if (timer > waitingTime)
        {
            try
            {
                Instantiate(abc, new Vector3(Random.value * 10, Random.value * 10, Random.value * 10), Quaternion.identity);
                timer = 0.0f;
            }
            catch (UnassignedReferenceException)
            {
            }
        }
        else
        {
            timer += Time.deltaTime;
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        waitingTime = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        createEnemy(); 
    }
}
