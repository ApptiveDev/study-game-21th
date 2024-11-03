using UnityEngine;

public class ArrowSpawner : MonoBehaviour
{
    [SerializeField] private GameObject arrowPrefab;           // 화살 프리팹
    [SerializeField] private Transform playerTransform;         // 플레이어 위치 참조
    [SerializeField] private Transform enemyContainer;          // 적을 담는 부모 오브젝트

    public GameObject currentArrow = null;              // 현재 생성된 화살
    private bool isSpawningAllowed = true;              // 화살 생성 가능 여부

    private void Update()
    {
        // 적이 없으면 화살을 생성하지 않음
        if (!IsEnemyAlive())
        {
            return;
        }

        // 이전 화살이 소멸되었으면 새 화살 생성
        if (currentArrow == null && isSpawningAllowed)
        {
            SpawnArrow();
        }
    }

    // 화살 생성 함수
    void SpawnArrow()
    {
        Transform target = FindClosestEnemy(); // 가장 가까운 적 찾기

        if (target != null) // 적이 존재할 때만 화살 생성
        {
            // 화살 프리팹이 유효한지 확인
            if (arrowPrefab != null)
            {
                currentArrow = Instantiate(arrowPrefab, playerTransform.position, Quaternion.identity); // 화살 프리팹 생성
                Arrow arrowScript = currentArrow.GetComponent<Arrow>(); // 화살의 Arrow 스크립트 가져오기
                arrowScript.SetTarget(target); // 타겟 설정
            }
            else
            {
                Debug.LogError("Arrow Prefab is missing! Please assign it in the Inspector.");
            }
        }
    }

    // 가장 가까운 적을 찾아주는 함수
    Transform FindClosestEnemy()
    {
        Transform closestEnemy = null;
        float shortestDistance = Mathf.Infinity;

        foreach (Transform enemy in enemyContainer) // enemyContainer를 Transform으로 사용
        {
            if (enemy != null) // 적 오브젝트가 유효할 때만
            {
                float distanceToEnemy = Vector3.Distance(playerTransform.position, enemy.position);
                if (distanceToEnemy < shortestDistance)
                {
                    shortestDistance = distanceToEnemy;
                    closestEnemy = enemy;
                }
            }
        }

        return closestEnemy;
    }

    // 적이 살아 있는지 여부를 확인하는 함수
    bool IsEnemyAlive()
    {
        foreach (Transform enemy in enemyContainer) // enemyContainer를 Transform으로 사용
        {
            if (enemy != null) // 적 오브젝트가 유효하면 true 반환
            {
                return true;
            }
        }
        return false; // 모든 적이 null이면 false 반환
    }
}
