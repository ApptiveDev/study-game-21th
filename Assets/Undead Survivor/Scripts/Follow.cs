using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    RectTransform rect;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    private void FixedUpdate()
    {
        Transform playerPos = GameManager.instance.player.transform;
        rect.position = Camera.main.WorldToScreenPoint(playerPos.position);
    }
}
