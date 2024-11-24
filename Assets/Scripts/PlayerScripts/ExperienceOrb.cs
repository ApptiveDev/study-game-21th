using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceOrb : MonoBehaviour
{
    public int experienceValue = 20; // 구슬 먹었을 때 경험치 양
    
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player"))
        {
            LevelManager levelManager = collision.GetComponent<LevelManager>();
            if (levelManager != null)
            {
                levelManager.AddExperience(experienceValue);
                Debug.Log("경험치 획득: " + experienceValue); // 캐릭터에 충돌 시 경험치 추가
            }
            Destroy(gameObject);
        }
    }
}
