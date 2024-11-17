using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//pool manager에서 받아온 weapon을 배치하고 관리함
public class WeaponManager : MonoBehaviour
{
    public int id;
    public int prefabId;
    public float damage;
    public int count;
    public float speed;
    float timer;
    Player player;

    private void Awake()
    {
        player = GetComponentInParent<Player>();
    }
    private void Start()
    {
        Init();
    }

    private void Update()
    {
        switch (id)
        {
            case 0:
                //근접 무기는 회전
                transform.Rotate(Vector3.back * speed * Time.deltaTime);
                break;
            case 1:
                //총알은 정해진 간격으로 발사
                timer += Time.deltaTime;
                if (timer > speed)
                {
                    timer = 0f;
                    fire();
                }
                break;
            default:
                break;
        }
    }
    public void Init()
    {
        switch(id)
        {
            case 0:
                speed = 150; //회전 속도
                PutWeapon();
                break;
            case 1:
                speed = 0.5f; //총알 발사 간격
                break;
            default:
                break;
        }
    }
    public void LevelUp(int item)
    {
        switch (item)
        {
            case 1:
                count++;
                Init();
                break;
            default:
                break;
        }
    }

    void PutWeapon()
    {
        for(int i=0; i<count; i++)
        {
            Transform bullet;
            //이미 weapon이 child에 있으면 pool에 get요청을 하지 않음
            if(i < transform.childCount)
            {
                bullet = transform.GetChild(i);
            } else
            {
                bullet = GameManager.instance.pool.Get(prefabId).transform;
                bullet.parent = transform; //player를 parent로
            }

            bullet.localPosition = Vector3.zero;
            bullet.localRotation = Quaternion.identity;

            Vector3 rotVec = Vector3.back * 360 / count * i;
            bullet.Rotate(rotVec);
            bullet.Translate(bullet.up * 1.5f, Space.World);
            bullet.GetComponent<Bullet>().Init(damage, -1, Vector3.zero);
        }
    }

    void fire()
    {
        if (!player.scanner.nearesttarget) return;

        Transform bullet = GameManager.instance.pool.Get(prefabId).transform;
        Vector3 targetPos = player.scanner.nearesttarget.position;
        Vector3 dir = targetPos - transform.position;
        dir = dir.normalized;
        
        bullet.position = transform.position;
        bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir);
        bullet.GetComponent<Bullet>().Init(damage, count, dir);
        GetComponent<AudioSource>().Play();
    }
}
