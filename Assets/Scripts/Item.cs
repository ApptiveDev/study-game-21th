using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public ItemData data;
    public int level; // 아이템 레벨 플레이 경험치마다 달라지게
    public Weapon weapon;
    public Gear gear;
    
    Image icon;
    Text textLevel;
    Text textName;
    Text textDesc;

    void Awake()
    {
        icon = GetComponentsInChildren<Image>()[1];
        icon.sprite = data.itemIcon; // 아이템 데이터에 연결되어있는 아이콘으로 초기화 !

        Text[] texts = GetComponentsInChildren<Text>();
        
        // 인스펙터 창 순서대로 0 1 2
        textLevel = texts[0];
        textName = texts[1];
        textDesc = texts[2];
        textName.text = data.itemName; // 데이터에서 가져오기
    }

    void OnEnable()
    {
        // 원거리 공격은 기본으로 제공 -> UI 처음부터 Lv 2 표시 수정
        if (data.itemType == ItemData.ItemType.Range){
            textLevel.text = "Lv. " + level;
        }
        else{
            textLevel.text = "Lv. " + (level + 1);
        }

        switch(data.itemType){
            // 무기는 매개변수를 2개 받음 -> damages, counts
            // * 100 : 백분율로 수치 지정했으니 설명란엔 *100
            case ItemData.ItemType.Melee:
            case ItemData.ItemType.Range:
                textDesc.text = string.Format(data.itemDesc, data.damages[level] * 100, data.counts[level]);
                break;
            case ItemData.ItemType.Glove:
            case ItemData.ItemType.Shoe:
                textDesc.text = string.Format(data.itemDesc, data.damages[level] * 100);
                break;
            default:
                textDesc.text = string.Format(data.itemDesc);
                break;
        }
    }
  
    public void OnClick() // 버튼 클릭했을 때 인스펙터에서 이벤트 함수 연결할 수 있게 함스 작성
    {
        switch(data.itemType) // 클릭한 데이터의 아이템타입이 ... 일 때
        {
            case ItemData.ItemType.Melee: // 무기 1은 적용안되는 에러 -> break; 로 빠져나와서 . 바본가 삭제함
            case ItemData.ItemType.Range:
                if(level == 0){
                    GameObject newWeapon = new GameObject();
                    weapon = newWeapon.AddComponent<Weapon>(); // AddConponent : Weapon 속성 추가
                    weapon.Init(data);
                }
                else{
                    float nextDamage = data.baseDamage;
                    int nextCount = 0;

                    nextDamage += data.baseDamage * data.damages[level]; // 스크립트블 데이터 0.5 1 1.5 ... 일케 구현해서 백분율로 그냥 곱해준거
                    nextCount += data.counts[level];

                    weapon.LevelUp(nextDamage, nextCount);
                }
                level++;
                break;
            case ItemData.ItemType.Glove:
            case ItemData.ItemType.Shoe:
                if(level == 0){
                    GameObject newGear = new GameObject();
                    gear = newGear.AddComponent<Gear>(); // AddConponent : Weapon 속성 추가
                    gear.Init(data);
                }
                else{
                    float nextRate = data.damages[level];
                    gear.LevelUp(nextRate);
                }
                level++;
                break;
            case ItemData.ItemType.Heal:
                // 일회성 아이템 한번에 구현 -> 블럭 바깥에 있던 level++ 개인으로 적용
                GameManager.instance.health = GameManager.instance.maxHealth;
                break;
        }

        

        if(level == data.damages.Length) // 최대 레벨 (damage 레벨로)이 되었을 때 인식 불가
        {
            GetComponent<Button>().interactable = false; // 버튼은 .interactable = false
        }
    }
}
    
