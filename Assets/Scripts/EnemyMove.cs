using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    private GameObject player;
    private Vector3 playerPosition;
    private Vector3 direction;
    private float moveSpeed = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        // FindWithTag 플레이어 오브젝트 가져오기
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        // Player 위치 가져오기
        playerPosition = player.transform.position;
        // Player 쪽으로 다가가기 위한 방향벡터 구하기
        direction = (playerPosition - transform.position).normalized;
        // 구한 방향벡터를 통해 플레이어 추격하기
        transform.position += direction * moveSpeed * Time.deltaTime;
    }
}
