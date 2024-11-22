using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Change : MonoBehaviour
{
    public void ShowInGameScene()
    {
        SceneManager.LoadScene("InGame");
    }

    public void ShowCollectionScene()
    {
        SceneManager.LoadScene("Collection");
    }
}
