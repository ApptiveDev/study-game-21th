using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{   
    public float health = 10f; // 적의 체력

    float enemySpeed; // 적의 이동 속도
    private GameObject player;

    public void SetMoveSpeed()
    {
        enemySpeed = Random.Range(1f, 3f);
    }

    // Start is called before the first frame update
    void Start()
    {
        // 태그가 "Player"인 오브젝트를 찾는다.
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null) // 플레이어가 존재하는지 확인
        {
            Vector3 enemyVec = (player.transform.position - transform.position).normalized;
            transform.position += enemyVec * enemySpeed * Time.deltaTime;
        }
    }

}
