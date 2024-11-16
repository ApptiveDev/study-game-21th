using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyBossSystem : MonoBehaviour
{
    [SerializeField] GameObject Exp;
    [SerializeField] GameObject Bullet;
    GameObject player;
    int HP = 5;
    float speed = 1.5f;
    float curTime;
    const float checkDelay = 1f;
    const float attackRange = 10f;
    const float BulletDelay = 0.5f;
    bool isAttacking = false;
    enum enemyState
    {
        MOVE,
        ATTACK,
    }
    enemyState state;
    private void Awake()
    {
        player = GameObject.Find("Player");
    }

    void Start()
    {
        StartCoroutine(CheckState());
    }


    void Update()
    {
        if (state == enemyState.MOVE) EnemyMove();
        else if (state == enemyState.ATTACK)
        {
            EnemyAttack();
        }
    }

    public IEnumerator CheckState()
    {
        while (true)
        {
            if (Vector3.Distance(player.transform.position, transform.position) >= attackRange)
            {
                state = enemyState.MOVE;
            }
            else
            {
                state = enemyState.ATTACK;
            }
            yield return new WaitForSeconds(checkDelay);
        }
    }

    void EnemyMove()
    {
        isAttacking = false;
        transform.position += Vector3.right * (player.transform.position.x - transform.position.x) * Mathf.Abs((1 / (player.transform.position.x - transform.position.x))) * speed * Time.deltaTime;
        transform.position += Vector3.up * (player.transform.position.y - transform.position.y) * Mathf.Abs((1 / (player.transform.position.y - transform.position.y))) * speed * Time.deltaTime;

        Vector2 direction = player.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
    }

    void EnemyAttack()
    {
        if (!isAttacking)
        {
            StartCoroutine(SpawnBullet());
            isAttacking = true;
        }
        Vector2 direction = player.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Weapon")) TakeDamage(1);
        if (collision.gameObject.CompareTag("LastingWeapon")) TakeDamage(1);
        if (collision.gameObject.CompareTag("Stop")) speed = 0;
    }

    void OnTriggerStay2D(Collider2D LastingWeapon)
    {
        if (LastingWeapon.CompareTag("LastingWeapon"))
        {
            curTime += Time.deltaTime;
            if (curTime >= 1)
            {
                TakeDamage(1);
            }
        }
    }

    void TakeDamage(int damage)
    {
        HP -= damage;
        if (HP == 0)
        {
            Destroy(gameObject);
            GameObject ExpInstance = Instantiate(Exp);
            ExpInstance.transform.position = transform.position;
        }   
    }

    public IEnumerator SpawnBullet()
    {
        while (state == enemyState.ATTACK)
        {
            yield return new WaitForSeconds(BulletDelay);
            GameObject bulletInstance = Instantiate(Bullet);
            bulletInstance.transform.position = transform.position;

            EnemyBulletSystem bulletSystem = bulletInstance.GetComponent<EnemyBulletSystem>();
            bulletSystem.BulletDirection(transform.rotation);
        }
    }
}
