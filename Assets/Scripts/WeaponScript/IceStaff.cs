using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class IceStaff : MonoBehaviour
{
    GameObject target;

    Vector3 moveDir;
    float moveSpeed = 4f;
    float iceDamage = 2f; // 닿았을 때 줄 얼림 데미지
    private AudioSource audioSource;

    private void Start() {
    List<GameObject> allEnemies = new List<GameObject>();
    allEnemies.AddRange(FindObjectsOfType<Enemy>().Select(e => e.gameObject));
    allEnemies.AddRange(FindObjectsOfType<RangeEnemy>().Select(re => re.gameObject));
    // 보스 추가 필요

    if (allEnemies.Count > 0) {
        target = allEnemies[Random.Range(0, allEnemies.Count)];
        moveDir = target.transform.position - transform.position;
        moveDir.Normalize(); 
    } else {
        Destroy(gameObject); 
    }
    audioSource = GetComponent<AudioSource>();

}

    private void Update() {
        if (target == null) { 
            Destroy(gameObject);
        }
        transform.position += moveDir * moveSpeed * Time.deltaTime;

}

    private void OnTriggerEnter2D(Collider2D collision) { 
        Debug.Log("Ice Hit!");
        Enemy enemy = collision.GetComponent<Enemy>();
        RangeEnemy rangeEnemy = collision.GetComponent<RangeEnemy>();
        BossEnemy bossEnemy = collision.GetComponent<BossEnemy>();

        
        if (enemy != null && enemy.gameObject == target) {
            if (audioSource != null) {
                audioSource.Play(); 
            }
            enemy.enemyHealth -= iceDamage;
            enemy.FreezeForSeconds(1f); // 1초 동안 얼림
            gameObject.SetActive(false);
        }

        if (rangeEnemy != null && rangeEnemy.gameObject == target) {
            if (audioSource != null) {
                audioSource.Play(); 
            }
            rangeEnemy.rangeEnemyHealth -= iceDamage;
            rangeEnemy.FreezeForSeconds(1f); // 1초 동안 얼림
            gameObject.SetActive(false);
        }
        
        if (bossEnemy != null && bossEnemy.gameObject == target) {
            if (audioSource != null) {
                audioSource.Play(); 
            }
            bossEnemy.bossHealth -= iceDamage;
            enemy.FreezeForSeconds(1f); // 1초 동안 얼림
            gameObject.SetActive(false);
        }

        }

}

