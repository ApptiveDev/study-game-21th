using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyBossSystem : MonoBehaviour
{
    GameObject player;
    void Start()
    {
        player = GameObject.Find("Player");
    }

    float speed = 1.5f;
    void Update()
    {
        transform.position += Vector3.right * (player.transform.position.x - transform.position.x) * Mathf.Abs((1/(player.transform.position.x - transform.position.x))) * speed * Time.deltaTime;
        transform.position += Vector3.up * (player.transform.position.y - transform.position.y) * Mathf.Abs((1/(player.transform.position.y - transform.position.y))) * speed * Time.deltaTime;
    }
}
