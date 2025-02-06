using UnityEngine;

public class LevelUp : MonoBehaviour
{
    RectTransform rect; // UI의 크기를 다룰때에는 RectTransform 사용하는것 확인
    Item[] items; // 레벨업 스크립트에서 아이템 배열 변수 선언 및 초기화

    void Awake() 
    {
        rect = GetComponent<RectTransform>();
        items = GetComponentsInChildren<Item>(true); // 자식오브젝트중 Item을 모두 가져옴, 비활성화되어있는 Item도 있기에 true 포함
    }

    public void Show() // 레벨업창이 보이도록 하는 함수
    {
        Next(); // 랜덤으로 아이템이 나타나도록 하는 함수
        rect.localScale = Vector3.one; // RectTransform.localScale을 1로 하여 보이도록함
        GameManager.instance.Stop(); // 레벨 업 창이 나타나거는 타이밍에 시간 Stop
    }

    
    public void Hide() // 다시 숨기는함수, 유니티의 기능중 버튼 OnClick에서 Level Up.Hide()를 호출하도록 설정
    {
        rect.localScale = Vector3.zero; // RectTransform.localScale을 1로 하여 숨김
        GameManager.instance.Resume(); // 이후 레벨업이 끝나면 다시 시간이 흐르도록 함
    }
    
    public void Select(int index) // 게임이 시작했을때 기본무기 지급을 위해 버튼을 대신 눌러주는 함수
    {
        items[index].OnClick();
    }

    void Next() // 아이템 스크립트의 랜덤 선택 확성화
    {
        // 1. 모든 아이템 비활성화
        foreach (Item item in items) { // foreach를 활용하여 모든 아이템 오브젝트 비활성화
            item.gameObject.SetActive(false);
        }

        // 2. 그중에서 랜덤 3개 아이템 활성화
        int[] ran = new int[3]; // 랜덤으로 활성화 할 아이템의 인덱스 3개를 담을 배열 선언
        while (true) { // 무한 반복문 사용시 게임이 정지할수 있기에 꼭 break조건문 사용
            ran[0] = Random.Range(0, items.Length); // 3개 데이터 모두 Random.Range 함수로 임의의 수 생성
            ran[1] = Random.Range(0, items.Length);
            ran[2] = Random.Range(0, items.Length);
            
            if(ran[0]!=ran[1] && ran[1]!=ran[2] && ran[0]!=ran[2]) { // 서로 비교하여 모두 같지 않으면 반복문을 빠져나가도록 작성
                break;
            }
        }

        for (int index=0; index<ran.Length; index++) { // for문을 통해 3개의 아이템 버튼 활성화
            Item ranItem = items[ran[index]];
            
            // 3. 만렙 아이템의 경우는 소비 아이템으로 대체 
            // 현재 활성화하려는 아이템의 레벨이 최대레벨(Item.data.damages배열의 길이와 같다)이라면 소비아이템으로 대체
            if (ranItem.level == ranItem.data.damages.Length) { 
                items[ran.Length-1].gameObject.SetActive(true); // 소비아이템이 하나라면 지정하여 SetActive해주면 됨
                // items[Random.Range(4, items.Length)].gameObject.SetActive(true) // 만약 소비아이템이 여러개라면 Random.Range활용
            } 
            else { 
                ranItem.gameObject.SetActive(true);
            }
        }
    }
}