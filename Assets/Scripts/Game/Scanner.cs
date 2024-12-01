using UnityEngine;

public class Scanner : MonoBehaviour
{
    public float scanRange; // 감지 범위
    public LayerMask targetLayer; // 감지할 레이어
    public RaycastHit2D[] targets; // 감지 대상을 저장할 변수
    public Transform nearestTarget; // 가장가까운 대상의 Transform

    void FixedUpdate() 
    {
        targets = Physics2D.CircleCastAll(transform.position, scanRange, Vector2.zero, 0, targetLayer); // 원형의 케스트를 쏘고 모든 결과를 반환하는 함수(캐스팅 시작위치, 원의 반지름, 캐스팅 방향, 캐스팅 길이, 대상 레이어)
        nearestTarget = GetNearest(); // GetNearest 함수를 통해 반환된 result를 저장
    }

    Transform GetNearest() 
    {
        Transform result = null;
        float diff = 100; // 플레이어와의 최소거리

        foreach (RaycastHit2D target in targets)  { // foreach 문으로 캐스팅 결과 오브젝트를 하나씩 접근, RaycastHit2D 확인
            Vector3 myPos = transform.position; // 플레이어의 위치
            Vector3 targetPos = target.transform.position; // targets 배열에서 꺼낸 target의 위치
            float curDiff = Vector3.Distance(myPos, targetPos); // Distance(A, B) = 벡터 A와 B의 거리를 계산해주는 함수

            if (curDiff < diff) { // 반복문을 돌며 가져온 거리가 저장된 거리보다 작으면 교체
                diff = curDiff;
                result = target.transform;
            } // 가장 최소거리의 target.transform이 result에 저장되게 된다
        }

        return result;
    }

}