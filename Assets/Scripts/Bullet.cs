using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;
    public int per;

    Rigidbody2D rigid; // 원거리 공격 -> 속도 -> 리지드바디 속성

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }
    public void Init(float damage, int per, Vector3 dir) // 해당클래스의 변수로 접근
    {
        this.damage = damage;
        this.per = per;

        if(per > -1){ // 관통이 -1 (무한)보다 큰 것에 대해 속도 적용
            rigid.velocity = dir * 15f; // 발사 속도 15 !
        }

    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if(!collision.CompareTag("Enemy") || per == -1) // 이너미에 닿지 않았거나 per가 -1이면 적용 X
            return;

        if(collision.CompareTag("Enemy"))
        {
            per--;

            if(per == -1){
                rigid.velocity = Vector2.zero; // 속도 초기화
                gameObject.SetActive(false);
            }
        }
    }
}
