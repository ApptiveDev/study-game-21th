using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrefab;
    public float moveSpeed = 1f;
    private Transform player;



    public float health = 10f;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (player != null)
        {
            Vector3 direction = player.position - transform.position;
            direction.Normalize(); // 방향 사용하려고 Normalize

            transform.position += direction * moveSpeed * Time.deltaTime;
        }
    }
}
