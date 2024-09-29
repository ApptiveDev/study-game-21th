using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //키 입력을 받아서, 플레이어의 위치를 이동시킨다.
    //1. 키 입력을 받고
    //2. 플레이어 위치를 이동시킴

    [SerializeField] float speed = 2f;

    void Update()
    {
        //if (Input.GetKey(KeyCode.W))
        //{
        //    transform.position += speed * Vector3.up * Time.deltaTime;
        //}
        //if (Input.GetKey(KeyCode.A))
        //{
        //    transform.position += speed * Vector3.left * Time.deltaTime;
        //}
        //if (Input.GetKey(KeyCode.S))
        //{
        //    transform.position += speed * Vector3.down * Time.deltaTime;
        //}
        //if (Input.GetKey(KeyCode.D))
        //{
        //    transform.position += speed * Vector3.right * Time.deltaTime;
        //}

        transform.position += Input.GetAxisRaw("Horizontal") * Vector3.right * speed * Time.deltaTime;
        transform.position += Input.GetAxisRaw("Vertical") * Vector3.up * speed * Time.deltaTime;
    }
}
