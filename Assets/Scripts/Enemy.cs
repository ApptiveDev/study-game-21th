using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{   
    public float speed;
    public float health;
    public float maxHealth;
    public Rigidbody2D target;

    bool isLive; // 살아있는지를 확인

    Rigidbody2D rigid; // 위치이동을 위해 rigid 변수 생성
    Collider2D coll; // 죽었을때 비활성화 되도록 하기 위한 Collider2D 변수
    Animator anim; // 받은 애니매이션들을 저장할 변수
    SpriteRenderer spriter; // 
    WaitForFixedUpdate wait;

    // Update is called once per frame
    void Awake() 
    {
        rigid = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
        spriter = GetComponent<SpriteRenderer>();
        wait = new WaitForFixedUpdate();
    }

    void Update()
    {
        if (!GameManager.instance.isLive) {
            return; // isLive가 false이면(시간이 멈추면) 동작하지 못하도록 조건 추가
        }

        if (!isLive) // 살아있지 않다면 아래의 코드를 실행하지 않는다
            return;

        if (target != null) // 플레이어가 존재하는지 확인
        {
            Vector3 enemyVec = (target.transform.position - transform.position).normalized;
            transform.position += enemyVec * speed * Time.deltaTime;
        }
    }

    // Enemy의 target을 설정해야하지만 prefab은 장면의 오브젝트를 접근할수 없다
    // Enemy가 생성될 때 target을 초기화 해줄 필요가 있다
    void OnEnable() // OnEnable = 스크립트가 활성화 될때 호출되는 이벤트 함수, 
    {
        target = GameManager.instance.player.GetComponent<Rigidbody2D>(); // OnEnable에서 타겟변수에 게임매니저를 활용하여 플레이어 할당, target은 RigidBody2D이므로 GetComponent가 필요하다
        isLive = true; // 처음 생성될때는 살아있도록 설정
        coll.enabled = true; // 컴포넌트의 비활성화는 .enabled = false
        rigid.simulated = true; // 리지드바디의 물리적 비활성화는 .simulated = false
        spriter.sortingOrder = 3; // 스프라이트 렌더러의 Sorting Order 감소
        anim.SetBool("Dead", false);
        health = maxHealth; // 처음 생성될때는 최대체력으로 생성되도록 해야한다   
    }
    
    public void Init(SpawnData data) // 레벨링에 따른 몬스터 상태 변경
    {
        speed = Random.Range(data.speed, data.speed * 2f);
        maxHealth = data.health;
        health = data.health;
    }
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Bullet") || !isLive) // 사망로직이 연달아 실행되는 것을 방지하기 위해 조건 추가
            return;
        
        health -= collision.GetComponent<Bullet>().damage;
        
        if (health > 0) {
            // .. Live, Hit Action
            StartCoroutine(KnockBack()); // StartCoroutine == 코루틴을 실행하는 키워드, StartCoroutine("KnockBack") 도 가능           
        }
        else {
            // .. Die, Die이후 OnEnable 함수에서 되돌려줘야한다(재활용사용)
            isLive = false; // 여러로직을 제어하는 isLive변수를 false로 변경
            coll.enabled = false; // 컴포넌트의 비활성화는 .enabled = false
            rigid.simulated = false; // 리지드바디의 물리적 비활성화는 .simulated = false
            spriter.sortingOrder = 2; // 스프라이트 렌더러의 Sorting Order 감소
            anim.SetBool("Dead", true); // SetBool 함수를 통해 죽는 애니메이션 상태로 전환
            // Dead()를 여기서 실행시키지 않고 유니티내의 애니메이션에서 실행되도록 한다

            GameManager.instance.kill++; // 몬스터 사망시 킬수 증가와 함께 경험치 함수 호출
            GameManager.instance.GetExp();
        }
    }

    // 코루틴 Coroutine = 생명주기와 비동기처럼 실행되는 함수
    IEnumerator KnockBack() // IEnumerator = 코루틴만의 반환형 인터페이서
    {
        // yield = 코루틴의 반환 키워드
        // yield return null = 1프레임 쉬기
        // yield return을 통해 다양한 쉬는 시간 조정
        // yield return new WaitForSeconds(2f) = 2초 쉬기, new를 계속 사용할경우 최적화에 안좋은 영향을 끼치기에 변수사용
        yield return wait; // 댜음 하나의 물리 프레임 딜레이
        Vector3 playerPos = GameManager.instance.player.transform.position;
        Vector3 dirVec = transform.position - playerPos; // 플레이어 기준의 반대 방향 = 현재 위치 - 플레이어 위치, 벡터의 뺄샘
        rigid.AddForce(dirVec.normalized * 3, ForceMode2D.Impulse); // RigidBody2D의 AddForce 함수로 힘 가하기, 
        // 현재 dirVec는 크기를 포함하고 있으므로 normalized필요
        // 순간적임 힘이므로 ForceMode2D.Impulse 속성 추가
    }

    void Dead()
    {
        gameObject.SetActive(false); // 사망할 땐 SetActive함수를 통한 오브젝트 비활성화, Destroy하면 안됨
    }

}
