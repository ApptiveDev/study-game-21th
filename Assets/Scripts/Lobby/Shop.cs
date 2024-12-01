using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public enum InfoType {Coin, MaxHealth};
    public InfoType type; // 선언한 열거형을 타입으로 변수 추가

    Text myText;

    void Awake()
    {
        myText = GetComponent<Text>();
    }

    void Start()
    {
        switch (type) {
            case InfoType.Coin:
                myText.text = string.Format("{0:F0}", DataManager.instance.nowPlayer.coin);
                break;
            case InfoType.MaxHealth:
                myText.text = string.Format("{0:F0}", DataManager.instance.nowPlayer.maxHealth);
                break;    
        }
    } 

    void LevelUp()
    {
        
    }  
}
