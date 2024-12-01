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

    Rigidbody2D rigid;
    SpriteRenderer spriter;
    Animator anim;
    public Transform position;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        scanner = GetComponent<Scanner>(); // Weapon -> Scanner 바로 접근 못하니 플레이어에 Scanner 속성 추가
    }
    void FixedUpdate()
    {
        if(!GameManager.instance.isLive)
            return;

        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(x, y);
        Debug.Log($"Move Direction: {moveDirection}, Speed: {speed}");
        
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

    void OnCollisionStay2D(Collision2D collision)
    {
        if (!GameManager.instance.isLive)
            return;

        // string tag = collision.collider.tag;

        if (collision.gameObject.CompareTag("Enemy"))
        {
            GameManager.instance.health -= Time.deltaTime * 10f;
            
            // health가 0보다 작을 때 사망
            if (GameManager.instance.health <= 0)
            {
                /* 플레이어 자식 오브젝트 모두 비활성화
                for(int index=2; index < transform.childCount; index++){
                    transform.GetChild(index).gameObject.SetActive(false);
                }
                */

                // 프로젝트 내 자식 오브젝트 하나만 존재 -> for 문 작성 필요 x
                transform.GetChild(0).gameObject.SetActive(false);

                GameManager.instance.health = 0;
                // 사망 구현 anim.SetTrigger("Dead");

                GameManager.instance.GameOver(); // 플레이어 사망할 때 게임오버 함수 호출 !
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision) // collider는 충돌하는 동안 trigger는 중돌시 *****
    {
        if (!GameManager.instance.isLive)
            return;

        if (collision.CompareTag("Projectile")){
            GameManager.instance.health -= 10f; // 덜에 맞을때마다 플레잉어체력 10씩 감소함

            if (GameManager.instance.health <= 0)
            {
                transform.GetChild(0).gameObject.SetActive(false);
                GameManager.instance.health = 0;
                GameManager.instance.GameOver();
            }
        }
    }

    public void UpdateSpeed(float newSpeed)
    {
        speed = newSpeed;
        Debug.Log($"Player speed updated to {speed}"); 
    }
}
