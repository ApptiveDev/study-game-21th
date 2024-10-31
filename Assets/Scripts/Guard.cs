using UnityEngine;

public class Guard : MonoBehaviour
{
    public Transform player; // 캐릭터의 Transform
    public float distance = 2.0f; // 캐릭터와의 거리
    public float speed = 50.0f; // 회전 속도

    private float angle; // 현재 각도

    private void Update()
    {
        // 플레이어 주위를 원운동
        if (player != null)
        {
            // 각도 증가
            angle += speed * Time.deltaTime;

            // 방패의 위치를 계산
            float x = Mathf.Cos(angle * Mathf.Deg2Rad) * distance;
            float y = Mathf.Sin(angle * Mathf.Deg2Rad) * distance;

            // 방패 위치 설정
            transform.position = new Vector3(player.position.x + x, player.position.y + y, transform.position.z);

            // 방패가 캐릭터를 바라보도록 설정
            Vector3 direction = player.position - transform.position;
            float angleToPlayer = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f; // 방패가 바라볼 방향 계산
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angleToPlayer));
        }
    }
    
}
