using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopPanel : MonoBehaviour
{
    [SerializeField] private Text moneyText;
    [SerializeField] private Button backButton;

    // Start is called before the first frame update
    void Start()
    {
        moneyText.text = "Money: " + GameManager.Instance.Money.ToString();
        backButton.onClick.AddListener(GameManager.Instance.LoadTitleScene);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
