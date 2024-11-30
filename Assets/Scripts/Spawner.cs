using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoint; // 스폰 위치 배열
    public SpawnData[] spawnData; // 데이터 여러개니 배열..
    
    
    int level; // 레벨에 따라 다르게 소환 구현 -> level 0일때: 추적형 ai만 생성, level 1: 원거리 적 생성
    float timer;
    bool bossSpawned = false;

    void Awake()
    {
        spawnPoint = GetComponentsInChildren<Transform>();

        // 부모 객체(Spawner) 포함 여부 확인
        Debug.Log($"Number of spawn points: {spawnPoint.Length}");
        for (int i = 0; i < spawnPoint.Length; i++)
        {
            Debug.Log($"Spawn point {i}: {spawnPoint[i].position}");
        }
    }

    void Update()
    {
        if(!GameManager.instance.isLive)
            return;

        timer += Time.deltaTime;
        //Mathf.FloorToInt : 버려서 Int형으로 변환
        level = Mathf.FloorToInt(GameManager.instance.gameTime / 10f); // 게임타임이 10초씩 늘어나면 난이도 증가
        if(level <= 1){ // 레벨 0, 1일 때 작은 enemy들 무한 생성
            if (timer > spawnData[level].spawnTime)
            {
                timer = 0;
                Spawn();
            }
        } else if (level >=2 && !bossSpawned){ // 30초 이상일 때 Boss 생성
                Debug.Log("Attempting to spawn Boss...");
                SpawnBoss();
                bossSpawned = true;
        }
    }

    void Spawn() 
    {
        if (spawnPoint.Length <= 1) 
        {
            Debug.LogError("No valid spawn points! Add child objects to Spawner.");
            return;
        }

        // 부모 객체(0번)를 제외한 랜덤 인덱스
        // 스폰 위치 랜덤 선택
        int randomIndex = Random.Range(1, spawnPoint.Length); 

        // int enemyIndex = spawnData[level].spriteType; // 추가코드 -> level에 따라 한 적만 생성
        
        // 랜덤 확률로 생성 적용 (level 1은 원거리적 + 기본적 생성)
        int enemyIndex;
        if(level == 1){
            float randomValue = Random.Range(0f, 1f);
            if( randomValue < 0.4f ){
                enemyIndex = 0;
            } else {
                enemyIndex = 1;
            }
        } else{
            enemyIndex = spawnData[level].spriteType;
        } 
        GameObject enemy = GameManager.instance.pool.Get(enemyIndex); // Get(0)에서 변경 -> 0번째 이너미만 있었을때
        
        // 랜덤 위치로 적 배치
        enemy.transform.position = spawnPoint[randomIndex].position;
        enemy.GetComponent<Enemy>().Init(spawnData[enemyIndex]);
        
        
        //enemy.GetComponent<Enemy>().Init(spawnData[level]); // 이너미의 소환 데이터 가져오기
        
    }
    void SpawnBoss() 
    {
        if (spawnPoint.Length <= 1) 
        {
            return;
        }

        int randomIndex = Random.Range(1, spawnPoint.Length);
        GameObject boss = GameManager.instance.pool.Get(2);
        
        if (boss == null)
        {
            Debug.LogError("Boss prefab is not set in PoolManager!");
            return;
        }
        
        boss.transform.position = spawnPoint[randomIndex].position;
        boss.GetComponent<Enemy>().Init(spawnData[2]);
        Debug.Log($"Boss spawned at: {boss.transform.position}");
        
    }
}


// 게임 타임에 따라 소환되는 적 난이도 달라지게 구현
// 인스펙터에서 지정 필요 (초기화 x)
[System.Serializable] public class SpawnData{

    public int spriteType; // 소환 캐릭터 -> 풀매니저의 인덱스 (0: enemy. 1:rangerenemy)
    public float spawnTime;
    public int health;
    public float speed;


}

