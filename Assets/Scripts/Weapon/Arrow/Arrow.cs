using UnityEngine;

public class Arrow : MonoBehaviour
{
    private Transform target; // 적 타겟
    private float speed; // 화살 속도
    public int damage = 10; // 데미지

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

    private void Update()
    {
        if (target == null)
        {
            Destroy(gameObject); // 타겟이 없으면 화살 삭제
            return;
        }

        // 타겟 방향으로 이동
        Vector3 direction = (target.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;

        // 적에게 매우 가까워지면 충돌 처리
        if (Vector3.Distance(transform.position, target.position) < 0.2f)
        {
            HitTarget();
        }
    }

    void HitTarget()
    {
        if (target != null) // 타겟이 null인지 확인
        {
            EnemyHealth enemyHealth = target.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage); // 적의 체력 감소
            }
        }

        Destroy(gameObject); // 화살 삭제
    }

    private void OnDestroy()
    {
        // 화살이 파괴될 때 Spawner에게 알림
        ArrowSpawner spawner = FindObjectOfType<ArrowSpawner>();
        if (spawner != null)
        {
            spawner.currentArrow = null; // 화살이 파괴되면 Spawner가 새로운 화살을 생성할 수 있도록 null로 설정
        }
    }
}
