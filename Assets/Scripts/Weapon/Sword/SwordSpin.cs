using UnityEngine;

public class SwordOrbit : MonoBehaviour
{
    public Transform player;           
    public float orbitRadius = 1.5f;   
    public float orbitSpeed = 180f;     
    public int swordDamage = 10;        

    private float currentAngle = 0f;   

    private void Update()
    {
        if (player != null)
        {
           
            currentAngle += orbitSpeed * Time.deltaTime;

            
            float radians = currentAngle * Mathf.Deg2Rad;
            float x = player.position.x + orbitRadius * Mathf.Cos(radians);
            float y = player.position.y + orbitRadius * Mathf.Sin(radians);

            transform.position = new Vector3(x, y, transform.position.z);

       
            Vector3 direction = transform.position - player.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
        if (enemyHealth != null)
        {
            enemyHealth.TakeDamage(swordDamage); 
        }
    }
}
