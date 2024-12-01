using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public enum InfoType {Coin, MaxHealth, Speed};
    public InfoType type; // 선언한 열거형을 타입으로 변수 추가

    Text myText;

    void Awake()
    {
        myText = GetComponent<Text>();
    }

    void LateUpdate() 
    {
        if (myText == null) {
            return;
        }

        switch (type) {
            case InfoType.Coin:
                myText.text = string.Format("{0:F0}", DataManager.instance.nowPlayer.coin);
                break;
            case InfoType.MaxHealth:
                myText.text = string.Format("{0:F0}", DataManager.instance.nowPlayer.maxHealth);
                break; 
            case InfoType.Speed:
                myText.text = string.Format("{0:F0}", DataManager.instance.nowPlayer.speed);
                break;       
        }
    }

    public void OnClick()
    {
        switch (type) {
            case InfoType.MaxHealth:
                if (DataManager.instance.nowPlayer.coin >= 15) {
                    DataManager.instance.nowPlayer.coin -= 15;
                    DataManager.instance.nowPlayer.maxHealth += 10;
                }
                break;
            case InfoType.Speed:
                if (DataManager.instance.nowPlayer.coin >= 15) {
                    DataManager.instance.nowPlayer.coin -= 15;
                    DataManager.instance.nowPlayer.speed += 0.3f;
                }
                break;    
        }
    }  
}
