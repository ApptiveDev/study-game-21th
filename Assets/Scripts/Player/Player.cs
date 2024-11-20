using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    private Vector3 mousePosition;
    private Vector2 direction;

    private Rigidbody2D rb;

    [SerializeField] private float moveSpeed;

    private float xInput;
    private float yInput;

    private int facingDir = 1;
    private bool facingRight = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");

        Move();
        FlipController();
    }

    private void Move()
    {
        rb.velocity = new Vector2(xInput * moveSpeed, yInput * moveSpeed);
    }

    private void Flip()
    {
        facingDir = facingDir * -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }

    private void FlipController()
    {
        if (rb.velocity.x > 0 && !  facingRight)
        {
            Flip();
        }
        else if (rb.velocity.x < 0 && facingRight)
        {
            Flip();
        }
    }

  
}
