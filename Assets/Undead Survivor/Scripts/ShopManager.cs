using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public static ShopManager instance;
    public Text moneyText;
    public int money;
    void Start()
    {
        money = GameManager.instance.money;
        SetMoney();
    }

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SetMoney()
    {
        money = GameManager.instance.money;
        moneyText.text = money + " Gold";
    }
}
