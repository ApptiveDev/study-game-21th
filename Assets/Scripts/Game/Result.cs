using UnityEngine;

public class Result : MonoBehaviour
{
    // 게임 결과의 타이틀 오브젝트는 모두 비활성화해둔다 
    // 부모 오브젝트인 GameResult.gameObject.SetActive(true)가 실행되어도 비활성화해둔 게임결과의 타이틀은 활성화 되지 않는다
    public GameObject[] titles;

    public void Lose() 
    {
        titles[0].SetActive(true);
    }

    public void Win()
    {
        titles[1].SetActive(true);
    }

    
}