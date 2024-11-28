using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEditorInternal;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    // 풀매니저로 무기도 관리하도록
    // 유니티 상에 생성되는 변수 관리할 부모 오브젝트 Weapon 추가

    public int id;
    public int prefabId;
    public float damage;
    public int count;
    public float speed;

    float timer; // 원거리 공격 위해 선언하는 변수 -> 몇초마다 생성
    Player player;

    void Awake()
    {
        player = GameManager.instance.player;
    }

    void Update()
    {
        if(!GameManager.instance.isLive)
            return;

        switch (id) {
            case 0:
                transform.Rotate(Vector3.back * speed * Time.deltaTime);
                break;
            default: // 원거리 공격 프리팹일 때
                timer += Time.deltaTime;

                if (timer > speed){ // speed보다 커지면 초기화하면서 발사 로직 실행
                    timer = 0f;
                    Fire();
                }
                break;
        }

        if(Input.GetButtonDown("Jump"))
        {
            LevelUp(20, 5);
        }
    }

    //레벨업 하면 무기의 damage랑 count(개수) 오르도록
    public void LevelUp(float damage, int count)
    {
        this.damage = damage;
        this.count += count; // 카운트는 하나씩 추가되자나
        
        if(id == 0)
            Batch();


        // 에러 해결 -> 옵션 추가 -> 자식옵젝 없을때.. 꼭 메세지 필요하진 앟ㄴ다
        player.BroadcastMessage("ApplyGear", SendMessageOptions.DontRequireReceiver); // ApplyGear가 실행되는 곳 -> 1. Weapon Init , 2. Weapon LevelUp 3. Gear LevelUP(기어레벨업) 4. Gear Init (기어자체 새로 생겼을 때)
    }


    // 프리팹 id에 따라 필요 초기 데이터 다르게 -> Init
    public void Init(ItemData data)// 스크립트블 오브젝트를 매개변수로 받을거야 
    {
        // Basic Set -> 기본적인 정보 : 이름, 생성 위치
        name = "Weapon" + data.itemId;
        transform.parent = player.transform; // 부모 위치를 플레이어쪽으로 !
        transform.localPosition = Vector3.zero; // 로컬 위치가 플레이어 위치로 설정되도록.. 머?
        
        // Property Set -> 레벨업 되면서 바뀌는 정보들
        id = data.itemId;
        damage = data.baseDamage;
        count = data.baseCount;

        // 뭔소리지  14강 38m 스크립터블 옵젝의 독립성을 위해 작성햇다는데 프리팹이랑 머가다른지원
        // 데이터 넣을 때 프리팹으로 맞춘걸.. 수정한듯
        for(int index=0; index < GameManager.instance.pool.prefabs.Length; index++){
            if(data.projectile == GameManager.instance.pool.prefabs[index]){
                prefabId = index;
                break;
            }
        }


        switch (id) {
            case 0:
                speed = 150;  
                Batch();
                break;
            default:
                speed = 0.3f; // 연사 속도 의미. 적을 수록 많이 발사 (= spawnTime 개념)
                break;
        }
        player.BroadcastMessage("ApplyGear", SendMessageOptions.DontRequireReceiver); // ApplyGear를 가진 애들 모두에게 적용해줘 -> 안해도 적용되는거 같은데 왜하는지 . .  머가문제임 ㅠ ㅠ 14강 57m
    }

    void Batch() // 근접 무기 공격 구현
    {
        for (int index=0; index < count; index++){
            Transform bullet;
            
            // 개수 적용 에러 해결
            if(index < transform.childCount){ // 자식 오브젝트가 있으면 먼저 쓴다
                bullet = transform.GetChild(index);
            }
            else{ // 아니면 오브젝트 풀링에서 가져온다
                bullet = GameManager.instance.pool.Get(prefabId).transform;
                bullet.parent = transform; // .parent 로 부모 속성 지정
            }

            
            
            // World 기준이라 위치 안잡히는 발생 에러 해결 -> 일단 로컬 정보 초기화
            bullet.localPosition = Vector3.zero;
            bullet.localRotation = Quaternion.identity;


            // 레벨에 따라 무기 갯수 증가
            // 증가한 무기 갯수 적용 후 회전 기능
            Vector3 rotVec = Vector3.forward * 360 * index / count;
            bullet.Rotate(rotVec);
            bullet.Translate(bullet.up * 1.5f, Space.World); // 자기자신 위로 거리 1.5만큼 / World 좌표 기준 이동 -> 왜..? 로컬로 해서
            
            bullet.GetComponent<Bullet>().Init(damage, -1, Vector3.zero); // 관통 무한 의미 : -1
        }
    }

    void Fire() // 원거리 공격 구현
    {   
        // 스캐너에서 찾은 대상이 없으면 리턴
        if (!player.scanner.nearestTarget)
            return;

        Vector3 targetPos = player.scanner.nearestTarget.position;
        Vector3 dir = targetPos - transform.position;
        dir = dir.normalized;

        Transform bullet = GameManager.instance.pool.Get(prefabId).transform;
        bullet.position = transform.position;
        bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir); // 축을 기준으로 회전하는 함수 FromToRotation
        bullet.GetComponent<Bullet>().Init(damage, count, dir);
    }

    /*
    //public int id;
    public GameObject bulletPrefab;
    public GameObject stonePrefab;
    public GameObject player;
    public float damage;
    // public int count;

    // private Scanner scanner;
    private float timer;
    // private Player player;

    public float bulletInterval = 2f;
    public float stoneInterval = 4f;

    void Awake()
    {
        // player = GetComponentInParent<Player>();
        // scanner = GetComponent<Scanner>();

        InvokeRepeating("SpawnBullet", 3f, bulletInterval);
        InvokeRepeating("SpawnStone", 5f, stoneInterval);
    }

    void SpawnBullet()
    {
        GameObject targetEnemy = GameObject.FindWithTag("Enemy");
        if(targetEnemy != null)
        {
            GameObject bullet = Instantiate(bulletPrefab, player.transform.position, Quaternion.identity);
            Vector3 direction = (targetEnemy.transform.position - transform.position).normalized;
        bullet.GetComponent<Bullet>().SetDirection(direction);

        Debug.Log($"총알 생성: 방향 = {direction}");
        }
        else
        {
            Debug.Log("적이 없습니다. 총알을 생성하지 않습니다.");
        }
        // Transform nearestTarget = scanner.GetNearest();
        // if(nearestTarget != null)
        // Vector3 direction = (bullet.transform.position - transform.position).normalized;
        // bullet.GetComponent<Bullet>().SetDirection(direction);
    
    }

    void SpawnStone()
    {
        GameObject stone = Instantiate(stonePrefab, player.transform.position, Quaternion.identity);
    }

    // public void LevelUp(float newDamage, int newCount)
    // {
    //     damage = newDamage;
    //     count += newCount;

    //     if (id == 0)
    //         Batch();
    // }

    // void Batch()
    // {
    //     for(int index = 0; index < count; index++)
    //     {
    //         GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity, transform); // 총알 생성
    //         bullet.transform.localPosition = Vector3.zero;
    //         bullet.transform.localRotation = Quaternion.identity;

    //         Vector3 rotVec = Vector3.forward * 360 * index / count;
    //         bullet.transform.Rotate(rotVec);
    //         bullet.transform.Translate(bullet.transform.up * 1.5f, Space.World);
    //         bullet.GetComponent<Bullet>().Init(damage, -1, Vector3.zero);
    //     }
    // }

    // void Fire()
    // {
    //     if(!player.scanner.nearestTarget)
    //         return;

    //     Vector3 targetPos = player.scanner.nearestTarget.position;
    //     Vector3 dir = (targetPos - transform.position).normalized;

    //     GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity); // 총알 생성
    //     bullet.transform.rotation = Quaternion.FromToRotation(Vector3.up, dir);
    //     bullet.GetComponent<Bullet>().Init(damage, count, dir);
    // }
    */
}
