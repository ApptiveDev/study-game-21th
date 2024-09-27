using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
    private GameObject player;
    private float speed = 2f;

    void Start()
    {
        player = GameObject.Find("Player");    
    }

    private void MoveToPlayer()
    {
        Vector3 deltaPosition = player.transform.position - transform.position;
        Vector3 direction = deltaPosition.normalized;
        transform.position += direction * speed * Time.deltaTime;
    }

    void Update()
    {
        MoveToPlayer();
    }
}
