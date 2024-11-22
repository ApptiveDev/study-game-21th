using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    
    public float speed = 3f;
    public float damage = 1f;
    public Vector3 dir;
    GameObject target;
    void Start()
    {
       
    }

    void Update()
    {
        if(dir != Vector3.zero)
        {
            transform.position += dir * speed * Time.deltaTime;
        }
    }

    public void SetDirection(Vector3 direction)
    {
        dir = direction;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            EnemyBase enemy = collision.GetComponent<EnemyBase>();
            if(enemy != null)
            {
                enemy.health -= damage;
                if(enemy.health <= 0)
                {
                    Destroy(collision.gameObject);
                }
                Destroy(gameObject);
            }
        }
    }

}
