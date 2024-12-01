using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField] private ObjectPool mobPool;
    [SerializeField] private ObjectPool bossPool;
    [SerializeField] private GameObject enemyBoss;
    [SerializeField] private GameObject RealBoss;

    float curTimeMob = 0f;
    float curTimeBoss = 0f;
    float curTimeRealBoss = 0f;

    GameObject player;

    void Start()
    {
        player = GameObject.Find("Player");

        StartCoroutine(SpawnRealBoss());
        StartCoroutine(SpawnBoss());
    }

    void Update()
    {
        curTimeMob += Time.deltaTime;
        curTimeBoss += Time.deltaTime;

        if (curTimeMob >= 1f)
        {
            SpawnMob();
            curTimeMob = 0f;
        }
    }

    void SpawnMob()
    {
        GameObject mob = mobPool.GetObject();
        mob.transform.position = RandomPosition();
    }

    public IEnumerator SpawnBoss()
    {
        yield return new WaitForSeconds(4f);
        Instantiate(enemyBoss).transform.position = RandomPosition();
    }

    public IEnumerator SpawnRealBoss()
    {
        yield return new WaitForSeconds(60f);
        Instantiate(RealBoss).transform.position = RandomPosition();
    }

    Vector3 RandomPosition()
    {
        float x = Random.Range(-20f, 20f);
        float y = Random.Range(-20f, 20f);

        if (x <= player.transform.position.x + 3 && x >= player.transform.position.x - 3 &&
            y <= player.transform.position.y + 3 && y >= player.transform.position.y - 3)
        {
            x += 3;
            y += 3;
        }

        return new Vector3(x, y, 0);
    }
}
