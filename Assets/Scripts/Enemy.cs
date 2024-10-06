using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        float playerX = GameObject.FindWithTag("Player").GetComponent<Transform>().position.x;
        float playerY = GameObject.FindWithTag("Player").GetComponent<Transform>().position.y;
        float targetX = playerX - transform.position.x;
        float targetY = playerY - transform.position.y;
        Vector3 moving = new Vector3(targetX, targetY);

        transform.position += moving * Time.deltaTime;
    }
}
