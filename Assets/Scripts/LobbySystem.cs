using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbySystem : MonoBehaviour
{

    public void LoadStart()
    {
        SceneManager.LoadScene("InGame");
    }

    public void LoadStore()
    {
        SceneManager.LoadScene("Store");
    }
}
