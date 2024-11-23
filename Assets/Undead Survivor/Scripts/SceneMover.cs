using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMover : MonoBehaviour
{
    public void LoadScene(int i)
    {
        switch(i)
        {
            case 1:
                SceneManager.LoadScene("Game");
                break;
            case 2:
                SceneManager.LoadScene("Lobby");
                break;
            default:
                SceneManager.LoadScene("Shop");
                break;
        }
    }
}
