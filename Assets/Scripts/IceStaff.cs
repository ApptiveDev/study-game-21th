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
    public SoundManager.WeaponType weaponType = SoundManager.WeaponType.IceStaff; 
    private SoundManager soundManager;

    private void Start() {
    soundManager = FindObjectOfType<SoundManager>();    
    List<GameObject> allEnemies = new List<GameObject>();
    allEnemies.AddRange(FindObjectsOfType<Enemy>().Select(e => e.gameObject));
    allEnemies.AddRange(FindObjectsOfType<RangeEnemy>().Select(re => re.gameObject));

    if (allEnemies.Count > 0) {
        target = allEnemies[Random.Range(0, allEnemies.Count)];
        moveDir = target.transform.position - transform.position;
        moveDir.Normalize(); 
    } else {
        Destroy(gameObject); 
    }


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

        if (soundManager != null) {
            soundManager.PlayWeaponSound(weaponType);
        }

        if (enemy != null && enemy.gameObject == target) {
            enemy.enemyHealth -= iceDamage;
            enemy.FreezeForSeconds(1f); // 1초 동안 얼림
        }

        if (rangeEnemy != null && rangeEnemy.gameObject == target) {
            rangeEnemy.rangeEnemyHealth -= iceDamage;
            rangeEnemy.FreezeForSeconds(1f); // 1초 동안 얼림
        }
        
        gameObject.SetActive(false);
        }

}

