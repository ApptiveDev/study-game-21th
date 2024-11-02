using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnWeapon : MonoBehaviour
{
    [SerializeField] GameObject Bullet;
    [SerializeField] GameObject BlackHole;
    [SerializeField] GameObject StopGas;
    float curTimeBullet = 0;
    float curTimeBlackHole = 0;
    float curTimeStopGas = 0;
    GameObject player;
    void Start()
    {
        player = GameObject.Find("Player");
    }

    void Update()
    {
        curTimeBullet += Time.deltaTime;
        if (curTimeBullet >= 0.25f)
        {
            SpawnBullet();
            curTimeBullet = 0;
        }

        curTimeBlackHole += Time.deltaTime;
        if (curTimeBlackHole >= 3f)
        {
            SpawnBlackHole();
            curTimeBlackHole = 0;
        }

        curTimeStopGas += Time.deltaTime;
        if (curTimeStopGas >= 5f)
        {
            SpawnStopGas();
            curTimeStopGas = 0;
        }
    }

    void SpawnBullet()
    {
        GameObject bulletInstance = Instantiate(Bullet);
        bulletInstance.transform.position = PlayerPosition();

        BulletSystem bulletSystem = bulletInstance.GetComponent<BulletSystem>();
        bulletSystem.BulletDirection(player.transform.rotation);
    }

    void SpawnBlackHole()
    {
        GameObject blackholeInstance = Instantiate(BlackHole);
        blackholeInstance.transform.position = PlayerPosition();

        BlackHoleSystem blackholeSystem = blackholeInstance.GetComponent<BlackHoleSystem>();
        blackholeSystem.BlackHoleDirection(player.transform.rotation);
    }

    void SpawnStopGas()
    {
        GameObject stopgasInstance = Instantiate(StopGas);
        stopgasInstance.transform.position = PlayerPosition();
    }
    Vector3 PlayerPosition()
    {
        return new Vector3(player.transform.position.x, player.transform.position.y, 0);
    }
}
