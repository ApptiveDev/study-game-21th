using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScene : MonoBehaviour
{
    public void LoadInGameScene()
    {
        Debug.Log("Loading InGame scene...");
        SceneManager.LoadScene("InGame");
    }
    public void LoadShopScene()
    {
        Debug.Log("Loading Shop scene...");
        SceneManager.LoadScene("Shop3");
    }
    public void LoadTitleScene()
    {
        Debug.Log("Loading Title scene...");
        SceneManager.LoadScene("Title");
    }
}
