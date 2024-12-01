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
        // player = GetComponentInParent<Player>(); // GetCommponentInParent 함수로 부모의 컴포넌트 가져오기
        player = GameManager.instance.player; // 처음부터 플레이어의 자식으로 있는것이 아니라 Init함수에 의해 플레이어의 자식으로 생성되므로 플레이어의 초기화는 게임매니저 활용으로 변경
    }

    // void Start() // Start로직에서의 Init은 이제 Item.Onclick에서 실행될것이므로 삭제
    // {
    //     Init();
    // }

    void Update()
    {
        if (!GameManager.instance.isLive) {
            return; // isLive가 false이면(시간이 멈추면) 동작하지 못하도록 조건 추가
        }

        // Update로직도 switch 문 활용하여 무기마다 로직 실행
        switch (id) {
            case 0:
                transform.Rotate(Vector3.back * speed * Time.deltaTime ); // Vector3.forward = (0,0,1), Vector3.back = (0,0,-1)
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
    }

    public void LevelUp(float damage, int count) // 레벨업 기능 함수
    {
        this.damage += damage;
        this.count += count;

        if (id == 0) { 
            Batch(); // 속성 변경과 동시에 근접무기의 경우 배치도 필요하니 함수 호출
        }

        player.BroadcastMessage("ApplyGear", SendMessageOptions.DontRequireReceiver); 
        // 레벨업을 하고 Gear를 호출하는 경우에도 달라진 값이 적용될수있도록 마지막 부분에서 호출
        // Gear.cs에서 Weapon의 damage를 받아오는 것이 아니라 직접 입력한 값으로 작용하기 떄문에 생기는 문제
        // BroadcastMessage의 두번째 인자값으로 DontrequireReceiver 추가
    }

    public void Init(ItemData data) // 변수들을 초기화, 스크립트블 오브젝트를 매개변수로 받아 활용
    {
        // Basic Set
        name = "Weapon" + data.itemId; // 새로 생성될 파일의 이름 설정
        transform.parent = player.transform; // 부모오브젝트를 player로 고정
        // 처음 생성할때 플레이어의 자식오브젝트로 생성되야한다
        transform.localPosition = Vector3.zero; // 지역 위치인 localPosition을 원점으로 변경

        // Property Set
        id = data.itemId; // 각종 무기 속성변수들을 스크립트블 오브젝트 데이터로 초기화 
        damage = data.baseDamage;
        count = data.baseCount; 

        // ItemData 에서 prefabId를 숫자로 입력받아도 되지만 그렇게하면 PoolManager에서 prefab의 순서와 prefabId를 일치하여 입력해줘야한다는 번거러움이 있다
        // 그렇기에 스크립트블 오브젝트의 독립성을 위해서 인덱스가 아닌 프리펩으로 설정한다
        for (int index=0; index < GameManager.instance.pool.prefabs.Length; index++) {
            if (data.projectile == GameManager.instance.pool.prefabs[index]) { // 프리펩 아이디는 풀링 매니저의 변수에서 찾아서 초기화
                prefabId = index;
                break;
            }
        }

        switch (id) { // 무기 ID에 따라 로직을 분리할 Switch문 작성
            case 0:
                speed = 150; // 시계방향의 속도 -150, speed에 양수를 사용하기위해 Update에서 Vector3.forward가 아닌 Vector3.back를 곱해줌
                Batch();
                break;
            default:
                speed = 0.3f; // speed값은 연사속도를 의미 = 적을 수록 많이 발사
                break;    
        }

        player.BroadcastMessage("ApplyGear", SendMessageOptions.DontRequireReceiver); // BroadcastMessage = 특정함수 호출을 모든 자식에서 방송하는 함수
        // BroadcastMessage의 두번째 인자값으로 DontrequireReceiver 추가
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
            
            bullet.GetComponent<Bullet>().Init(damage, -1, id, Vector3.zero); // -1 is Infinity Per, per은 관통 계수이지만 근접무기의 경우 의미가 없으므로 -1로 지정, 속도도 0으로 지정
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
        bullet.position = transform.position - PickRandomPosition(); // 기존 생성로직을 그대로 활용하면서 위치 지정

        bullet.rotation = Quaternion.Euler(0, 0, 45);
        bullet.rotation *= Quaternion.FromToRotation(Vector3.up, dir); // FromToRatation = 지정된 축을 중심으로 목표를 향해 회전하는 함수


        bullet.GetComponent<Bullet>().Init(damage, count, id, dir); // 원거리 공격에 맞게 초기화 함수 호출하기, 원거리 공격에서는 Count가 관통변수이고 dir이 속도이다

    }

    Vector3 PickRandomPosition() // 랜덤한 위치(벡터3)을 반환한다.
    {
        float x = Random.Range(-1f, 1f);
        float y = Random.Range(-1f, 1f);

        return new Vector3(x, y, 0);
    }
}