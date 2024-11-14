using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : MonoBehaviour
{
    private BossPattern nextPattern = BossPattern.None;
    private Transform player;
    public float rushSpeed = 5f;
    public float idleTime = 2f; // 대기 시간
    private float idleTimer; // 대기 시간 타이머 
    
    public enum BossPattern {
        None,
        Rush
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;  // 플레이어 찾기
        nextPattern = BossPattern.Rush; 
        idleTimer = idleTime;
    }

    void Update()
    {
        switch (nextPattern)
        {
            case BossPattern.None:
                // 대기 상태에서 대기 시간만큼 기다림
                idleTimer -= Time.deltaTime;
                if (idleTimer <= 0)
                {
                    nextPattern = BossPattern.Rush; // 대기 후 Rush 패턴으로 전환
                    idleTimer = idleTime;  // 타이머 리셋
                }
                break;

            case BossPattern.Rush:
                Rush();
                break;
        }
    }

    void Rush() 
    {
        if (player == null) return;

        Vector3 direction = (player.position - transform.position).normalized; // 플레이어 방향으로 벡터 계산
        transform.position += direction * rushSpeed * Time.deltaTime;  // 돌진 이동

        if (Vector3.Distance(transform.position, player.position) < 2f)
        {
            nextPattern = BossPattern.None;  // 플레이어와 가까워지면 대기 상태로 변경
            idleTimer = idleTime;  // 대기 시간 리셋
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("RangeEnemy"))
        {
            return;
        }
    }
}
