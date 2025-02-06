using UnityEngine;

public class Gear : MonoBehaviour
{
    public ItemData.ItemType type;
    public float rate; // 장비의 레벨별 데이터

    public void Init(ItemData data) // 초기화 함수
    {   
        // Basic Set
        name = "Gear " + data.itemId;
        transform.parent = GameManager.instance.player.transform; // 플레이어의 위치를 부모로 가짐
        transform.localPosition = Vector3.zero; // 그냥 position이 아닌 LocalPosition을 쓰는것 주의

        // Property Set
        type = data.itemType;
        rate = data.damages[0]; // 초기 rate는 data.damage배열의 첫번째로 함
        ApplyGear(); // 장비가 새로 추가될떄 로직적용 함수를 호출
    }

    public void LevelUp(float rate) // Gear는 Count 불필요
    {
        this.rate = rate;
        ApplyGear(); // 장비가 레벨업 할때 로직적용 함수를 호출
    }

    void ApplyGear() // 타입에 따라 적절하게 로직을 적용시켜주는 함수 추가
    {
        switch (type) {
            case ItemData.ItemType.Belt: // 그냥 글러브가 아닌 ItemData.ItemType.Glove인것 확인
                RateUP();
                break;
            case ItemData.ItemType.Shoe:
                SpeedUp();
                break;

        }
    }

    void RateUP() // 장갑의 기능인 연사력을 올리는 함수 작성
    {
        // Weapon은 Item.cs에 의해 player의 자식 오브젝트로 생성됨 -> 플레이어로 올라가서 모든 weapon을 가져옴
        Weapon[] weapons = transform.parent.GetComponentsInChildren<Weapon>(); 

        foreach(Weapon weapon in weapons) {
            switch(weapon.id) { // foreach문으로 하나씩 순회하면서 타입에 따라 속도 올리기
                case 0: // 근접무기일 경우
                    weapon.speed = 150 + (150 * rate);
                    break;
                default: 
                    weapon.speed = 0.3f * (1f - rate);    
                    break;
            }
        }
    }

    void SpeedUp() // 신발의 기능인 이동 속도를 올리는 함수 작성
    {
        float speed = DataManager.instance.nowPlayer.speed;
        GameManager.instance.player.speed = speed + speed * rate;
    }
}