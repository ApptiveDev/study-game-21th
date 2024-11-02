using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet1 : MonoBehaviour
{
    Rigidbody2D rigid;
    GameObject target;
    Vector3 moveDir;
    float moveSpeed = 3f;
    float damage = 10f;

    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        Enemy enemy = FindObjectOfType<Enemy>();
        if (enemy != null)
        {
            target = enemy.gameObject;
            moveDir = target.transform.position - transform.position;
            moveDir.Normalize();
        }
        rigid.velocity = moveDir * 10f;
        transform.rotation = Quaternion.FromToRotation(Vector3.up, moveDir);
    }

    private void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
        }

        transform.position += moveDir * moveSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();
        if (enemy.gameObject == target)
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }

    }
}
