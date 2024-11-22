using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public ItemData data;
    public int level;

    Image icon;
    Text textLevel;
    Text textName;
    Text textDesc;

    void Awake()
    {
        icon = GetComponentsInChildren<Image>()[1];
        icon.sprite = data.itemIcon;

        Text[] texts = GetComponentsInChildren<Text>();
        textLevel = texts[0];
        textName = texts[1];
        textDesc = texts[2];
        textName.text = data.itemName;
    }

    void OnEnable()
    {
        textLevel.text = "Lv." + (level + 1);

        switch(data.itemType){
            case ItemData.ItemType.Pet:
                textDesc.text = string.Format(data.itemDesc);
                break;
            case ItemData.ItemType.Equipment:
                textDesc.text = string.Format(data.itemDesc, data.damages[level] * 100);
                break;
            case ItemData.ItemType.Etc:
                textDesc.text = string.Format(data.itemDesc);
                break;
            default:
                break;
        }
    }
  
    public void OnClick()
    {
        switch(data.itemType)
        {
            case ItemData.ItemType.Pet:
                break;
            case ItemData.ItemType.Equipment:
                break;
            case ItemData.ItemType.Etc:
                break;
        }

        level++;

        if(level == data.damages.Length)
        {
            GetComponent<Button>().interactable = false;
        }
    }
}
    
