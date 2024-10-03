using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    GameObject target;

    Vector3 moveDir;
    float moveSpeed = 3f;
    float damage = 10f;

    private void Start() {
        target = FindObjectOfType<Enemy>().gameObject; // 타겟을 적의 오브젝트로 설정
        moveDir = target.transform.position - transform.position; // 이동 방향 벡터를 계산
        moveDir.Normalize(); // 이동 방향 벡터 정규화
    }

    private void Update() {
        if (target == null) { // 타겟이 없다면 화살을 지움
            //gameObject.SetActive(false);
            Destroy(gameObject);
        }
        transform.position += moveDir * moveSpeed * Time.deltaTime; // 화살을 진행방향 만큼 이동
    }

    private void OnTriggerEnter2D(Collider2D collision) { // 충돌했을 때
        Debug.Log("Hit!");
        Enemy enemy = collision.GetComponent<Enemy>(); // 적의 enemy 스크립트를 참조
        if (enemy.gameObject == target) { // 
            enemy.health -= damage; // 충돌한 적의 체력을 데미지만큼 감소시키는 코드
            if (enemy.health <= 0) { // 적 체력이 0 이하인 경우
                //collision.gameObject.SetActive(false); // 적 게임 오브젝트를 끔
                gameObject.SetActive(false); // 본인 오브젝트도 끔
                Destroy(collision.gameObject); // 적 게임 오브젝트를 지움
            }
        }
    }
}
