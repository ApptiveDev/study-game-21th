using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraMovement : MonoBehaviour
{
    GameObject player;
    void Start()
    {
        player = GameObject.Find("Player");
    }

    float BackgroundWidth = 8.9f;
    float BackgroundLength = 15.4f;
    float CameraX;
    float CameraY;
    void Update()
    {
        if (player.transform.position.x >= BackgroundWidth) CameraX = BackgroundWidth;
        else if (player.transform.position.x <= -BackgroundWidth) CameraX = -BackgroundWidth;
        else CameraX = player.transform.position.x;

        if (player.transform.position.y >= BackgroundLength) CameraY = BackgroundLength;
        else if (player.transform.position.y <= -BackgroundLength) CameraY = -BackgroundLength;
        else CameraY = player.transform.position.y;

        transform.position = new Vector3 (CameraX, CameraY, -10);
    }
}
