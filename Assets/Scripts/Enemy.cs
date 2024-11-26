using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour // 적 하나에 적용
{
    public float speed = 1f;
    public Rigidbody2D target;

    bool isLive = true;

    Rigidbody2D rigid;
    SpriteRenderer spriter; 

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
    }
    void FixedUpdate()
    {
        if(!isLive)
            return;

        Vector2 direction = ( target.position - rigid.position ).normalized * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + direction); // 현재 위치 + direction 함으로써 화면 중앙에만 생성문제 해결

        rigid.velocity = Vector2.zero;
    }

    void LateUpdate()
    {
        if(!isLive)
            return;
        
        spriter.flipX = target.position.x < rigid.position.x;
    }

    void OnEnable() // 스크립트가 활성화될 때 호출되는 이벤트 함수 OnEnable / target 지정 위해
    {
        target = GameManager.instance.player.GetComponent<Rigidbody2D>();
    }


}
