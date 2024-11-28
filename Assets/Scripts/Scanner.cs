using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    public float scanRange;
    public LayerMask targetLayer; // 스캔할 레이어 선언
    public RaycastHit2D[] targets; // 스캔 결과 담을 배열
    public Transform nearestTarget; // 플레이어랑 가장 가까운 타겟

    void FixedUpdate()
    {   
        // CircleCastAll() : 원형의 캐스트 쏘고 모든 결과 반환 함수
        // 1. 캐스팅 시작 위치 2. 원반지름 3. 캐스팅 방향 4. 캐스팅 길이 5. 대상 레이어
        // 캐스팅 쏘기만 하고 움직임 X -> 길이, 방향 모두 필요 X
        targets = Physics2D.CircleCastAll(transform.position, scanRange, Vector2.zero, 0, targetLayer); 
        nearestTarget = GetNearest();    
    }

    Transform GetNearest()
    {
        Transform result = null;
        
        float diff = 100; // 거리 100

        // foreach 로 targets 배열에 담긴 캐스팅 결과 오브젝트 하나씩 접근
        foreach(RaycastHit2D target in targets)
        {

            // 거리가 가장 가까운 곳에 있는 타겟 찾기 구현
            Vector3 myPos = transform.position;
            Vector3 targetPos = target.transform.position;
            float curDiff = Vector3.Distance(myPos, targetPos);

            if (curDiff < diff){
                diff = curDiff; // 거리가 저장된 거리보다 작으면 교체
                result = target.transform;
            }
        }

        return result;
    }
}
