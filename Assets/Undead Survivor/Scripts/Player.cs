using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    SpriteRenderer renderer;
    Rigidbody2D rigid;
    Animator animator;
    public float speed;
    public Vector2 inputVector;
    public int weapon = -1;
    public Bullet1 bullet1;
    float spawnDelay = 0.5f;
    float currentDelay = 0f;
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        renderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        inputVector.x = Input.GetAxisRaw("Horizontal");
        inputVector.y = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        Vector2 nextVector = inputVector.normalized * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVector);
        if (weapon > 0) attack();
    }
    private void LateUpdate()
    {
        animator.SetFloat("Speed", inputVector.magnitude);
        if(inputVector.x != 0)
        {
            renderer.flipX = inputVector.x < 0;
        }
    }

    void attack()
    {
        if(weapon == 1)
        {
            currentDelay += Time.deltaTime;
            if (currentDelay >= spawnDelay)
            {
                Instantiate(bullet1, transform.position, Quaternion.identity);
                currentDelay = 0f;
            }
        } else if(weapon == 2)
        {

        }
    }
}
