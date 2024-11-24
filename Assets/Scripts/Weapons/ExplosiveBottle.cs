using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBottle : Weapon
{
    private GameObject explosionPrefab;

    private bool explosionTrigger = false;

    private float timeToExplode = 2f;
    private float currTime = 0f;

    public override void Upgrade() 
    { 
        Damage = Damage + 5f;
        WeaponInfo = "잠시 후 폭발하며 " + Damage.ToString() + "만큼의 데미지를 줌";
    }

    // Start is called before the first frame update
    void Start()
    {
        Damage = 10f;
        WeaponName = "폭발하는 병";
        WeaponInfo = "잠시 후 폭발하며 " + Damage.ToString() + "만큼의 데미지를 줌";
        explosionPrefab = GameManager.Instance.GetExplosionPrefab();
    }

    void Explode()
    {
        // 한 번만 폭발하도록
        if (explosionTrigger) return;

        // 폭발 애니메이션 실행
        GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        explosion.GetComponent<Explosion>().Damage = Damage;

        Destroy(this.gameObject, 0.1f);
        explosionTrigger = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (currTime >= timeToExplode)
        {
            Explode();
        } 
        currTime += Time.deltaTime;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy") {
            Explode(); 
        }
    }
}
