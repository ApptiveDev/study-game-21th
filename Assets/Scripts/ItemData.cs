using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptble Object/ItemData")] // 에셋 직접 생성
public class ItemData : ScriptableObject
{
    public enum ItemType
    {
        Pet,
        Equipment,
        Etc
    }
    
    [Header("# Main Info")]
    public ItemType itemType;
    public int itemId;
    public string itemName;

    [TextArea]
    
    public string itemDesc;
    public Sprite itemIcon;

    [Header("# Level Data")]
    public float baseDamage;
    public int baseCount;
    public float[] damages;
    public int[] counts;

}
