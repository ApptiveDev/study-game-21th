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
        foreach(GameObject gameObject in button) {
            gameObject.SetActive(false);
        }

        shop.gameObject.SetActive(true);
    }

    public void ToLobby()
    {
        foreach(GameObject gameObject in button) {
            gameObject.SetActive(true);
        }
        
        shop.gameObject.SetActive(false);
    }
}
