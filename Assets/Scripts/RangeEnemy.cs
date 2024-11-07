using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RangeEnemy : MonoBehaviour
{
    public enum EnemyState
    {
        MOVE,
        ATTACK
    }

    private EnemyState state;
    float ATTACK_RANGE = 10f;
    float CHECK_DELAY = 1f;
    float moveSpeed = 2f;
    [SerializeField] GameObject bullet;
    float spawnDelay = 2f;
    float currentDelay = 0f;

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

    
}
