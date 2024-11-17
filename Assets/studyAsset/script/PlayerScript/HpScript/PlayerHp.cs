using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHp : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private float Hp;
    private float MaxHp;

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
        Hp = 100;
        MaxHp = 100;
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
