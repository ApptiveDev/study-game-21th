using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RangerEnemy : Enemy // 적 하나에 적용, 애니메이터 추가 -> 움직이지 않는 에러 발생 / 일단 주석 처리
{
    public enum EnemyState { MOVE, ATTACK }
    public EnemyState state = EnemyState.MOVE;

    public GameObject projectilePrefab; // 발사체 프리팹
    public Transform firePoint; // 발사 위치
    public float attackRange;
    public float attackCooldown; // 모두 유니티 상에서만 조정가능하게 수정

    private float lastAttackTime;
    private float originSpeed;

    protected override void Start()
    {
        base.Start(); // Enemy 클래스의 Start 호출
        state = EnemyState.MOVE;
        originSpeed = speed;
    }

    void Update()
    {
        if(!isLive)
            return;

        float distance = Vector2.Distance(target.position, transform.position);

        // 상태 전환
        if (distance <= attackRange){
            state = EnemyState.ATTACK;
        } else{
            state = EnemyState.MOVE;
        }

        // 상태에 따른 행동
        switch (state)
        {
            case EnemyState.MOVE:
                speed = originSpeed;
                MoveTowardsPlayer();
                break;

            case EnemyState.ATTACK:
                speed = 0;
                if (Time.time >= lastAttackTime + attackCooldown){
                    AttackPlayer();
                    lastAttackTime = Time.time; // 공격 시간 갱신
                }
                break;
        }
    }

    private void MoveTowardsPlayer()
    {
        if (!isLive || state != EnemyState.MOVE)
            return;

        Vector2 direction = (target.position - rigid.position).normalized;
        rigid.MovePosition(rigid.position + direction * speed * Time.fixedDeltaTime);
    }

    private void AttackPlayer()
    {
        // 발사체 생성
        if (projectilePrefab != null && firePoint != null)
        {
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
            Projectile proj = projectile.GetComponent<Projectile>();
            if (proj != null)
            {
                proj.Init((target.position - (Vector2)firePoint.position).normalized); // 방향 설정
            }
        }
    }

    protected override void LateUpdate()
    {
        base.LateUpdate();
        spriter.flipX = target.position.x < rigid.position.x; // 이동 시 방향 조정

    }
}
