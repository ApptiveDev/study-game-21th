using UnityEngine;

public class Arrow : MonoBehaviour
{
    private Transform target; 
    private float speed; 
    public int damage = 10; 

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

    private void Update()
    {
        if (target == null)
        {
            Destroy(gameObject); 
            return;
        }

        Vector3 direction = (target.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;


        if (Vector3.Distance(transform.position, target.position) < 0.2f)
        {
            HitTarget();
        }
    }

    void HitTarget()
    {
        if (target != null) 
        {
            EnemyHealth enemyHealth = target.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage); 
            }
        }

        Destroy(gameObject); 
    }

    private void OnDestroy()
    {

        ArrowSpawner spawner = FindObjectOfType<ArrowSpawner>();
        if (spawner != null)
        {
            spawner.currentArrow = null; 
        }
    }
}
