using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{

    void Start()
    {
        
    }

    int PlayerHP = 3;
    void Update()
    {
        Debug.Log("Player HP : " + PlayerHP);
        if (PlayerHP <= 0) Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy")) TakeDamage(1);
    }

    void TakeDamage (int damage)
    {
        PlayerHP -= damage;
    }
}
