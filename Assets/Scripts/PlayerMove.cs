using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    [SerializeField] float moveSpeed = 2;

    private void Start()
    {
        
    }

    private void Update()
    {
        //Input.GetKeyDown()
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
    }
}
