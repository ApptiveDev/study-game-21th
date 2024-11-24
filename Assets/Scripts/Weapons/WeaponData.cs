using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "Weapons/WeaponData", order = 1)]
public class WeaponData : ScriptableObject
{
    [SerializeField] private string weaponName;
    [SerializeField] private string weaponInfo;
    [SerializeField] private float damage;
    [SerializeField] private Sprite weaponIcon;

    public string WeaponName
    {
        get => weaponName;
        set => weaponName = value;
    }

    public string WeaponInfo
    {
        get => weaponInfo;
        set => weaponInfo = value;
    }

    public float Damage
    {
        get => damage;
        set
        {
            // 값 검증 로직 추가 가능 (예: 음수 방지)
            if (value >= 0)
                damage = value;
            else
                Debug.LogWarning("Damage cannot be negative!");
        }
    }

    public Sprite WeaponIcon
    {
        get => weaponIcon;
        set => weaponIcon = value;
    }
}

