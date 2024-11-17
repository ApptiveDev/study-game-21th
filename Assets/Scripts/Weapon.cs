using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Weapon : MonoBehaviour
{
    public float speed = 10f; 
    public GameObject experienceorbPrefab;
    private Vector2 shootDirection;
    public Transform player;

    void Start()
    {
        
        if (player == null)
        {
            player = GameObject.FindWithTag("Player").transform; 
        }

        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");


        Vector2 moveDirection = new Vector2(moveX, moveY).normalized;

        shootDirection = moveDirection;
    }

    void Update()
    {
        
        transform.Translate(shootDirection * speed * Time.deltaTime);
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Instantiate(experienceorbPrefab, transform.position, Quaternion.identity);

            Destroy(collision.gameObject);

            Destroy(gameObject);
        }
        
    }
}




