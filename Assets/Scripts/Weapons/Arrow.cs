using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : Weapon
{
    // Start is called before the first frame update
    private GameObject enemy;
    private Vector3 moveDirection;

    private float fireSpeed = 5.0f;
    
    public override void Upgrade()
    {
        Damage = Damage + 2;
        WeaponInfo = "적을 유도하여 " + Damage.ToString() + "만큼의 데미지를 줌";
    }

    void Start()
    {
        Damage = 5f;
        WeaponName = "유도화살";
        WeaponInfo = "적을 유도하여 " + Damage.ToString() + "만큼의 데미지를 줌";
        enemy = GameObject.FindWithTag("Enemy");
        Destroy(this.gameObject, 5f);
    }

    // Update is called once per frame
    void Update()
    {
        ChaseEnemy();
    }
    void ChaseEnemy()
    {
        // 기존에 추격하던 적이 죽으면, 새로운 적을 찾음
        if (enemy == null) {
            enemy = GameObject.FindWithTag("Enemy");
        }

        if (enemy != null)
        {
            moveDirection = (enemy.transform.position - transform.position).normalized;
            transform.position += moveDirection * fireSpeed * Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<Enemy>().BeAttacked(Damage);
            Destroy(this.gameObject);
        }
    }
}
