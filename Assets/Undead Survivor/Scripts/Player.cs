using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    SpriteRenderer render;
    Rigidbody2D rigid;
    Animator animator;
    public float speed;
    public Vector2 inputVector;
    public Scanner scanner;
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        render = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        scanner = GetComponent<Scanner>();
    }

    void Update()
    {
        inputVector.x = Input.GetAxisRaw("Horizontal");
        inputVector.y = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        Vector2 nextVector = inputVector.normalized * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVector);
    }
    private void LateUpdate()
    {
        animator.SetFloat("Speed", inputVector.magnitude);
        if(inputVector.x != 0)
        {
            render.flipX = inputVector.x < 0;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        GameManager.instance.curHp -= Time.deltaTime * 20;

        if(GameManager.instance.curHp < 0)
        {
            Dead();
        }
    }

    public void Dead()
    {
        animator.SetTrigger("Dead");
        GameManager.instance.GameOver();
    }
}
