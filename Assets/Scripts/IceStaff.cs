using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceStaff : MonoBehaviour
{
    GameObject target;

    Vector3 moveDir;
    float moveSpeed = 4f;
    float iceDamage = 2f; // 닿았을 때 줄 얼림 데미지

    private void Start() {
        Enemy foundEnemy = FindObjectOfType<Enemy>();
        if (foundEnemy != null) {
            target = FindObjectOfType<Enemy>().gameObject;
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
        if (enemy != null && enemy.gameObject == target ) { 
            enemy.enemyHealth -= iceDamage; 
            enemy.FreezeForSeconds(1f); // 1초 동안 얼림
                gameObject.SetActive(false); 
        }
    }

}
