using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Transform playerTransform;
    public GameObject player;
    private float speed = 3;
    void Start()
    {
        playerTransform = GetComponent<Transform>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.W)) {
            playerTransform.position += Vector3.up * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A)) {
            playerTransform.position += Vector3.left * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S)) {
            playerTransform.position += Vector3.down * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D)) {
            playerTransform.position += Vector3.right * speed * Time.deltaTime;
        }                        
    }
}
