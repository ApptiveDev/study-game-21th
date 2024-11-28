using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHp : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private float Hp = 100;
    private float MaxHp = 100;

    public void SubtractHp(float operand)
    {
        Hp -= operand;
    }

    public void AddHp(float operand)
    {
        Hp += operand;
    }

    public void SetHpFull()
    {
        Hp = MaxHp;
    }

    public void AddMaxHp(float operand)
    {
        MaxHp += operand;
    }

    public void SubtractMaxHp(float operand)
    {
        MaxHp -= operand;
    }
    
    public float GetNowHp()
    {
        return Hp;
    }

    public float GetMaxHp()
    {
        return MaxHp;
    }
    void Start()
    {
        MaxHp += CoinManager.Instance.ShopPlusHp;
        Hp = MaxHp;
    }

    // Update is called once per frame
    void Update()
    {
        if(Hp < 0)
        {
            GameMgr.Instance.ShowDeathUI();
        }
    }
}
