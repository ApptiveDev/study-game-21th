using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    float moveSpeed = 2;

    // 플레이어 이동 기능
    // 1. 키 입력을 받는다.
    // 2. 키 입력에 따라서, 플레이어 오브젝트를 이동시킨다.
    private void Update() // 매 프레임마다 동작을 한다. 컴퓨터나 동작하는 환경의 성능에 따라서 1초당 몇 프레임인지가 달라짐.
    {
        // Input.GetKeyDown()
        if (Input.GetKey(KeyCode.W))
        {
            Debug.Log("Press W!");
           transform.position += Vector3.up * moveSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
           Debug.Log("Press A!");
           transform.position += Vector3.left * moveSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
           Debug.Log("Press S!");
           transform.position += Vector3.down * moveSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
           Debug.Log("Press D!");
           transform.position += Vector3.right * moveSpeed * Time.deltaTime;
        }

        // Input.GetAxis()
        transform.position += Vector3.right * Input.GetAxisRaw("Horizontal") * moveSpeed * Time.deltaTime;
        transform.position += Vector3.up * Input.GetAxisRaw("Vertical") * moveSpeed * Time.deltaTime;
    }
}
