using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    SpriteRenderer renderer;
    Rigidbody2D rigid;
    Animator animator;
    public float speed;
    public Vector2 inputVector;
    public Scanner scanner;
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        renderer = GetComponent<SpriteRenderer>();
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
            renderer.flipX = inputVector.x < 0;
        }
    }

}
