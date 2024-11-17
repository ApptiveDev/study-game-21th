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
    private bool isAttacking;
    void Awake()
    {
        //animator = GetComponent<Animator>();
        render = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();
        isAttacking = false;
    }
    private void FixedUpdate()
    {
        Vector2 targetVector = target.position - rigid.position;
        Vector2 nextVector = targetVector.normalized * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVector);
        if (enemyId == 1)
        {
            float distance = Vector2.Distance(target.position, rigid.position);
            if(distance < 5 && !isAttacking)
            {
                StartCoroutine("AttackRoutine");
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
    IEnumerable AttackRoutine()
    {
        isAttacking = true;
        Transform bullet = GameManager.instance.pool.Get(2).transform;
        Vector3 targetPos = target.position;
        Vector3 dir = targetPos - transform.position;
        dir = dir.normalized;

        bullet.position = transform.position;
        bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir);
        bullet.GetComponent<Bullet>().Init(0, 1, dir);
        yield return new WaitForSeconds(1f);

        isAttacking = false;
    }
    void Dead()
    {
        gameObject.SetActive(false);
        GameManager.instance.KillEnemy();
        if(enemyId == 3)
        {

        }
    }
}


