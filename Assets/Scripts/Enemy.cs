using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrefab;
    public float moveSpeed = 1f;
    private Transform player;
    public float enemyDamage = 10; // 적이 주는 데미지


    public float enemyHealth = 10f; // 적 체력


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (player != null)
        {
            Vector3 direction = player.position - transform.position;
            direction.Normalize(); // 방향 사용하려고 Normalize

            transform.position += direction * moveSpeed * Time.deltaTime;
        }
    }

     public void OnTriggerEnter2D(Collider2D collision) {
        Player player = collision.GetComponent<Player>(); // 충돌한 오브젝트에서 player 요소 가져옴
        if (player != null ) { // 충돌한 오브젝트가 플레이어인지 확인
            player.TakeDamage((int)enemyDamage);
            Debug.Log("남은 체력: "+player.playerHealth);
            gameObject.SetActive(false); // 적 오브젝트 끄기
        }

    }



}

