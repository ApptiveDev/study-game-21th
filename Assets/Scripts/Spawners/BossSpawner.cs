using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    [SerializeField] private GameObject bossPrefab;  // 보스 프리팹
    public Transform spawnPoint;   // 보스가 생성될 위치
    private GameObject currentBoss; // 현재 생성된 보스 인스턴스
    public LevelManager levelManager;

    private void Start()
    {
        currentBoss = null; // 처음에 생성 안되게 하려고 했는데 구현이 잘 안됨
    }

    public void SpawnBoss()
    {
        if (levelManager.currentLevel % 3 == 0 && currentBoss == null)
        {
            currentBoss = Instantiate(bossPrefab, spawnPoint.position, Quaternion.identity);
            Debug.Log("보스 생성!");
        }
    }
}
