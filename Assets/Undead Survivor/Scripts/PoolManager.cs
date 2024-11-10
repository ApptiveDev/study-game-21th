using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    //프리펩을 보관할 변수
    public GameObject[] prefabs;
    List<GameObject>[] pools;

    private void Awake()
    {
        pools = new List<GameObject>[prefabs.Length];

        for(int i=0; i<pools.Length; i++)
        {
            pools[i] = new List<GameObject>();
        }
    }
    public GameObject Get(int index)
    {
        GameObject select = null;

        foreach (GameObject item in pools[index])
        {
            //선택한 풀의 비활성화된 게임오브젝트 접근
            if(!item.activeSelf)
            {
                //발견하면 select에 할당
                select = item;
                select.SetActive(true);
                return select;
            }
        }
        //못 찾았으면 새로 생성해서 select에 할당하고 pool에 넣어줌
        select = Instantiate(prefabs[index], transform);
        pools[index].Add(select);
        return select;
    }
}
