using NUnit.Framework;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptble Object/ItemData")] // CreateAssetMenu = 커스텀 메뉴를 생성하는 속성
public class ItemData : ScriptableObject // 스크립트블 오브젝트 = 다양한 데이터를 저장하는 에셋
{
    public enum ItemType { Melee, Range, Belt, Shoe, Heal } // 근접공격, 원거리공격, 장갑, 신발, 체력회복
    
    [Header("# Main Info")] // 아이템의 각종 속성들을 변수로 작성하기
    public ItemType itemType;
    public int itemId; 
    public string itemName;

    [TextArea] // 인스팩터에 텍스트를 여러줄 넣을수있게 TextArea 속성 부터
    public string itemDesc; 
    public Sprite itemIcon; // 아이템 아이콘

    [Header("# Level Data")]
    public float baseDamage; // 0레벨 일때의 기본 공격력
    public int baseCount; // 0레벨 일때의 기본 카운트(관통, 근접무기갯수)
    public float[] damages;
    public int[] counts;

    [Header("# Weapon")]
    public GameObject projectile; // 투사체
}