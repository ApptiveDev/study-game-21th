using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeEnemy : ChasingEnemy
{
    private GameObject player;
    const float ATTACK_RANGE = 4f;
    const float CHECK_DELAY = 0.5f;
    public enum EnemyState
    {
        MOVE,
        ATTACK,
    }

    private EnemyState state = EnemyState.MOVE;
    private GameObject bullet;
    private bool isShooting = false; // 공격 중인지 체크하는 플래그

    // Start is called before the first frame update
    void Start()
    {
        Hp = 10f;
        MoveSpeed = 1f;
        Damage = 2f;
        bullet = GameManager.Instance.GetRangeEnemyBulletPrefab();
        player = GameManager.Instance.GetPlayer();

        StartCoroutine(CheckState());
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (Hp <= 0)
        {
            Die();
        }
        ActAccordingToState();
    }

    public IEnumerator CheckState()
    {
        while (true)
        {
            if (Vector3.Distance(player.transform.position, transform.position) <= ATTACK_RANGE)
            {
                state = EnemyState.ATTACK;
            }
            else
            {
                state = EnemyState.MOVE;
            }
            yield return new WaitForSeconds(CHECK_DELAY);
        }
    }

    public IEnumerator Shot()
    {
        isShooting = true; // 공격 중으로 설정
        Instantiate(bullet, this.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(1f); // 1초 대기
        isShooting = false; // 공격 완료 후 다시 공격 가능하게 설정
    }

    void ActAccordingToState()
    {
        switch (state)
        {
            case EnemyState.MOVE:
                Chase();
                break;
            case EnemyState.ATTACK:
                if (!isShooting) // 공격 중이 아닐 때만 Shot 호출
                {
                    StartCoroutine(Shot());
                }
                break;
        }
    }
}
