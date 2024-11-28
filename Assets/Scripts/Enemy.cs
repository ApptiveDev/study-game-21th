using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour // 적 하나에 적용, 애니메이터 추가 -> 움직이지 않는 에러 발생 / 일단 주석 처리
{
    public float speed = 1f;
    public float health;
    public float maxHealth;
    //public RuntimeAnimatorController[] animCon;
    
    
    public Rigidbody2D target;

    bool isLive;

    Rigidbody2D rigid;
    Collider2D coll;
    Animator anim;
    SpriteRenderer spriter; 
    WaitForFixedUpdate wait; // 코루틴을 위한 변수 -> 보통 기다리는 시간 변수로

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
        spriter = GetComponent<SpriteRenderer>();
        wait = new WaitForFixedUpdate();
    }
    void FixedUpdate()
    {
        if(!GameManager.instance.isLive) // 죽었을 때 -> isLive 상태 player, enemy 동일하게 적용
            return;

        // IsNmae : 해당 상태 이름 확인하는 함수, GetCur.. 현재 애니메이터 레이어 인덱스
        if(!isLive || anim.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
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
        isLive = true;

        // 몬스터 오브젝트 재활용 하기 위해 OnTigger에서 설정해준 거 OnEnable에서 되돌리기
        coll.enabled = true;
        rigid.simulated = true; // 리지드바디 물리적 비활성화는 .simulated = false. . SetActive가 아니야
        spriter.sortingOrder = 2;
        anim.SetBool("Dead", false);

        health = maxHealth;
    }

    public void Init(SpawnData data)
    {
        //anim.runtimeAnimatorController = animCon[data.spriteType];

        speed = data.speed;
        maxHealth = data.health;
        health = data.health;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(!collision.CompareTag("Weapon") || !isLive) // 두번 충돌할 때 경험치 중복 방지 -> isLive 조건 추가
            return;

        health -= collision.GetComponent<Bullet>().damage;
        StartCoroutine(KnockBack());

        if(health > 0) {
            anim.SetTrigger("Hit"); // 애니메이터의 SetTrigger 함수 호출해서 피격 때 상태 변경
        }
        else {
            isLive = false;
            coll.enabled = false;
            rigid.simulated = false; // 리지드바디 물리적 비활성화는 .simulated = false. . SetActive가 아니야
            // spriter.sortingOrder = 1; // 죽었을 때 다른 오브젝트 가리지 않게 레이어 낮추기 (원래 2)
            anim.SetBool("Dead", true);
            Dead();

        }

        IEnumerator KnockBack()
        {
            // 코루틴의 반환 키워드 : yield
            yield return wait; // 다음 하나의 물리 프레임 딜레이
            Vector3 playerPos = GameManager.instance.player.transform.position;
            Vector3 dirVec = transform.position - playerPos;
            rigid.AddForce(dirVec.normalized * 3, ForceMode2D.Impulse); // 순간적인 힘 : Impulse
        }


        void Dead()
        {
            gameObject.SetActive(false);
            GameManager.instance.kill++;
            GameManager.instance.GetExp();
        }
    }
}
