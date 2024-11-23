using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameSharedUI : MonoBehaviour
{
    // #region Singleton class: GameSharedUI

    // public static GameSharedUI instance;

    // void Awake()
    // {
    //     if (instance == null)
    //     {
    //         instance = this;
    //     }
    // }

    // #endregion


    [SerializeField] Text[] coinsUIText;


    void Start()
    {
        FindCoinsTextInHierarchy();
        UpdateCoinsUIText();
    }

    void FindCoinsTextInHierarchy() // 추후 삭제
    {
        // "Coin Info" 하위의 모든 TMP_Text 컴포넌트를 찾아 배열에 추가
        Transform coinInfoTransform = transform.Find("Canvas/Coin Info");
        if (coinInfoTransform != null)
        {
            coinsUIText = coinInfoTransform.GetComponentsInChildren<Text>();
        }
        else
        {
            Debug.LogError("Coin Info를 찾을 수 없습니다! 하이어라키 구조를 확인하세요.");
        }
    }

    public void UpdateCoinsUIText()
    {
        for (int i=0; i<coinsUIText.Length; i++)
        {
            SetCoinsText(coinsUIText[i], GameDataManager.GetCoins());
        }
    }

    void SetCoinsText (Text textMesh, int value)
    {
        if (value >= 1000)
            textMesh.text = string.Format("{0}K.{1}",(value/1000, GetFirstDigitFromNumber(value % 1000)));
        else   
            textMesh.text = value.ToString();
    }

    int GetFirstDigitFromNumber(int num)
    {
        return int.Parse (num.ToString()[0].ToString());
    }
}
