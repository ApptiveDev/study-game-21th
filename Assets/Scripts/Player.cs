using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    float moveSpeed = 5;
    void Update()
    {
        transform.position += Vector3.right * Input.GetAxisRaw("Horizontal") * moveSpeed * Time.deltaTime;
        transform.position += Vector3.up * Input.GetAxisRaw("Vertical") * moveSpeed * Time.deltaTime;
    }
}
