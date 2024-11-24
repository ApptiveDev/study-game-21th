using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeEnemyBullet : MonoBehaviour
{
    private float shotSpeed = 3f;
    private Vector3 direction;
    private float damage = 2f;

    private void Start()
    {
        Destroy(this.gameObject, 3f);

        // 처음에 플레이어 방향으로 초기화
        UpdateDirection();
        // 방향 업데이트 코루틴 시작
        StartCoroutine(UpdateDirectionCoroutine());
    }

    private void Update()
    {
        // 방향에 따라 이동
        transform.position += direction * shotSpeed * Time.deltaTime;
    }

    // 플레이어 위치를 기반으로 방향 업데이트
    private void UpdateDirection()
    {
        Vector3 playerPosition = GameManager.Instance.GetPlayer().transform.position;
        Vector3 newDirection = (playerPosition - transform.position).normalized;

        // 정확도를 낮추기 위해 약간의 무작위성을 추가
        Vector3 offset = new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f), 0);
        newDirection += offset;
        newDirection.Normalize();

        // 현재 방향과 새로운 방향을 조금씩 섞어서 부드러운 회전 효과 추가
        direction = Vector3.Lerp(direction, newDirection, 0.1f);
    }

    // 일정 시간마다 방향을 업데이트하는 코루틴
    private IEnumerator UpdateDirectionCoroutine()
    {
        while (true)
        {
            UpdateDirection();
            yield return new WaitForSeconds(0.2f);  // 0.2초 간격으로 업데이트
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.GetPlayer().GetComponent<Player>().BeAttacked(damage);
            if (this.gameObject != null) Destroy(this.gameObject);
        }
    }
}
