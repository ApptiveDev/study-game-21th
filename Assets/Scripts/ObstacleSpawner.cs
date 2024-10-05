using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private GameObject Flower;

    // 현재시간 변수
    private float curTime = 0f;

    // 생성된 장애물들을 저장할 큐
    private Queue<GameObject> flowerQueue = new Queue<GameObject>();

    // 장애물의 최대 개수
    private int maxFlowerCount = 10;

    void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            GameObject newFlower = Instantiate(Flower); // 물체(꽃)를 생성한다.
            newFlower.transform.position = PickRandomPosition();
            newFlower.GetComponent<SpriteRenderer>().color = PickRandomColor();

            // 큐에 생성된 장애물을 추가
            flowerQueue.Enqueue(newFlower);
        }
    }

    void Update()
    {
        curTime += Time.deltaTime;
        if (curTime >= 1) 
        {
            MakeRandomFlower();
            curTime = 0;
        }
    }

    void MakeRandomFlower()
    {
        // 새 장애물 생성
        GameObject newFlower = Instantiate(Flower);
        newFlower.transform.position = PickRandomPosition();
        newFlower.GetComponent<SpriteRenderer>().color = PickRandomColor();

        // 큐에 새로 생성된 장애물을 추가
        flowerQueue.Enqueue(newFlower);

        // 만약 장애물이 10개를 넘으면, 가장 오래된 장애물 제거
        if (flowerQueue.Count > maxFlowerCount)
        {
            GameObject oldFlower = flowerQueue.Dequeue(); // 가장 오래된 장애물을 큐에서 제거
            Destroy(oldFlower); // 제거된 장애물 삭제
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
