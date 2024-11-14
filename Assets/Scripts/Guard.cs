using System;
using UnityEngine;

public class Guard : MonoBehaviour
{
    public Transform player; // 캐릭터의 Transform
    public float distance = 2.0f; // 캐릭터와의 거리
    public float rotateSpeed = 50.0f; // 회전 속도
    public float guardDamage = 3f;
    public float pushForce = 5f;

    private float angle; // 현재 각도
    
    private AudioSource audioSource;

    private void Start() {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        // 플레이어 주위를 원운동
        if (player != null)
        {
            // 각도 증가
            angle += rotateSpeed * Time.deltaTime;

            // 방패의 위치를 계산
            float x = Mathf.Cos(angle * Mathf.Deg2Rad) * distance;
            float y = Mathf.Sin(angle * Mathf.Deg2Rad) * distance;

            // 방패 위치 설정
            transform.position = new Vector3(player.position.x + x, player.position.y + y, transform.position.z);

            // 방패가 캐릭터를 바라보도록 설정
            Vector3 direction = player.position - transform.position;
            float angleToPlayer = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f; // 방패가 바라볼 방향 계산
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angleToPlayer)); // Quaternion.Euler로 오브젝트의 회전을 특정한 각도로 지정
        }

        if (Player.Instance != null && Player.Instance.playerHealth <= 0)
        {
            gameObject.SetActive(false);
            return;
        }

    }

    private void OnTriggerStay2D(Collider2D collision) {
    Enemy enemy = collision.GetComponent<Enemy>();
    RangeEnemy rangeEnemy = collision.GetComponent<RangeEnemy>();
    BossEnemy bossEnemy = collision.GetComponent<BossEnemy>();
    ExperienceOrb experienceOrb = collision.GetComponent<ExperienceOrb>();
    if (audioSource != null) {
        audioSource.Play();
        }

    if (enemy != null) {
        enemy.enemyHealth -= guardDamage * Time.deltaTime; // 근접 적에게 데미지 주기
    }

    if (rangeEnemy != null) {
        rangeEnemy.rangeEnemyHealth -= guardDamage * Time.deltaTime; // 원거리 적에게 데미지 주기
    }

     if (bossEnemy != null) {
        bossEnemy.bossHealth -= guardDamage * Time.deltaTime; // 원거리 적에게 데미지 주기
    }

    Vector3 pushDirection = collision.transform.position - transform.position;
    pushDirection.Normalize();
    collision.transform.position += pushDirection * pushForce * Time.deltaTime;

    if (enemy != null && enemy.enemyHealth <= 0) {
        collision.gameObject.SetActive(false);
    }

    if (rangeEnemy != null && rangeEnemy.rangeEnemyHealth <= 0) {
        collision.gameObject.SetActive(false);
    }

    if (bossEnemy != null && bossEnemy.bossHealth <= 0) {
        collision.gameObject.SetActive(false);
    }

}

    public void IncreaseSpeed(float amount) // 회전속도 증가시키는 함수
    {
        rotateSpeed += amount;
        Debug.Log("Guard Speed increased to: " + rotateSpeed);
    }
    
}
    

