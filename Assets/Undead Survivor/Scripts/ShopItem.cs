using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShopItem : MonoBehaviour
{
    public enum ItemType { Health, Speed, Weapon1, Weapon2 }
    public ItemType type;
    public Text costText;
    public static int[] upgradeCost = { 100, 100, 100, 100 };

    private void Awake()
    {
        for (int i = 0; i < 4; i++) SetCost(i);
    }

    //씬 나갔다 들어올 때 cost가 제대로 초기화 안됨, 왜?
    public void SetCost(int i)
    {
        costText.text = upgradeCost[i] + " Gold";
    }
    public void ItemUpgrade()
    {
        switch (type)
        {
            case ItemType.Health:
                if (upgradeCost[0] > ShopManager.instance.money) return;
                GameManager.instance.money -= upgradeCost[0];
                ShopManager.instance.SetMoney();
                upgradeCost[0] += 100;
                SetCost(0);
                GameManager.instance.maxHp *= 1.1f;
                break;
            case ItemType.Speed:
                if (upgradeCost[1] > ShopManager.instance.money) return;
                GameManager.instance.money -= upgradeCost[1];
                ShopManager.instance.SetMoney();
                upgradeCost[1] += 100;
                SetCost(1);
                break;
            case ItemType.Weapon1:
                if (upgradeCost[2] > ShopManager.instance.money) return;
                GameManager.instance.money -= upgradeCost[2];
                ShopManager.instance.SetMoney();
                upgradeCost[2] += 100;
                SetCost(2);
                break;
            case ItemType.Weapon2:
                if (upgradeCost[3] > ShopManager.instance.money) return;
                GameManager.instance.money -= upgradeCost[3];
                ShopManager.instance.SetMoney();
                upgradeCost[3] += 100;
                SetCost(3);
                break;
            default:
                break;
        }
    }
}
