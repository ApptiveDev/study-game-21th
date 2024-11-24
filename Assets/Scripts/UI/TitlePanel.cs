using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitlePanel : MonoBehaviour
{
    private Button startButton;
    private Button shopButton;
    // Start is called before the first frame update
    void Start()
    {
        startButton = transform.Find("StartButton").GetComponent<Button>();
        shopButton = transform.Find("ShopButton").GetComponent<Button>();

        startButton.onClick.AddListener(GameManager.Instance.RestartGame);
        shopButton.onClick.AddListener(GameManager.Instance.LoadShopScene);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
