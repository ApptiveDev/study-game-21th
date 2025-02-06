using System.Collections;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{ // 근접무기 프리펩의 Order in Layer를 몬스터보다 높이기

    public float health; 
    public float time;  

    Rigidbody2D rigid; // 총탄은 속도가 필요하므로 RigidBody2D 추가
    WaitForFixedUpdate wait;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        wait = new WaitForFixedUpdate();
    }

    void Enalble()
    {
        StartCoroutine(Dead());
    }

    public void Init(Vector3 dir, float health)
    {
        rigid.velocity = dir * 15f; // velocity = 속도, 속력을 곱해주어 총알이 날아가는 속도 증가시키기
        this.health = health;
    }

    void OnTriggerEnter2D(Collider2D collision) 
    {
        if (!collision.CompareTag("Bullet")) // 사망로직이 연달아 실행되는 것을 방지하기 위해 조건 추가
            return;
        
        gameObject.SetActive(false);
        // health -= collision.GetComponent<Bullet>().damage;

        // if (health > 0) {
        //     StartCoroutine(KnockBack()); // StartCoroutine == 코루틴을 실행하는 키워드, StartCoroutine("KnockBack") 도 가능     
        // }
        // else {
        //     gameObject.SetActive(false); // 사망할 땐 SetActive함수를 통한 오브젝트 비활성화, Destroy하면 안됨
        // }

    }

    IEnumerator Dead()
    {
        yield return wait;
        yield return wait;
        gameObject.SetActive(false);
    }

    // 코루틴 Coroutine = 생명주기와 비동기처럼 실행되는 함수
    // IEnumerator KnockBack() // IEnumerator = 코루틴만의 반환형 인터페이서
    // {
    //     // yield = 코루틴의 반환 키워드
    //     // yield return null = 1프레임 쉬기
    //     // yield return을 통해 다양한 쉬는 시간 조정
    //     // yield return new WaitForSeconds(2f) = 2초 쉬기, new를 계속 사용할경우 최적화에 안좋은 영향을 끼치기에 변수사용
    //     // 댜음 하나의 물리 프레임 딜레이
    //     Vector3 playerPos = GameManager.instance.player.transform.position;
    //     Vector3 dirVec = transform.position - playerPos; // 플레이어 기준의 반대 방향 = 현재 위치 - 플레이어 위치, 벡터의 뺄샘
    //     Vector3 temp = rigid.velocity; 
    //     rigid.AddForce(dirVec.normalized * 3, ForceMode2D.Impulse); // RigidBody2D의 AddForce 함수로 힘 가하기, 
    //     yield return wait;
    //     // 현재 dirVec는 크기를 포함하고 있으므로 normalized필요
    //     // 순간적임 힘이므로 ForceMode2D.Impulse 속성 추가
    // }
}