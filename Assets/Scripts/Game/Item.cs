using Unity.XR.Oculus.Input;
using UnityEngine;
using UnityEngine.UI; // UI 컴포넌트(Image)를 쓰기위해서 사용해야하는 에셋

public class Item : MonoBehaviour
{
    public ItemData data; // 아이템 관리에 필요한 변수들 선언
    public int level;
    public Weapon weapon;
    public Gear gear; // 버튼 스크립트에서 새롭게 작성한 장비 타입의 변수 선언

    Image icon;
    Text textLevel;
    Text textName;
    Text textDesc;

    void Awake() 
    {
        // 자식 오브젝트의 컴포넌트가 필요하므로 GetComponetsInChilden 사용       
        // GetComponentsInChildren에서 두번째 값으로 가져오기 (첫번째는 자기자신)
        icon = GetComponentsInChildren<Image>()[1]; 
        icon.sprite = data.itemIcon;

        Text[] texts = GetComponentsInChildren<Text>();
        textLevel = texts[0]; // GetComponents의 순서는 계층구조의 순서에 맞게 배치
        textName = texts[1];
        textDesc = texts[2];
        textName.text = data.itemName;
    }

    void OnEnable() // 활성화 되었을 때 실행되는 함수
    {
        textLevel.text = "Lv." + (level + 1); // 레벨을 0이 아닌 1부터 시작

        switch (data.itemType) { // 아이템 타입에 따라 switch case문으로 로직 분리
            case ItemData.ItemType.Melee:
            case ItemData.ItemType.Range:
                // 설명에 데이터가 들어가는 자리는 {index} 형태로 작성
                textDesc.text = string.Format(data.itemDesc, data.damages[level] * 100, data.counts[level]); 
                // Format함수로 data.itemDesc와 함께 {index}를 data.damages와 data.counts로 변경
                // 데미지 %상승을 보여줄 땐 100을 곱한다
                break;
            case ItemData.ItemType.Belt: // 무기와 다르게 장비들의 data.itemDesc에는 damages만 들어가므로 다시 스크립트 작성
            case ItemData.ItemType.Shoe: 
                textDesc.text = string.Format(data.itemDesc, data.damages[level] * 100);
                break;   
            default: // 소비아이템의 경우  
                textDesc.text = string.Format(data.itemDesc);
                break;
        }
    }

    public void OnClick() // 버튼 클릭이벤트와 연결할 함수 추가
    {
        switch(data.itemType) { // 아이템 타입을 통해 switch case문 작성해두기
            case ItemData.ItemType.Melee: // 여러 개의 case를 붙여서 로직을 실행
            case ItemData.ItemType.Range:
                if (level == 0) {
                    GameObject newWeapon = new GameObject(); // 새로운 게임오브젝트를 코드로 생성
                    weapon = newWeapon.AddComponent<Weapon>(); // AddComponent 함수 반환값을 미리 선언해둔 Weapon변수에 저장
                    // AddComponent<T> = 게임오브젝트에 T 컴포넌트를 추가하는 함수 
                    weapon.Init(data);
                }
                else {
                    float nextDamage = data.baseDamage; 
                    int nextCount = data.baseCount;

                    nextDamage = data.baseDamage * data.damages[level];
                    nextCount = data.counts[level];

                    weapon.LevelUp(nextDamage, nextCount);
                }
                level++; // 레벨 값을 올리는 로직을 무기, 장비 case 안쪽으로 이동, 일회성 로직의 레벨이 오르는 것을 막기 위함
                break;
            case ItemData.ItemType.Belt:
            case ItemData.ItemType.Shoe:
                if (level == 0) {
                    GameObject newGear = new GameObject(); // 새로운 게임오브젝트를 코드로 생성
                    gear = newGear.AddComponent<Gear>(); // AddComponent 함수 반환값을 미리 선언해둔 Weapon변수에 저장
                    // AddComponent<T> = 게임오브젝트에 T 컴포넌트를 추가하는 함수 
                    gear.Init(data);
                }

                else {
                    float nextRate = data.damages[level];
                    gear.LevelUp(nextRate);
                }
                level++; // 레벨 값을 올리는 로직을 무기, 장비 case 안쪽으로 이동, 일회성 로직의 레벨이 오르는 것을 막기 위함
                break;
            case ItemData.ItemType.Heal: // 치료기능의 음료수 로직은 case문에서 바로 작성
                GameManager.instance.health = GameManager.instance.maxHealth;
                break;        
        }   

        if (level == data.damages.Length) { // 스크립트블 오브젝트에 작성한 레벨 데이터 개수를 넘기지 않게 로직 추가
            GetComponent<Button>().interactable = false; // 버튼의 interactable 옵션을 꺼서 선택되지 않도록 함
        }  
    }
}