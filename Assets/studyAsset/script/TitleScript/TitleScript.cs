using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScript : MonoBehaviour
{
    public void GoGameScene()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void GoShopScene()
    {
        SceneManager.LoadScene("ShopScene");
    }

    public void GoTitleScene()
    {
        SceneManager.LoadScene("TitleScene");
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
