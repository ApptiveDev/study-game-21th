using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject enemy;
    float curTime = 0;
    void Start()
    {
        
    }
    private void Update()
    {
        curTime += Time.deltaTime;
        if (curTime >= 1)
        {
            MakeEnemy();
            curTime = 0;
        }
    }

    void MakeEnemy() // 물체(원)를 생성한다.
    {
        Instantiate(enemy);
        enemy.transform.position = PickRandomPosition();
    }

    Vector3 PickRandomPosition() // 랜덤한 위치(벡터3)을 반환한다.
    {
        Vector2 playerPos = GameManager.instance.player.transform.position;
        float x = Random.Range(playerPos.x - 8, playerPos.x + 8);
        float y = Random.Range(playerPos.y - 8, playerPos.y + 8);

        return new Vector3(x, y, 0);
    }
}
