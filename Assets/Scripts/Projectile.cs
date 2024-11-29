using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 0.5f;

    private Vector3 moveDirection;

    public void Init(Vector3 targetPosition)
    {
        // 이동 방향 계산 (목표 위치 - 발사 위치)
        moveDirection = (targetPosition - transform.position).normalized;

        // 발사체 회전 (화살 촉 방향을 목표로 맞춤)
        transform.rotation = Quaternion.FromToRotation(Vector3.up, moveDirection);
    }

    void Update()
    {
        transform.position += moveDirection * speed * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(!collision.CompareTag("Player"))
            return;
        
        if (collision.CompareTag("Player")){
            Destroy(gameObject); // 충돌 시 발사체 삭제
        }
    }
}
