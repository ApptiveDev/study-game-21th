using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    [SerializeField] private GameObject bossPrefab; // 보스 프리팹
    [SerializeField] private Transform spawnPoint;  // 보스가 생성될 위치
    [SerializeField] private LevelManager levelManager; // 레벨 매니저
    
    private GameObject currentBoss = null; // 현재 생성된 보스 인스턴스

    public void SpawnBoss()
    {
        // 레벨이 3의 배수이고 현재 보스가 없을 때만 보스를 생성
        if (levelManager != null && levelManager.currentLevel % 3 == 0 && currentBoss == null)
        {
            currentBoss = Instantiate(bossPrefab, spawnPoint.position, Quaternion.identity);
            Debug.Log("보스 생성!");
        }
    }

    public void BossDefeated()
    {
        if (currentBoss != null)
        {
            Destroy(currentBoss);
            currentBoss = null;
            Debug.Log("보스 제거!");
        }
    }
}
