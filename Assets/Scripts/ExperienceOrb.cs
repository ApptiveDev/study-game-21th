using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ExperienceOrb : MonoBehaviour
{
    public float speed = 5f; 
    private Transform player; 

    void Start()
    {
      
        player = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        if (player != null)
        {
           
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}

