using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEditorInternal;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    //public int id;
    public GameObject bulletPrefab;
    public GameObject stonePrefab;
    public GameObject player;
    public float damage;
    // public int count;

    // private Scanner scanner;
    private float timer;
    // private Player player;

    public float bulletInterval = 2f;
    public float stoneInterval = 4f;

    void Awake()
    {
        // player = GetComponentInParent<Player>();
        // scanner = GetComponent<Scanner>();

        InvokeRepeating("SpawnBullet", 3f, bulletInterval);
        InvokeRepeating("SpawnStone", 5f, stoneInterval);
    }

    void SpawnBullet()
    {
        GameObject targetEnemy = GameObject.FindWithTag("Enemy");
        if(targetEnemy != null)
        {
            GameObject bullet = Instantiate(bulletPrefab, player.transform.position, Quaternion.identity);
            Vector3 direction = (targetEnemy.transform.position - transform.position).normalized;
        bullet.GetComponent<Bullet>().SetDirection(direction);

        Debug.Log($"총알 생성: 방향 = {direction}");
        }
        else
        {
            Debug.Log("적이 없습니다. 총알을 생성하지 않습니다.");
        }
        // Transform nearestTarget = scanner.GetNearest();
        // if(nearestTarget != null)
        // Vector3 direction = (bullet.transform.position - transform.position).normalized;
        // bullet.GetComponent<Bullet>().SetDirection(direction);
    
    }

    void SpawnStone()
    {
        GameObject stone = Instantiate(stonePrefab, player.transform.position, Quaternion.identity);
    }

    // public void LevelUp(float newDamage, int newCount)
    // {
    //     damage = newDamage;
    //     count += newCount;

    //     if (id == 0)
    //         Batch();
    // }

    // void Batch()
    // {
    //     for(int index = 0; index < count; index++)
    //     {
    //         GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity, transform); // 총알 생성
    //         bullet.transform.localPosition = Vector3.zero;
    //         bullet.transform.localRotation = Quaternion.identity;

    //         Vector3 rotVec = Vector3.forward * 360 * index / count;
    //         bullet.transform.Rotate(rotVec);
    //         bullet.transform.Translate(bullet.transform.up * 1.5f, Space.World);
    //         bullet.GetComponent<Bullet>().Init(damage, -1, Vector3.zero);
    //     }
    // }

    // void Fire()
    // {
    //     if(!player.scanner.nearestTarget)
    //         return;

    //     Vector3 targetPos = player.scanner.nearestTarget.position;
    //     Vector3 dir = (targetPos - transform.position).normalized;

    //     GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity); // 총알 생성
    //     bullet.transform.rotation = Quaternion.FromToRotation(Vector3.up, dir);
    //     bullet.GetComponent<Bullet>().Init(damage, count, dir);
    // }
}
