using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoobyManager : MonoBehaviour
{
    public GameObject[] button;
    public Shop shop;
    public void ToGame()
    {
        SceneManager.LoadScene("gameScene");
    }

    public void ToShop()
    {
        button[0].SetActive(false);
        button[1].SetActive(false);
        button[2].SetActive(false);
        shop.gameObject.SetActive(true);
    }

    public void ToLobby()
    {
        button[0].SetActive(true);
        button[1].SetActive(true);
        button[2].SetActive(true);
        shop.gameObject.SetActive(false);
    }
}
