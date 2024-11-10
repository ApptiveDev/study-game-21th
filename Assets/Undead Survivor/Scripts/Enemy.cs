using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Animator animator;
    SpriteRenderer renderer;
    Rigidbody2D rigid;
    public float speed;
    public float health;
    public Rigidbody2D target;
    public Collider2D coll;

    void Awake()
    {
        animator = GetComponent<Animator>();
        renderer = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();
        
    }
    private void FixedUpdate()
    {
        Vector2 targetVector = target.position - rigid.position;
        Vector2 nextVector = targetVector.normalized * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVector);
    }
    private void LateUpdate()
    {
        renderer.flipX = target.position.x < rigid.position.x;
    }

    private void OnEnable()
    {
        target = GameManager.instance.player.GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Bullet")) return;
        health -= collision.GetComponent<Bullet>().damage;

        if(health <= 0)
        {
            Dead();
            Transform exp = GameManager.instance.pool.Get(3).transform;
            exp.position = transform.position;
        }
    }

    void Dead()
    {
        gameObject.SetActive(false);
    }
}


