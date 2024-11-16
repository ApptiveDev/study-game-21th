using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField] GameObject EnemyMob;
    [SerializeField] GameObject EnemyBoss;
    [SerializeField] GameObject RealBoss;
    float curTimeMob = 0;
    float curTimeBoss = 0;
    float curTimeRealBoss = 0;
    GameObject player;
    void Start()
    {
        player = GameObject.Find("Player");
        for (int i = 0; i < 10; i++)
        {
            Instantiate(EnemyMob);
            EnemyMob.transform.position = RandomPosition();
        }

        StartCoroutine(SpawnRealBoss());
    }

    void Update()
    {
        curTimeMob += Time.deltaTime;
        curTimeBoss += Time.deltaTime;
        curTimeRealBoss += Time.deltaTime;

        if (curTimeMob >= 1)
        {
            SpawnMob();
            curTimeMob = 0;
        }
        if (curTimeBoss >= 4) 
        {
            SpawnBoss();
            curTimeBoss = 0;
        }
    }
    void SpawnMob()
    {
        Instantiate(EnemyMob);
        EnemyMob.transform.position = RandomPosition();
    }

    void SpawnBoss()
    {
        Instantiate(EnemyBoss);
        EnemyBoss.transform.position = RandomPosition();
    }
  
    public IEnumerator SpawnRealBoss()
    {
        while(curTimeRealBoss <= 40)
        {
            yield return new WaitForSeconds(40);
            Instantiate(RealBoss);
            RealBoss.transform.position = RandomPosition();
        }
    }
    Vector3 RandomPosition()
    {
        float x = Random.Range(-20f, 20f);
        float y = Random.Range(-20f, 20f);
        if (x <= player.transform.position.x + 3 && x >= player.transform.position.x - 3 && y <= player.transform.position.y + 3 && y >= player.transform.position.y - 3)
        {
            x += 3;
            y += 3;
        }

        return new Vector3(x, y, 0);
    }
}
