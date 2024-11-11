using UnityEngine;

public class Guard : MonoBehaviour
{
    public Transform player; // 캐릭터의 Transform
    public float distance = 2.0f; // 캐릭터와의 거리
    public float rotateSpeed = 50.0f; // 회전 속도
    public float guardDamage = 3f;
    public float pushForce = 5f;

    private float angle; // 현재 각도
    public SoundManager.WeaponType weaponType = SoundManager.WeaponType.Guard; 
    private SoundManager soundManager;
    private bool soundPlayed = false; // 소리 재생 여부를 추적하는 변수

    private void Start() {
        soundManager = FindObjectOfType<SoundManager>();
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

    private void OnTriggerStay2D(Collider2D collision) { // OnTriggerStay2D는 트리거 내부에 오브젝트가 계속 머무르고 있는 동안 로직 반복
        Enemy enemy = collision.GetComponent<Enemy>();
        if (enemy != null) {
            enemy.enemyHealth -= guardDamage * Time.deltaTime; // 적에게 데미지 주기
        
            Vector3 pushDirection = collision.transform.position - transform.position; // 적을 밀어내기
            pushDirection.Normalize(); //pushDirection은 두 오브젝트 간의 방향을 나타내는 벡터, 충돌 시 밀어내는 방향 정의
            collision.transform.position += pushDirection * pushForce * Time.deltaTime;

        if (!soundPlayed && soundManager != null) { // 소리가 아직 재생되지 않았다면
                soundManager.PlayWeaponSound(weaponType); // 소리 재생
                soundPlayed = true; // 소리가 재생되었음을 추적
        }

            if (enemy.enemyHealth <= 0) {
                collision.gameObject.SetActive(false);
            }
        }
    }

    public void IncreaseSpeed(float amount) // 회전속도 증가시키는 함수
    {
        rotateSpeed += amount;
        Debug.Log("Guard Speed increased to: " + rotateSpeed);
    }

    // 적이 트리거를 벗어나면 소리 재생 상태 리셋
    private void OnTriggerExit2D(Collider2D collision) {
        soundPlayed = false; // 적이 벗어나면 소리 재생 상태 리셋
    }

    
    
}
    

