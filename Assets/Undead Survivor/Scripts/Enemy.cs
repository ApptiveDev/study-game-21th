using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    SpriteRenderer render;
    Rigidbody2D rigid;
    public float speed;
    public float health;
    public Rigidbody2D target;
    public Collider2D coll;
    public int enemyId;
    public float attackSpeed;
    float timer;
    void Awake()
    {
        //animator = GetComponent<Animator>();
        render = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        Vector2 targetVector = target.position - rigid.position;
        Vector2 nextVector = targetVector.normalized * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVector);
        timer += Time.deltaTime;
        if (enemyId == 1)
        {
            float distance = Vector2.Distance(target.position, rigid.position);
            if(distance < 15f && attackSpeed < timer)
            {
                timer = 0;
                Attack();
            }
        }
    }
    private void LateUpdate()
    {
        render.flipX = target.position.x < rigid.position.x;
    }

    private void OnEnable()
    {
        target = GameManager.instance.player.GetComponent<Rigidbody2D>();
        health = 10f;
        speed = 2f;
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
    void Attack()
    {
        Transform bullet = GameManager.instance.pool.Get(6).transform;
        Vector3 targetPos = target.position;
        Vector3 dir = targetPos - transform.position;
        dir = dir.normalized;

        bullet.position = transform.position;
        bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir);
        bullet.GetComponent<Arrow>().Init(1, dir);
    }
    void Dead()
    {
        gameObject.SetActive(false);
        GameManager.instance.KillEnemy();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Aura"))
        {
            speed = 4f;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Aura"))
        {
            speed = 2f;
        }
    }
}


