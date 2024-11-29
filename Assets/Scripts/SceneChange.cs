using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public void ShowInGameScene()
    {
        SceneManager.LoadScene("InGame");
        //GameManager.instance.GameStart();
    }
    /*
    public void ShowInGameScene()
    {
        StartCoroutine(LoadSceneAndInitialize());
    }

    private IEnumerator LoadSceneAndInitialize()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("InGame");

    // 씬 로딩이 완료될 때까지 대기
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    yield return new WaitForEndOfFrame(); // 씬 완전히 로드 후 한 프레임 대기
    // GameManager 초기화
        GameManager.instance.GameStart();
    }
    */
    public void ShowCollectionScene()
    {
        SceneManager.LoadScene("__Shop");
    }

    public void ShowTitleScene()
    {
        SceneManager.LoadScene("Title");
    }

}
