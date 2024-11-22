using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public float health = 1f;
    public float speed = 1f;
    public GameObject expPrefab;

    public void TakeDamage(float damage)
    {
        health -= damage;

        if(health <= 0)
        {
            HandleDeath();
        }
    }

    public bool IsDead()
    {
        return health <= 0;
    }

    public void HandleDeath()
    {
        SpawnExperience();
        Destroy(gameObject);
    }

    protected virtual void SpawnExperience()
    {
        if (expPrefab != null)
        {
            Instantiate(expPrefab, transform.position, Quaternion.identity);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Weapon"))
        {
            Bullet bullet = collision.GetComponent<Bullet>();
            if (bullet != null)
            {
                TakeDamage(bullet.damage);
            }

            Stone stone = collision.GetComponent<Stone>();
            if (stone != null)
            {
                TakeDamage(stone.damage);
            }
        }
    }
}
