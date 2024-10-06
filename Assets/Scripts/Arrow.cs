using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private GameObject enemy;
    private float speed = 1f;

    void Start()
    {
        enemy = GameObject.FindGameObjectWithTag("Enemy");
    }

    private void MoveToEnemy()
    {
        try
        {
            Vector3 enemyPosition = enemy.transform.position;
            Vector3 deltaPosition = enemyPosition - transform.position;
            Vector3 direction = deltaPosition.normalized;
            transform.position += direction * speed * Time.deltaTime;
        } catch (MissingReferenceException)
        {
            enemy = GameObject.FindGameObjectWithTag("Enemy");
        }

    }

    void Update()
    {
        MoveToEnemy();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
