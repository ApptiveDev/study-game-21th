using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoint; // 스폰 위치 배열
    public SpawnData[] spawnData; // 데이터 여러개니 배열..
    
    
    int level; // 레벨에 따라 다르게 소환 구현
    float timer;

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
        level = Mathf.Min(Mathf.FloorToInt(GameManager.instance.gameTime / 10f), spawnData.Length - 1); // 게임타임이 10초씩 늘어나면 난이도 증가
        
        if (timer > spawnData[level].spawnTime)
        {
            timer = 0;
            Spawn();
        }
    }

    void Spawn() // 수정
    {
        if (spawnPoint.Length <= 1)
        {
            Debug.LogError("No valid spawn points! Add child objects to Spawner.");
            return;
        }

        // 부모 객체(0번)를 제외한 랜덤 인덱스
        int randomIndex = Random.Range(1, spawnPoint.Length);
        GameObject enemy = GameManager.instance.pool.Get(0);

        if (enemy == null)
        {
            Debug.LogError("Failed to retrieve enemy from the pool!");
            return;
        }
        
        // 랜덤 위치로 적 배치
        enemy.transform.position = spawnPoint[randomIndex].position;
        enemy.GetComponent<Enemy>().Init(spawnData[level]); // 이너미의 소환 데이터 가져오기
        
        Debug.Log($"Enemy spawned at: {enemy.transform.position}");
    }
}


// 게임 타임에 따라 소환되는 적 난이도 달라지게 구현
// 인스펙터에서 지정 필요 (초기화 x)
[System.Serializable] public class SpawnData{

    public int spriteType; // 소환 캐릭터
    public float spawnTime;
    public int health;
    public float speed;


}

