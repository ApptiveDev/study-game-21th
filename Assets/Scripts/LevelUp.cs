using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;

public class LevelUp : MonoBehaviour
{
    // 무기 레벨업 창 scale 조정 -> 안보일 때 0 0 0 으로
    RectTransform rect;
    Item[] items;
    void Awake()
    {
        rect = GetComponent<RectTransform>();
        items = GetComponentsInChildren<Item>(true);
    }

    public void Show() // 창 보이게 하는 함수
    {
        Next();
        rect.localScale = Vector3.one; // Vector3.one : ( 1 1 1 )
        GameManager.instance.Stop();
    }
    public void Hide()
    {
        rect.localScale = Vector3.zero; // 크기 ( 0 0 0 )
        GameManager.instance.Resume();
    }

    public void Select(int index)
    {
        items[index].OnClick();
    }

    void Next() // 무기 3개 랜덤으로 띄우는 함수 -> SHOW() 에서 불러옴
    {
        // 1. 모든 아이템을 일단 비활성화 시킨다..
        foreach (Item item in items)
        {
            item.gameObject.SetActive(false); // ..?
        }

        // 2. 비활성화 된 아이템 목록 중 3개 랜덤으로 true 상태
        int[] ran = new int[3];
        while(true){
            // 아이템 배열에 랜덤한 아이템 배정 완료
            ran[0] = Random.Range(0, items.Length);
            ran[1] = Random.Range(0, items.Length);
            ran[2] = Random.Range(0, items.Length); 

            if(ran[0]!=ran[1] && ran[1]!=ran[2] && ran[0]!=ran[2])
                break;
        }
              
        for(int index=0; index < ran.Length; index++)
            {   
                // 배정한거 집어넣어
                Item ranItem = items[ran[index]];

                // 3. 만렙 아이템은 소비아이템으로 대체
                if(ranItem.level == ranItem.data.damages.Length){
                    items[4].gameObject.SetActive(true);
                }
                else{
                    ranItem.gameObject.SetActive(true); // ? 40m
                }
            }
    }
}

