using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetProjectile : MonoBehaviour
{
    public float speed;
    public float damage;

    private Vector2 moveDirection;

    public void Init(Vector2 direction)
    {
        moveDirection = direction.normalized;
        Debug.Log($"Initialized Projectile Direction: {moveDirection}");
        transform.rotation = Quaternion.FromToRotation(Vector3.up, moveDirection);
    }

    void Update()
    {
        transform.position += (Vector3)moveDirection * speed * Time.deltaTime;
        Debug.Log($"Projectile Moving: {transform.position}");
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(!collision.CompareTag("Enemy"))
            return;
        
        if (collision.CompareTag("Enemy")){
            Debug.Log($"Projectile hit {collision.name}");
            Destroy(gameObject);
        }
    }
}
