using UnityEngine;

public class HealthFollow : MonoBehaviour
{
    RectTransform rect; // transform과 달리 따로 변수를 선언해줘야한다

    void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    void FixedUpdate()
    {
        // rect.position = GameManager.instance.player.transform.position; -> 불가능 (월드 좌표와 스크린 좌표는 서로 다르다)
        rect.position = Camera.main.WorldToScreenPoint(GameManager.instance.player.transform.position); // WorldToScreenPoint = 월드 상의 오브젝트 위치를 스크린 좌표로 변환
    }
}