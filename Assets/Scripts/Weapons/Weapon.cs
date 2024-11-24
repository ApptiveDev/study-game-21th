using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    private string weaponName;
    private string weaponInfo;
    private float damage;
    public Sprite weaponIcon;

    public string WeaponName
    {
        get { return weaponName; }
        set { weaponName = value; }
    }
    public string WeaponInfo
    {
        get { return weaponInfo; }
        set { weaponInfo = value; }
    }
    public float Damage
    {
        get { return damage; }
        set { damage = value; }
    }

    // 순수 가상함수
    public abstract void Upgrade();

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}