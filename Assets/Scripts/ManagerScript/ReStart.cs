using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReStart : MonoBehaviour
{

    public void onClickRestart()
    {
        Time.timeScale = 1f;
        Player.Instance.gameObject.SetActive(true); // 초기화 필요할듯
        SceneManager.LoadScene("앱티브스터디");
    }
}
