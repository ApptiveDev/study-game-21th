using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    // .. 프리펩들을 보관한 변수
    public GameObject[] prefabs; // 프리펩들을 저장할 배열 변수 선언

    // .. 풀 담당을 하는 리스트들
    List<GameObject>[] pools; // 오브젝트 풀들을 저장한 배열 변수 선언

    void Awake()
    {
        pools = new List<GameObject>[prefabs.Length]; // 리스트 배열 변수 초기화할 때 크기는 프리펩 배열 길이 활용

        for (int index = 0; index < pools.Length; index++) { // for (반복문) : 시작문; 조건문; 증감문
            pools[index] = new List<GameObject>();   
        }
    }

    public GameObject Get(int index)
    {
        GameObject select = null;

        // .. 선택한 풀의 놀고있는 게임오브젝트 접근    
        foreach (GameObject item in pools[index]) { // foreach = 배열, 리스트들의 데이터를 순차적으로 접근하는 반복문    
            // .. 발견하면 select 변수에 할당
            if (!item.activeSelf) { // 내용물 오브젝트가 비활성화(대기상태)인지 확인
                select = item;
                select.SetActive(true); // 비활성화(대기 상태) 오브젝트를 찾으면 SetActive함수로 활성화
                break;
            }
        }
        // .. 못찾았으면? = 풀의 모든 게임오브젝트가 게임에 배치됨
        if (select == null) { // 미리 선언한 변수가 계속 비어있으면 생성로직으로 진입
            // .. 새롭게 생성하고 select 변수에 할당
            select = Instantiate(prefabs[index], transform); 
            // Instantiate = 원본 오브젝트를 복제하여 장면에 생성하는 함수
            // 생성한 오브젝트를 깔끔하게 정리하기위해 select안에 넣겠다
            pools[index].Add(select); // 생성된 오브젝트는 해당 오브젝트 풀 리스트에 Add함수로 추가
        }    
            

        return select;
    }
}