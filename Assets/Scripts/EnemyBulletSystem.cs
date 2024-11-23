using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyBulletSystem : MonoBehaviour
{
    Vector3 direction;
    float Speed = 10f;
    GameObject player;

    private void Start()
    {
        player = GameObject.Find("Player");
    }

    void Update()
    {
        BulletMove();
    }

    public void BulletDirection(Quaternion playerRotation)
    {
        direction = playerRotation * Vector3.up;
    }

    void BulletMove()
    {
        transform.position += direction * Speed * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) 
        {
            PlayerCollision playerCollision = player.GetComponent<PlayerCollision>();
            playerCollision.TakeDamage(1);
            Destroy(gameObject);
        }
    }
}
