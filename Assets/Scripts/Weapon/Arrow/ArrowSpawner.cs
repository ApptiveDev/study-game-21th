using UnityEngine;

public class ArrowSpawner : MonoBehaviour
{
    [SerializeField] private GameObject arrowPrefab;          
    [SerializeField] private Transform playerTransform;       
    [SerializeField] private Transform enemyContainer;          
    [SerializeField] private float arrowSpeed = 3f;            

    public GameObject currentArrow = null;             
    private bool isSpawningAllowed = true;             

    private void Update()
    {
 
        if (!IsEnemyAlive())
        {
            return;
        }

        
        if (currentArrow == null && isSpawningAllowed)
        {
            SpawnArrow();
        }
    }


    void SpawnArrow()
    {
        Transform target = FindClosestEnemy(); 

        if (target != null) 
        {
       
            if (arrowPrefab != null)
            {
                currentArrow = Instantiate(arrowPrefab, playerTransform.position, Quaternion.identity); 
                Arrow arrowScript = currentArrow.GetComponent<Arrow>(); 
                arrowScript.SetTarget(target); 
                arrowScript.SetSpeed(arrowSpeed); 
            }
            else
            {
                Debug.LogError("Arrow Prefab is missing! Please assign it in the Inspector.");
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
