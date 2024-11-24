using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyState
{
    //void EnterState(RangeEnemy rangeEnemy); 
    void ExecuteAction(RangeEnemy rangeEnemy); 
    //void ExitState(RangeEnemy rangeEnemy); 
}

public class MoveState : IEnemyState
{
    /*public void EnterState(RangeEnemy rangeEnemy)
    {
        Debug.Log("Entering Move State");
    }*/

    public void ExecuteAction(RangeEnemy rangeEnemy)
    {
        Vector3 direction = Player.Instance.transform.position - rangeEnemy.transform.position;
        direction.Normalize();
        rangeEnemy.transform.position += direction * rangeEnemy.moveSpeed * Time.deltaTime;
    }

    /*public void ExitState(RangeEnemy rangeEnemy)
    {
        Debug.Log("Exiting Move State");
    }*/
}

public class AttackState : IEnemyState
{
    /*public void EnterState(RangeEnemy rangeEnemy)
    {
        Debug.Log("Entering Attack State");
    }*/

    public void ExecuteAction(RangeEnemy rangeEnemy)
    {
        rangeEnemy.currentDelay += Time.deltaTime;
        if (rangeEnemy.currentDelay >= rangeEnemy.spawnDelay)
        {
            GameObject.Instantiate(rangeEnemy.bullet, rangeEnemy.transform.position, Quaternion.identity);
            rangeEnemy.currentDelay = 0f;
        }
    }

    /*public void ExitState(RangeEnemy enemy)
    {
        Debug.Log("Exiting Attack State");
    }*/
}

public class RangeEnemy : MonoBehaviour
{
    public IEnemyState curState;
    public IEnemyState moveState = new MoveState();
    public IEnemyState attackState = new AttackState();
    
    public float ATTACK_RANGE = 4f;
    public float CHECK_DELAY = 1f;
    public float moveSpeed = 1.5f;
    public GameObject bullet;
    public float spawnDelay = 2f;
    public float currentDelay = 0f;
    public float rangeEnemyHealth = 20f;
    [SerializeField] private GameObject experienceOrbPrefab;
    public float enemyDamage = 10;
    private float frozenTime = 0f;

    void Start()
    {
        ChangeState(moveState);
        StartCoroutine(CheckState());
    }

    public void ChangeState(IEnemyState newState)
    {
        /*if (curState != null)
        {
            curState.ExitState(this);
        }*/
        curState = newState;
        //curState.EnterState(this);
    }

    private IEnumerator CheckState()
    {
        while (true)
        {
            float distanceToPlayer = Vector3.Distance(Player.Instance.transform.position, transform.position);
            if (distanceToPlayer <= ATTACK_RANGE)
            {
                ChangeState(attackState);
            }
            else
            {
                ChangeState(moveState);
            }
            yield return new WaitForSeconds(CHECK_DELAY);
        }
    }

    void Update()
    {
        if (curState != null)
        {
            curState.ExecuteAction(this);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player playerScript = collision.GetComponent<Player>();
            if (playerScript != null)
            {
                playerScript.TakeDamage((int)enemyDamage);
                Debug.Log("남은 체력: " + playerScript.playerHealth);
                Destroy(gameObject);
            }
            return;
        }

        if (collision.CompareTag("Weapon"))
        {
            if (rangeEnemyHealth <= 0)
            {
                Die();
            }
        }
    }

    private void Die()
    {
        SpawnExperienceOrb();
        Destroy(gameObject);
    }

    private void SpawnExperienceOrb()
    {
        if (experienceOrbPrefab != null)
        {
            Instantiate(experienceOrbPrefab, transform.position, Quaternion.identity);
        }
    }

    public void FreezeForSeconds(float duration)
    {
        frozenTime = duration;
    }
}

/* 원래 했던 코드
public class RangeEnemy : MonoBehaviour
{
    public enum EnemyState
    {
        MOVE,
        ATTACK
    }

    private EnemyState state;
    float ATTACK_RANGE = 4f;
    float CHECK_DELAY = 1f;
    float moveSpeed = 1.5f;
    [SerializeField] GameObject bullet;
    float spawnDelay = 2f;
    float currentDelay = 0f;
    public float rangeEnemyHealth = 20f;
    [SerializeField] private GameObject experienceOrbPrefab;
    public float enemyDamage = 10; 
    private float frozenTime = 0f;

    void Start()
    {
        StartCoroutine(CheckState());
    }

    public IEnumerator CheckState()
    {
        while (true)
        {
            if (Vector3.Distance(Player.Instance.transform.position, transform.position) <= ATTACK_RANGE)
            {
                state = EnemyState.ATTACK;
                Debug.Log("어택");
            }
            else
            {
                state = EnemyState.MOVE;
                Debug.Log("이동");
            }
            yield return new WaitForSeconds(CHECK_DELAY);        
            }
    }
    private void Act() {
        if (state != 0)
        {
            currentDelay += Time.deltaTime;
            if (currentDelay >= spawnDelay)
            {
                Instantiate(bullet, transform.position, Quaternion.identity);
                currentDelay = 0f;
            }
        } else
        {
            Vector3 direction = Player.Instance.transform.position - transform.position;
            direction.Normalize();
            transform.position += direction * moveSpeed * Time.deltaTime;
            
        }
    }
    void Update()
    {
        Act();
    }

        public void OnTriggerEnter2D(Collider2D collision)
    {
        // 플레이어와 충돌한 경우
        if (collision.CompareTag("Player"))
        {
            Player playerScript = collision.GetComponent<Player>();
            if (playerScript != null)
            {
                playerScript.TakeDamage((int)enemyDamage);
                Debug.Log("남은 체력: " + playerScript.playerHealth);
                Destroy(gameObject);
            }
            return;
        }

        // 무기와 충돌한 경우
        if (collision.CompareTag("Weapon"))
        {
            if (rangeEnemyHealth <= 0) {
                Die(); 
            }
        }
    }

        private void Die()
    {
        SpawnExperienceOrb();
        Destroy(gameObject);
    }

        private void SpawnExperienceOrb()
    {
        if (experienceOrbPrefab != null)
        {
            Instantiate(experienceOrbPrefab, transform.position, Quaternion.identity);
        }
    }

       public void FreezeForSeconds(float duration)
    {
        frozenTime = duration; 
    }

}
*/