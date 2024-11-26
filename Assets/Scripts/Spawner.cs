using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoint; // 스폰 위치 배열
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
        timer += Time.deltaTime;

        if (timer >= 4f)
        {
            timer = 0;
            Spawn();
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
        int randomIndex = Random.Range(1, spawnPoint.Length);

        GameObject enemy = GameManager.instance.pool.Get(Random.Range(0, 2));

        if (enemy == null)
        {
            Debug.LogError("Failed to retrieve enemy from the pool!");
            return;
        }

        // 랜덤 위치로 적 배치
        enemy.transform.position = spawnPoint[randomIndex].position;
        Debug.Log($"Enemy spawned at: {enemy.transform.position}");
    }
}

