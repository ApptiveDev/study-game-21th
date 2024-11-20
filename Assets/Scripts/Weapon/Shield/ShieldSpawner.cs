using UnityEngine;

public class ShieldSpawner : MonoBehaviour
{
    [SerializeField] private GameObject shieldPrefab;          
    [SerializeField] private Transform playerTransform;        
    [SerializeField] private Transform enemyContainer;        
    [SerializeField] private float shieldSpeed = 10f;          

    public GameObject currentShield = null;                  
    private bool isSpawningAllowed = true;                   

    private void Update()
    {
 
        if (!IsEnemyAlive())
        {
            return;
        }

        if (currentShield == null && isSpawningAllowed)
        {
            SpawnShield();
        }
    }

    void SpawnShield()
    {
        Transform target = FindClosestEnemy(); 

        if (target != null) 
        {
            if (shieldPrefab != null)
            {
                currentShield = Instantiate(shieldPrefab, playerTransform.position, Quaternion.identity); 
                Shield shieldScript = currentShield.GetComponent<Shield>(); 
                shieldScript.SetTarget(target); 
                shieldScript.SetSpeed(shieldSpeed); 
            }
          
        }
    }

    Transform FindClosestEnemy()
    {
        Transform closestEnemy = null;
        float shortestDistance = Mathf.Infinity;

        foreach (Transform enemy in enemyContainer) 
        {
            if (enemy != null) 
            {
                float distanceToEnemy = Vector3.Distance(playerTransform.position, enemy.position);
                if (distanceToEnemy < shortestDistance)
                {
                    shortestDistance = distanceToEnemy;
                    closestEnemy = enemy;
                }
            }
        }

        return closestEnemy;
    }

    bool IsEnemyAlive()
    {
        foreach (Transform enemy in enemyContainer) 
        {
            if (enemy != null) 
            {
                return true;
            }
        }
        return false; 
    }
}
