using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int id; // 무기 id
    public int prefabId; // 프리펩 id
    public float damage;
    public int count;
    public float speed;

    float timer; // 총알 발사 간격
    Player player; // Scanner 스크립트에 접근하기위해 부모 오브젝트에 접근

    void Awake()
    {
        player = GetComponentInParent<Player>(); // GetCommponentInParent 함수로 부모의 컴포넌트 가져오기
    }

    void Start()
    {
        Init();
    }

    void Update()
    {
        // Update로직도 switch 문 활용하여 무기마다 로직 실행
        switch (id) {
            case 0:
                transform.Rotate(Vector3.back * speed * Time.deltaTime ); // Vector3.forward = (0,0,1), Vector3.back = (0,0,01)
                // Rotate를 하고 기준점을 Vector3.back으로 했으므로 위치를 (0,0,0)으로 두면 단지 제자리에서 돌 뿐이다, 그러므로 수정필요
                break;
            default:
                timer += Time.deltaTime; 

                if (timer > speed) { // speed보다 커지면 초기화하면서 발사로직 실행
                    timer = 0f;
                    Fire();
                }
                break;    
        }

        // .. Test Code .. 
        if (Input.GetButtonDown("Jump")) {
            LevelUp(10, 1);
        }
    }

    public void LevelUp(float damage, int count) // 레벨업 기능 함수
    {
        this.damage = damage;
        this.count += count;

        if (id == 0) { 
            Batch(); // 속성 변경과 동시에 근접무기의 경우 배치도 필요하니 함수 호출
        }
    }

    public void Init() // 변수들을 초기화
    {
        switch (id) { // 무기 ID에 따라 로직을 분리할 Switch문 작성
            case 0:
                speed = 150; // 시계방향의 속도 -150, speed에 양수를 사용하기위해 Update에서 Vector3.forward가 아닌 Vector3.back를 곱해줌
                Batch();
                break;
            default:
                speed = 0.3f; // speed값은 연사속도를 의미 = 적을 수록 많이 발사
                break;    
        }
    }

    // 생성된 무기를 배치하는 함수 생성, 호출 필요
    void Batch() // 자료를 모아두었다가 일관해서 처리하는 자료처리의 형태, 여기에서는 의미가 비슷하기에 사용
    {
        for (int index=0; index < count; index++) {
            Transform bullet;

            // 기존 오브젝트를 먼저 활용하고 모자란 것은 풀링에서 가져오기
            if (index < transform.childCount) { // 자신의 자식 오브젝트 개수 확인은 childCount속성
                bullet = transform.GetChild(index); // index가 아직 childCount 범위 내라면 GetChild 함수로 가져오기
            }
            else {
                bullet = GameManager.instance.pool.Get(prefabId).transform; // 의미상으로는 Get(1)이지만 원활한 코딩을 위해 prefabId를 사용한다
                bullet.parent = transform; // parent속성을 통해 부모 변경
                // 현재 bullet의 parent는 poolManager이지만 Player를 따라가야하므로 Weapon 0으로 parent를 바꿔줄 필요가 있다
            } 

            // 배치하면서 먼저 위치, 회전 초기화 하기
            bullet.localPosition = Vector3.zero; // 초기화하지않으면 위치가 월드기준으로 생성되기 때문에 문제 발생 
            bullet.localRotation = Quaternion.identity; // Quaternion은 3D 공간에서 회전을 표현하는 수학적 도구, Quaternion.identity는 회전이 적용되지 않은 기본 상태로 값은 (1, 0, 0, 0)

            Vector3 rotVec = Vector3.forward * 360 * index / count; // 회전 = 순서대로 360을 나눈 값을 z축에 적용
            bullet.Rotate(rotVec);
            bullet.Translate(bullet.up * 3f, Space.World); // 이동 = 회전한 상태에서 자신의 위쪽으로 이동
            // 회전 이휴의 자신의 위쪽방향으로 이동하는 것이 포인트
            // Space.World대신 Space.Self를 쓰게되면 회전방향에 따라 이동방향이 바뀌게 된다
            
            bullet.GetComponent<Bullet>().Init(damage, -1, Vector3.zero); // -1 is Infinity Per, per은 관통 계수이지만 근접무기의 경우 의미가 없으므로 -1로 지정, 속도도 0으로 지정
            // Bullet 컴포넌트 접근하여 미리 만들어놓은 Init 속성 초기화 함수 호출
        }
    }

    void Fire() 
    {
        if (!player.scanner.nearestTarget)  // Scannr안의 nearestTarget변수에 접근하기 위해 Player스크립트에 Scanner추가
            return;
        
        Vector3 targetPos = player.scanner.nearestTarget.position;
        Vector3 dir = targetPos - transform.position; // 크기가 포함된 방향 = 목표 위치 - 나의 위치 
        dir = dir.normalized; // normalized = 현재 벡터의 방향은 우지하고 크기를 1로 변환한 속성, dir은 크기를 가지고 있는 방향이기에 normalized필요

        Transform bullet = GameManager.instance.pool.Get(prefabId).transform; // 오브젝트 풀링에서 총알 생성
        bullet.position = transform.position; // 기존 생성로직을 그대로 활용하면서 위치는 플레이어 위치로 지정
        bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir); // FromToRatation = 지정된 축을 중심으로 목표를 향해 회전하는 함수
        bullet.GetComponent<Bullet>().Init(damage, count, dir); // 원거리 공격에 맞게 초기화 함수 호출하기, 원거리 공격에서는 Count가 관통변수이고 dir이 속도이다

    }
}