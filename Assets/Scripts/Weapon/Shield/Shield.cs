using UnityEngine;

public class Shield : MonoBehaviour
{
    private Transform target;
    private float speed;
    public int damage = 10;
    public float damageDuration = 2f; 
    private float damageTimer = 0f; 

    private bool isActive = true; 

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

     
        if (isActive)
        {
            damageTimer += Time.deltaTime;
            if (damageTimer >= damageDuration)
            {
                isActive = false; 
            }
        }
    }

    void HitTarget()
    {
        if (target != null && isActive)
        {
            EnemyHealth enemyHealth = target.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage((int)(damage * Time.deltaTime));
            }
        }

        if (!isActive)
        {
            Destroy(gameObject); 
        }
    }

    private void OnDestroy()
    {
       
        ShieldSpawner spawner = FindObjectOfType<ShieldSpawner>();
        if (spawner != null)
        {
            spawner.currentShield = null; 
        }
    }
}
