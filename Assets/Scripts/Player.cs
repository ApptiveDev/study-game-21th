using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.XR;

public class Player : MonoBehaviour
{
    public float speed = 3f;
    private Vector2 moveDirection = Vector2.zero;
    private float x = 0f;
    private float y = 0f;
    public Scanner scanner; // 스크립트도 컴포넌트로 가질 수 있어
    //public Hand[] hands;

    Rigidbody2D rigid;
    SpriteRenderer spriter;
    Animator anim;
    Transform position;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        scanner = GetComponent<Scanner>(); // Weapon -> Scanner 바로 접근 못하니 플레이어에 Scanner 속성 추가
        //hands = GetComponent<Rigidbody2D>();
    }
    void FixedUpdate()
    {
        if(!GameManager.instance.isLive)
            return;

        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(x, y);

        rigid.position += moveDirection * speed * Time.fixedDeltaTime;
    
        
    }
    void LateUpdate()
    {
        if(!GameManager.instance.isLive)
            return;
    
        if(x != 0)
        {
            spriter.flipX = x < 0; // 뒤집기!
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!GameManager.instance.isLive)
            return;

        // string tag = collision.collider.tag;

        if (collision.collider.CompareTag("Coin"))
        {
            GameDataManager.AddCoins(1);

            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            GameManager.instance.health -= 10f;

            if (GameManager.instance.health <= 0)
            {
                GameManager.instance.health = 0;
                GameManager.instance.GameOver();
            }
        }
    }
}
