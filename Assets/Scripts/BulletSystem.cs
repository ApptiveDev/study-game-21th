using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BulletSystem : MonoBehaviour
{
    Vector3 direction;
    float Speed = 15f;

    void Start()
    {
        
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
        if (collision.gameObject.CompareTag("Enemy")) Destroy(gameObject);
    }
}
