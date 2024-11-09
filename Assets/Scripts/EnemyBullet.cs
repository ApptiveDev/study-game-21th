using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    Vector3 moveDir;
    public float moveSpeed = 3f;
    public float bulletDamage = 5f;

    void Start()
    {
        moveDir = Player.Instance.transform.position - transform.position;
        moveDir.Normalize();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += moveDir * moveSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player"))
        {
            if (Player.Instance != null)
            {
                Player.Instance.playerHealth -= bulletDamage;
                Player.Instance.TakeDamage((int)bulletDamage);
                Debug.Log("남은 체력: " + Player.Instance.playerHealth);
                Destroy(gameObject);
            }
            gameObject.SetActive(false);
        }
    }
}
