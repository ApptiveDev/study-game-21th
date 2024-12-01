using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptble Object/ItemData")] // 에셋 직접 생성
// create - Scriptble Object - ItemData 로 생성 가능
public class ItemData : ScriptableObject // 스크립터블오브젝트 상속
{
    public enum ItemType // 일회용 / 영구적 구분
    {
        Melee, Range, Glove, Shoe, Heal
        // Range : 관통력 증가 Glove : 연사력 증가
    }
    
    [Header("# Main Info")]
    public ItemType itemType; // 아이템 타입 속성 생성
    public int itemId;
    public string itemName;

    [TextArea]
    public string itemDesc;
    public Sprite itemIcon;

    [Header("# Level Data")]
    public float baseDamage; // 기본
    public int baseCount;
    public float[] damages; // 레벨에 따라 달라지는 --- -> 인스펙터에서 추가 백분율로 구현
    public int[] counts;
    [Header("# Weapon")]
    public GameObject projectile;

}
