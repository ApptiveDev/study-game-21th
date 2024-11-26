using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class RangerEnemy : EnemyBase
{
    public float speed;

    [SerializeField] Transform player;
    private Vector2 target;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        target = new Vector2(player.position.x, player.position.y);
    }


    // public GameObject attackPrefab;
    // public float attackRange = 10f;
    // public float attackDelay = 2f;
    // public float checkDelay = 0.5f;

    // public GameObject player;
    // private bool canAttack = true;

    // public enum EnemyState
    // {
    //     MOVE,
    //     ATTACK
    // }
    // private EnemyState state;

    // void Start()
    // {
    //     // player = GameManager.instance.player.transform;
        
    //     StartCoroutine(CheckState());
    // }

    // void Update()
    // {
    //     if (!GameManager.instance.isLive || player == null)
    //         return;

    //     if (state == EnemyState.MOVE)
    //     {
    //         MoveTowardsPlayer();
    //     }
    //     else if (state == EnemyState.ATTACK)
    //     {
    //         AttackPlayer();
    //     }
    // }

    // IEnumerator CheckState()
    // {
    //     while (GameManager.instance.isLive)
    //     {
    //         float distance = Vector3.Distance(player.transform.position, transform.position);
    //         if (distance <= attackRange)
    //         {
    //             state = EnemyState.ATTACK;
    //         }
    //         else
    //         {
    //             state = EnemyState.MOVE;
    //         }

    //         yield return new WaitForSeconds(checkDelay);
    //     }
    // }

    // void MoveTowardsPlayer()
    // {
    //     Vector3 direction = (player.transform.position - transform.position).normalized;        
    //     transform.position += direction * speed * Time.deltaTime;
    // }

    // void AttackPlayer()
    // {
    //     if (canAttack)
    //     {
    //         StartCoroutine(PerformAttack());
    //     }
    // }

    // IEnumerator PerformAttack()
    // {
    //     canAttack = false;

    //     Instantiate(attackPrefab, transform.position, Quaternion.identity);


    //     yield return new WaitForSeconds(attackDelay);

    //     canAttack = true;
    // }
}
