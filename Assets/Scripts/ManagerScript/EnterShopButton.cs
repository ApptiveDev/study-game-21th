using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterShopButton : MonoBehaviour
{
    public void onClickEnterShop()
    {
        SceneManager.LoadScene("Shop");
    }
}
