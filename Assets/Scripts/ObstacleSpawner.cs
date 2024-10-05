using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] obstacle;

    // 현재시간 변수
    private float curTime = 0f;

    // 생성된 장애물들을 저장할 큐
    private Queue<GameObject> obstacleQueue = new Queue<GameObject>();

    // 장애물의 최대 개수
    private int maxObstacleCount = 10;

    void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            MakeRandomObstacle();
            // GameObject newObstacle = Instantiate(obstacle[]); // 물체(꽃)를 생성한다.
            // newObstacle.transform.position = PickRandomPosition();
            // newObstacle.GetComponent<SpriteRenderer>().color = PickRandomColor();

            // // 큐에 생성된 장애물을 추가
            // obstacleQueue.Enqueue(newObstacle);
        }
    }

    void Update()
    {
        curTime += Time.deltaTime;
        if (curTime >= 1) 
        {
            MakeRandomObstacle();
            curTime = 0;
        }
    }

    void MakeRandomObstacle()
    {
        int index = UnityEngine.Random.Range(0, obstacle.Length);
        
        GameObject newObstacle = Instantiate(obstacle[index]); // 새 장애물 생성
        newObstacle.transform.position = PickRandomPosition();
        newObstacle.GetComponent<SpriteRenderer>().color = PickRandomColor();

        // 큐에 새로 생성된 장애물을 추가
        obstacleQueue.Enqueue(newObstacle);

        // 만약 장애물이 10개를 넘으면, 가장 오래된 장애물 제거
        if (obstacleQueue.Count > maxObstacleCount)
        {
            GameObject oldObstacle = obstacleQueue.Dequeue(); // 가장 오래된 장애물을 큐에서 제거
            Destroy(oldObstacle); // 제거된 장애물 삭제
        }
    }

    Vector3 PickRandomPosition() // 랜덤한 위치(벡터3)을 반환한다.
    {
        float x = Random.Range(-8f, 8f);
        float y = Random.Range(-4f, 4f);

        return new Vector3(x, y, 0);
    }

    Color PickRandomColor() // 랜덤한 색깔을 반환한다.
    {
        float r = Random.Range(0, 1f);
        float g = Random.Range(0, 1f);
        float b = Random.Range(0, 1f);

        return new Color(r, g, b);
    }
}
