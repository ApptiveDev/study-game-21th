using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpSystem : MonoBehaviour
{
    GameObject player;
    const float DrainDistance = 2f;
    const float speed = 10f;
    void Start()
    {
        player = GameObject.Find("Player");
    }

    void Update()
    {
        ExpMovement();
    }


    void ExpMovement()
    {
        if (Mathf.Abs(player.transform.position.x - transform.position.x) <= DrainDistance && Mathf.Abs(player.transform.position.y - transform.position.y) <= DrainDistance)
        {
            transform.position += Vector3.right * (player.transform.position.x - transform.position.x) * Mathf.Abs((1 / (player.transform.position.x - transform.position.x))) * speed * Time.deltaTime;
            transform.position += Vector3.up * (player.transform.position.y - transform.position.y) * Mathf.Abs((1 / (player.transform.position.y - transform.position.y))) * speed * Time.deltaTime;
        }
    }
}
