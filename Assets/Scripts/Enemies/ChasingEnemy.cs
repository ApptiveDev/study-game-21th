using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasingEnemy : Enemy
{
    private Vector3 playerPosition;
    private Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
        Hp = 10f;
        MoveSpeed = 1f;
        Damage = 2f;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        ChasePlayer();
    }
    protected void ChasePlayer()
    {
        // Player 위치 가져오기
        playerPosition = GameManager.Instance.GetPlayer().transform.position;
        // Player 쪽으로 다가가기 위한 방향벡터 구하기
        direction = (playerPosition - transform.position).normalized;
        // 구한 방향벡터를 통해 플레이어 추격하기
        transform.position += direction * MoveSpeed * Time.deltaTime;
    }
}
