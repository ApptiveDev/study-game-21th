using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor;
using UnityEngine;

public class BossScript : EnemyMovement
{
    // Start is called before the first frame update
    private GameObject player;

    private float CHECK_DELAY = 1.0f;
    private float ATTACK_RANGE = 2.0f;
    private float ATTACK_DELAY = 1.0f;
    private float DASH_RANGE = 10.0f;
    private float DASH_TIME = 2.0f;
    private float DASH_DELAY = 1.0f;

    [SerializeField]
    private float knockbackSpeed = 10.0f;

    private bool isDash = false;
    private bool isAttack = false;
    public enum BossEnemyState
    {
        MOVE,
        ATTACK,
        DASH
    }

    public BossEnemyState state;

    public IEnumerator CheckState()
    {
        while (true)
        {
            // 공격범위 오면 공격
            if (Vector3.Distance(player.transform.position, transform.position) <= ATTACK_RANGE)
            {
                state = BossEnemyState.ATTACK;
            }
            else if (Vector3.Distance(player.transform.position, transform.position) >= DASH_RANGE)
            {
                state = BossEnemyState.DASH;
            }
            else
            {
                state = BossEnemyState.MOVE;
            }
            yield return new WaitForSeconds(CHECK_DELAY);
        }
    }
    void Start()
    {
        hp = 5000;
        speed = 1.0f;
        TackleDAMAGE = 3.0f;

        player = GameMgr.Instance.GetPlayer();

        StartCoroutine(CheckState());

    }

    public IEnumerator DashAttack()
    {
        // 1초 정지
        isDash = true;
        speed = 0.0f; // 정지 
        yield return new WaitForSeconds(DASH_DELAY);

        speed = 7.0f;
        yield return new WaitForSeconds(DASH_TIME);

        // 끝나고 나서 정지
        speed = 0.0f;
        yield return new WaitForSeconds(DASH_DELAY / 2);

        speed = 1.0f;
        isDash = false;
    }


    public IEnumerator KnockBackAttack()
    {
        isAttack = true;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(this.transform.position, ATTACK_RANGE);
        speed = 0.0f;
        foreach (Collider2D collider in colliders)
        {

            if (collider != null && collider.CompareTag("Player"))

            {
                yield return new WaitForSeconds(ATTACK_DELAY);
                Vector2 dir = collider.transform.position - this.transform.position;


                Rigidbody2D playerrb = player.GetComponent<Rigidbody2D>();
                playerrb.AddForce(dir.normalized * knockbackSpeed, ForceMode2D.Impulse);
                player.GetComponent<PlayerHp>().SubtractHp(TackleDAMAGE);

                yield return new WaitForSeconds(ATTACK_DELAY / 2);
                playerrb.velocity = Vector2.zero;
            }
        }

        speed = 1.0f;
        isAttack = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerHp>().SubtractHp(TackleDAMAGE);
        }
    }
    private void OnDrawGizmos()
    {
        // 디버깅을 위한 원형 범위 표시
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, ATTACK_RANGE);
    }
    // Update is called once per frame
    void Update()
    {

        DeleteEnemy();

        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);

        switch (state)
        {
            case BossEnemyState.DASH:
                if (!isDash && !isAttack)
                {
                    StartCoroutine(DashAttack());
                }
                break;

            case BossEnemyState.ATTACK:
                if (!isAttack && !isDash)
                {
                    StartCoroutine(KnockBackAttack());
                }
                break;
            case BossEnemyState.MOVE:
                break;
        }
    }
}
