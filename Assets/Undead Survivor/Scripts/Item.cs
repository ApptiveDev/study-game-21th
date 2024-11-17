using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public int itemId;

    public void LevelUpItem()
    {
        GameManager.instance.weaponManager0.LevelUp(itemId);
    }
}
