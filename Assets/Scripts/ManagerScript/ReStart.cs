using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReStart : MonoBehaviour
{

    public void onClickRestart()
    {
        Time.timeScale = 1f;
        Player.Instance.gameObject.SetActive(true); 
        Player.Instance.StartNewGame();
        SceneManager.LoadScene("Lobby");
    }
}
