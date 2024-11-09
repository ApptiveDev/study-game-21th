using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject experienceOrbPrefab;
    public float moveSpeed = 1f;
    private Transform player;
    public float enemyDamage = 10; // 적이 주는 데미지


    public float enemyHealth = 10f; // 적 체력
    private float frozenTime = 0f;


    void Start()
    {
        //player = GameObject.FindGameObjectWithTag("Player").transform;
        player = Player.Instance.transform;
    }

    void Update()
    {
        // 얼려진 상태가 아닐 경우에만 이동
        if (frozenTime <= 0)
        {
            if (player != null)
            {
                Vector3 direction = player.position - transform.position;
                direction.Normalize(); // 방향 사용하려고 Normalize
                transform.position += direction * moveSpeed * Time.deltaTime;
            }
        }
        else
        {
            frozenTime -= Time.deltaTime; // 얼려진 시간 감소
        }

    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        // 플레이어와 충돌한 경우
        if (collision.CompareTag("Player"))
        {
            Player playerScript = collision.GetComponent<Player>();
            if (playerScript != null)
            {
                playerScript.TakeDamage((int)enemyDamage);
                Debug.Log("남은 체력: " + playerScript.playerHealth);
                Destroy(gameObject);
            }
            return; // 이후 코드를 실행하지 않음
        }

        // 무기와 충돌한 경우
        if (collision.CompareTag("Weapon"))
        {
            if (enemyHealth <= 0) {
                Die(); 
            }
        }
    }


        // 적이 얼어있을 때 호출되는 메서드
    public void FreezeForSeconds(float duration)
    {
        frozenTime = duration; // 얼려진 상태 시간 설정
    }

    public void Die()
    {
        SpawnExperienceOrb();
        Destroy(gameObject);
    }

    private void SpawnExperienceOrb()
    {
        if (experienceOrbPrefab != null)
        {
            Instantiate(experienceOrbPrefab, transform.position, Quaternion.identity);
        }
    }

    public void IncreaseFrozenTime(float amount) // 얼리는 시간 증가 함수
    {
        frozenTime += amount;
        Debug.Log("Frozen Time increased to: " + frozenTime);
    }


}

