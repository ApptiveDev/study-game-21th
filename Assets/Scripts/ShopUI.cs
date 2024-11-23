using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    [Header ("Shop Events")]
    [SerializeField] GameObject shopUI;
    [SerializeField] Button closeShopButton;

    void AddShopEvents()
    {
        closeShopButton.onClick.RemoveAllListeners();
        closeShopButton.onClick.AddListener (CloseShop);
    }

    void CloseShop()
    {
        shopUI.SetActive(false);
    }

}
