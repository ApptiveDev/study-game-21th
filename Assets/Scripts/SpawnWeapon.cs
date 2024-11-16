using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnWeapon : MonoBehaviour
{
    [SerializeField] GameObject Bullet;
    [SerializeField] GameObject BlackHole;
    [SerializeField] GameObject StopGas;
    public float BulletDelay = 0.5f;

    void Start()
    {
        StartCoroutine(SpawnBullet());
    }

    public IEnumerator SpawnBullet()
    {
        while (true)
        {
            yield return new WaitForSeconds(BulletDelay);
            GameObject bulletInstance = Instantiate(Bullet);
            bulletInstance.transform.position = transform.position;

            BulletSystem bulletSystem = bulletInstance.GetComponent<BulletSystem>();
            bulletSystem.BulletDirection(transform.rotation);
        }
    }
}

