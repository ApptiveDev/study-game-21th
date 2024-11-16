using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemySystem : MonoBehaviour
{
    [SerializeField] GameObject Exp;
    GameObject player;
    void Start()
    {
        player = GameObject.Find("Player");
        
    }

    float speed = 3f;
    void Update()
    {
        transform.position += Vector3.right * (player.transform.position.x - transform.position.x) * Mathf.Abs((1/(player.transform.position.x - transform.position.x))) * speed * Time.deltaTime;
        transform.position += Vector3.up * (player.transform.position.y - transform.position.y) * Mathf.Abs((1/(player.transform.position.y - transform.position.y))) * speed * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Weapon"))
        {
            Destroy(gameObject);
            GameObject ExpInstance = Instantiate(Exp);
            ExpInstance.transform.position = transform.position;
        }
        if (collision.gameObject.CompareTag("LastingWeapon"))
        {
            Destroy(gameObject);
            GameObject ExpInstance = Instantiate(Exp);
            ExpInstance.transform.position = transform.position;
        }
        if (collision.gameObject.CompareTag("Stop")) speed = 0;
    }
}
