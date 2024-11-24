using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverPanel : MonoBehaviour
{
    // Start is called before the first frame update
    private Button restartButton;
    private Button titleButton;
    void Start()
    {
        restartButton = transform.Find("RestartButton").GetComponent<Button>();
        restartButton.onClick.AddListener(GameManager.Instance.RestartGame);
        titleButton = transform.Find("TitleButton").GetComponent<Button>();
        titleButton.onClick.AddListener(GameManager.Instance.LoadTitleScene);
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
