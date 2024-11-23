using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Arrow : MonoBehaviour
{
    GameObject target;

    Vector3 moveDir;
    public float moveSpeed = 3f;
    public float arrowDamage = 10f; 
    private AudioSource audioSource;

    private void Start() {
        // 적 종류가 많아지니까 리스트로 관리
        List<GameObject> allEnemies = new List<GameObject>();
        allEnemies.AddRange(FindObjectsOfType<Enemy>().Select(e => e.gameObject));
        allEnemies.AddRange(FindObjectsOfType<RangeEnemy>().Select(re => re.gameObject));
        //allEnemies.AddRange(FindAnyObjectByType<BossEnemy>().Select(be => be.gameObject));

        // 무작위로 타겟 선택
        if (allEnemies.Count > 0) {
            target = allEnemies[Random.Range(0, allEnemies.Count)];
            moveDir = target.transform.position - transform.position;
            moveDir.Normalize(); 
        } else {
            Destroy(gameObject); 
        }

        audioSource = GetComponent<AudioSource>();
        if (audioSource != null) {
            audioSource.Play(); // 생성될 때 소리 재생
        }

    }

    private void Update() {
        if (target == null) { // 타겟이 없다면 화살을 지움
            //gameObject.SetActive(false);
            Destroy(gameObject);
        }
        transform.position += moveDir * moveSpeed * Time.deltaTime; // 화살을 진행방향 만큼 이동
    }

    private void OnTriggerEnter2D(Collider2D collision) { // 충돌했을 때
        Enemy enemy = collision.GetComponent<Enemy>();
        RangeEnemy rangeEnemy = collision.GetComponent<RangeEnemy>();
        BossEnemy bossEnemy = collision.GetComponent<BossEnemy>();
        
        if ((enemy != null || rangeEnemy != null || bossEnemy != null) && collision.gameObject == target) {
            // 적의 체력을 감소시킴
            if (enemy != null) enemy.enemyHealth -= arrowDamage;
            if (rangeEnemy != null) rangeEnemy.rangeEnemyHealth -= arrowDamage;
            if (bossEnemy != null) bossEnemy.bossHealth -= arrowDamage;
            
            if ((enemy != null && enemy.enemyHealth <= 0) || 
                (rangeEnemy != null && rangeEnemy.rangeEnemyHealth <= 0) ||
                (bossEnemy != null && bossEnemy.bossHealth <= 0)) {
                Destroy(collision.gameObject); // 적 게임 오브젝트를 삭제
            }
            
            Destroy(gameObject); // 화살 오브젝트를 삭제
        }
    }

    public void IncreaseDamage(float amount)
    {
        arrowDamage += amount;
        Debug.Log("Arrow damage increased to: " + arrowDamage);
    }

}
