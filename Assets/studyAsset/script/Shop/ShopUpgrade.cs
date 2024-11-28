using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopUpgrade : MonoBehaviour
{
    int SHOP_ADD_HP = 10;
    float SHOP_ADD_ATTACK_SPEED = 0.1f;
    float SHOP_ADD_SPEED = 0.1f;
    int SHOP_PRICE = 10;
    // Start is called before the first frame update
    public void ShopAddHp()
    {
        if (CoinManager.Instance.Coin < SHOP_PRICE) { return; }
        else
        {
            CoinManager.Instance.Coin -= SHOP_PRICE;
            CoinManager.Instance.ShopPlusHp += SHOP_ADD_HP;
        }
    }
    public void ShopAddAttackSpeed()
    {
        if (CoinManager.Instance.Coin < SHOP_PRICE) { return; }
        else
        {
            CoinManager.Instance.Coin -= SHOP_PRICE;
            CoinManager.Instance.ShopPlusAttackSpeed += SHOP_ADD_ATTACK_SPEED;
        }
    }
    public void ShopAddSpeed()
    {
        if (CoinManager.Instance.Coin < SHOP_PRICE) { return; }
        else
        {
            CoinManager.Instance.Coin -= SHOP_PRICE;
            CoinManager.Instance.ShopPlusSpeed += SHOP_ADD_SPEED;
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
