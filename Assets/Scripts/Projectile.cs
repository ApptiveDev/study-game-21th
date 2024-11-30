using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed; // 유니티에서만 지정가능하게 수정
    public float damage;

    private Vector2 moveDirection;

    public void Init(Vector2 direction)
    {
        // 이동 방향 계산 (목표 위치 - 발사 위치)
        moveDirection = direction.normalized;

        // 발사체 회전 (화살 촉 방향을 목표로 맞춤) -> 화살이 흐물거려서 발사체 스프라이트 돌로 수정 . . . .왜지?
        transform.rotation = Quaternion.FromToRotation(Vector3.up, moveDirection);
    }

    void Update()
    {
        transform.position += (Vector3)moveDirection * speed * Time.deltaTime;
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
