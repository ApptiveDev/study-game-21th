using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    Transform transform;
    private float speed = 5f;
    public GameObject abc;
    public float timer;
    public float waitingtime;

    void getLeft()
    {
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
    }

    void getRight()
    {
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }

    }

    void getDown()
    {
        if (Input.GetKey(KeyCode.S))
        {
            transform.position += Vector3.down * speed * Time.deltaTime;
        }
    }

    void getUp()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += Vector3.up * speed * Time.deltaTime;
        }
    }
    void Start()
    {
        transform = GetComponent<Transform>();
        transform.position = new Vector3(0, 0, 0);
        timer = 0.0f;
        waitingtime = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        getUp();
        getRight();
        getDown();
        getLeft();
    }
}
