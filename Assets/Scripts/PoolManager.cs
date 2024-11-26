using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public GameObject[] prefabs;


    List<GameObject>[] pools;

    void Awake()
    {
        pools = new List<GameObject>[prefabs.Length];

        for (int index = 0; index < pools.Length; index++){
            pools[index] = new List<GameObject>();
        }
    }
    public GameObject Get(int index) // index에 포함된 게임 오브젝트 반환
    {
        // 에러 해결 시도 -> 배열 문제가 아녔삼 ㅎ ㅎ select 변수 없애니까 해결 왠지는..몰라
        if (index < 0 || index >= pools.Length)
        {
            return null;
        }
    
        foreach (GameObject item in pools[index])
        {
            if (!item.activeSelf) // 비활성화된 오브젝트를 발견하면 반환
            {
                item.SetActive(true);
                return item;
            }
        }

        GameObject newObject = Instantiate(prefabs[index], transform);
        newObject.SetActive(true); // 생성한 오브젝트를 활성화
        pools[index].Add(newObject); // 생성한 오브젝트를 풀에 추가
        return newObject; // 새 오브젝트 반환

    }
}
