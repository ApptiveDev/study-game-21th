using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class Stone : MonoBehaviour
{
    public float damage = 5f;
    Rigidbody2D rb;
    Vector3 dir;
    void Start()
    {
        dir = new Vector3(Random.Range(-300f, 300f), 600f, 0f);
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(dir, ForceMode2D.Force);
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
            }
        }
    }
}
