using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class RangerEnemy : MonoBehaviour
{
    public enum EnemyState { MOVE, ATTACK }
    public EnemyState state = EnemyState.MOVE; // 기본 상태를 MOVE로 설정
    
    public float speed = 1f;
    public float health;
    public float maxHealth;
    public int expValue = 1; // 적의 경험치 값
    public float attackRange = 5f;
    public float attackCooldown = 4f;
    public GameObject expPrefab;
    public GameObject projectilePrefab;
    public Transform firePoint;

    private bool isAttacking = false;


    bool isLive;

    Rigidbody2D target;
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

        target = GameManager.instance.player.GetComponent<Rigidbody2D>();
        StartCoroutine(CheckState());
        StartCoroutine(PerformAction());
    }

    public IEnumerator CheckState()
    {
        yield return new WaitForSeconds(1f); // 생성 후 최소 1초동안은 MOVE 유지 -> 멈춘 채로 생성 에러...
        while(isLive)
        {
            float distance = Vector3.Distance(target.position, transform.position);

            if (distance <= attackRange){
                state = EnemyState.ATTACK;
            }
            else{
                state = EnemyState.MOVE;
            }
            yield return new WaitForSeconds(0.1f);
        }
    }

    public IEnumerator PerformAction()
    {
        while (isLive)
        {
            if (state == EnemyState.MOVE){
                MoveTowardsPlayer();
            } else if (state == EnemyState.ATTACK && !isAttacking){
                StartCoroutine(AttackPlayer());
            }
            yield return null;
        }
    }

    void MoveTowardsPlayer()
    {
        Vector2 direction = (target.position - rigid.position).normalized;
        rigid.MovePosition(rigid.position + direction * speed * Time.fixedDeltaTime);
    }

    IEnumerator AttackPlayer()
    {
        isAttacking = true;

        rigid.velocity = Vector2.zero;

        // 발사체 생성 및 공격
        if (projectilePrefab != null && firePoint != null)
        {
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
            Projectile proj = projectile.GetComponent<Projectile>();
            if (proj != null)
            {
                proj.Init(target.position); // 플레이어 방향으로 발사
            }
        }

        yield return new WaitForSeconds(attackCooldown); // 공격 쿨다운
        isAttacking = false;
    }

    void LateUpdate()
    {
        if(!isLive)
            return;
        
        spriter.flipX = target.position.x < rigid.position.x;
    }

    void OnEnable() // 스크립트가 활성화될 때 호출되는 이벤트 함수 OnEnable / target 지정 위해
    {
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
            if (expPrefab != null){
                GameObject orb = Instantiate(expPrefab, transform.position, Quaternion.identity);
                ExpOrb expOrb = orb.GetComponent<ExpOrb>();
                
                if (expOrb != null){
                    expOrb.Init(expValue);
                }
            }

            gameObject.SetActive(false);
            GameManager.instance.kill++;
        }
    }
}
