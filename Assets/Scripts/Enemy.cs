using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{ // 경험치 구현.. 경험치 매니저 추후 고민?..?
    public float health = 10f;
    public GameObject expPrefab;
    public float speed = 1f;

    private Transform player;

    void Start()
    {
        player = GameManager.instance.player.transform;
    }

    void Update()
    {
        if(player != null)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            health -= collision.GetComponent<Bullet>().damage;

            if(health <= 0)
            {
            Vector3 spawnExpPosition = transform.position;   
            Instantiate(expPrefab, spawnExpPosition, Quaternion.identity);
            Destroy(gameObject);
            }
        }
    }
}
