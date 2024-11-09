using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditorInternal;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int id;
    public GameObject bulletPrefab; // 총알 프리팹을 인스펙터에서 할당
    public float damage;
    public int count;
    public float speed;

    private float timer;
    private Player player;

    void Awake()
    {
        player = GetComponentInParent<Player>();
    }

    void Start()
    {
        Init();
    }

    void Update()
    {
        // 무기의 종류에 따라 동작이 다름
        switch(id)
        {
            case 0:
                transform.Rotate(Vector3.back * speed * Time.deltaTime);
                break;

            default:
                timer += Time.deltaTime;
                if(timer > speed)
                {
                    timer = 0f;
                    Fire();
                }
                break;
        }
    }

    public void LevelUp(float newDamage, int newCount)
    {
        damage = newDamage;
        count += newCount;

        if (id == 0)
            Batch();
    }

    public void Init()
    {
        switch(id)
        {
            case 0:
                speed = 150; // 회전 무기의 속도
                Batch();
                break;

            default:
                speed = 0.3f; // 발사 무기의 발사 간격
                break;
        }
    }

    void Batch()
    {
        for(int index = 0; index < count; index++)
        {
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity, transform); // 총알 생성
            bullet.transform.localPosition = Vector3.zero;
            bullet.transform.localRotation = Quaternion.identity;

            Vector3 rotVec = Vector3.forward * 360 * index / count;
            bullet.transform.Rotate(rotVec);
            bullet.transform.Translate(bullet.transform.up * 1.5f, Space.World);
            bullet.GetComponent<Bullet>().Init(damage, -1, Vector3.zero);
        }
    }

    void Fire()
    {
        if(!player.scanner.nearestTarget)
            return;

        Vector3 targetPos = player.scanner.nearestTarget.position;
        Vector3 dir = (targetPos - transform.position).normalized;

        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity); // 총알 생성
        bullet.transform.rotation = Quaternion.FromToRotation(Vector3.up, dir);
        bullet.GetComponent<Bullet>().Init(damage, count, dir);
    }
}
