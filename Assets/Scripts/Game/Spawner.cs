using UnityEngine;

// 하나의 스크립트 내에 여러 클래스를 선언할 수 있다.
// 직렬화 (Serialization) = 개체를 저장 혹은 전송하기 위해 변환
[System.Serializable]
public class SpawnData
{
    public float spawnTime;
    public int spriteType;
    public int health;
    public float speed;
}

public class Spawner : MonoBehaviour
{
    public Transform spawnPoint; // 자식 오브젝트의 트랜스폼을 담을 배열 변수 선언 
    public SpawnData[] spawnData; 

    int level;
    float timer; // 소환 타이머를 위한 변수 선언

    void Awake()
    {
        spawnPoint = GetComponent<Transform>();
    }

    void Update()
    {
        if (!GameManager.instance.isLive) {
            return; // isLive가 false이면(시간이 멈추면) 동작하지 못하도록 조건 추가
        }

        timer += Time.deltaTime;
        level = Mathf.Min(Mathf.FloorToInt(GameManager.instance.gameTime / 30f), spawnData.Length - 1); // 적절한 숫자로 나누에 시간에 맞춰 레벨이 올라가도록 작성
        // FloorToInt = 소수점 아래는 버리고 int형으로 바꾸는 함수 
        // CeilToInt = 소수점 아래를 올리고 int형으로 바꾸는 함수
        // Mathf.Min = 둘중 최솟값을 반환, 시간이 지나면서 level이 배열보다 커지는 에러가 발생하는데 이를 막기 위해 사용

        if (timer > spawnData[level].spawnTime) { // 타이머가 일정 시간 값에 도달하면 소환하도록 작성, 레벨을 활용해 소환타이밍을 변경
            timer = 0;
            Spawn();
        }  
    }

    void Spawn()  
    {
        GameObject enemy = GameManager.instance.pool.Get(0); // 게임매니저의 인스턴스까지 접근하여 풀링의 함수 호출, Get(1) = 배열에서 1번쨰 요소에 해당(좀비 = 0, 스켈레톤 = 1) 
        enemy.transform.position = spawnPoint.position; // Instantiate 반환 값을 변수에 넣고 이를 만들어둔 소환 위치 중 하나로 배치되도록 작성
        enemy.GetComponent<Enemy>().Init(spawnData[level]); // 오브젝트 풀에서 가져온 오브젝트에서 Enemy 컴포넌트로 접근
    }
}

