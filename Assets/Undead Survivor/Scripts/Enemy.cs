using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Animator animator;
    SpriteRenderer renderer;
    Rigidbody2D rigid;
    public float speed;
    public Rigidbody2D target;

    void Awake()
    {
        animator = GetComponent<Animator>();
        renderer = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();
        target = GameManager.instance.player.GetComponent<Rigidbody2D>();
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
}
