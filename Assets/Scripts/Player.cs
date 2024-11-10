using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Player : MonoBehaviour
{
    public Vector2 inputVec;
    public float speed = 3f;
    public Scanner scanner;
    //public Hand[] hands;

    Rigidbody2D rigid;
    SpriteRenderer spriter;
    Animator anim;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        scanner = GetComponent<Scanner>();
        //hands = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if(!GameManager.instance.isLive)
            return;

        inputVec.x = Input.GetAxisRaw("Horizontal");
        inputVec.y = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        if(!GameManager.instance.isLive)
            return;

        Vector2 nextVec = inputVec.normalized * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
    }

    void LateUpdate()
    {
        if(!GameManager.instance.isLive)
            return;

        anim.SetFloat("Speed", inputVec.magnitude); // ?
    
        if(inputVec.x != 0)
        {
            spriter.flipX = inputVec.x < 0; // 뒤집기!
        }
    }
}

