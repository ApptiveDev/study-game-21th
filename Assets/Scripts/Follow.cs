using UnityEngine;

public class Follow : MonoBehaviour // 플레이어 위치 고정 x -> UI 체력바가 플레이어에 고정되도록
{
    RectTransform rect; // UI는 RectTransform 사용

    void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    void FixedUpdate() // 플레이어에 물리 속성 적용
    {
        // 월드 좌표 != 스크린 좌표
        // 월드 좌표를 스크린으로 반환 -> WorldToScreenPoint
        rect.position = Camera.main.WorldToScreenPoint(GameManager.instance.player.transform.position);
    }
}
