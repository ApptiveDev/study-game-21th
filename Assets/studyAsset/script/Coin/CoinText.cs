using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinText : MonoBehaviour
{
    [SerializeField] private TMP_Text coinText; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (CoinManager.Instance != null)
        {
            coinText.text = $"Coin: {CoinManager.Instance.Coin}";
        }
        else
        {
            coinText.text = "Coin: 0";

        }
            
    }
}
