using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RangeEnemy : MonoBehaviour
{
    public Transform player;
    public float CHECK_DELAY = 1f;
    public float speed = 5f;
    public GameObject enemyweaponPrefab;
    public enum EnemyState
    {
        MOVE,
        ATTACK,
    }
    void Start()
    {
        StartCoroutine(CheckState());
    }
    public EnemyState state;
    public IEnumerator CheckState()
    {
        while (true)
        {
            if (Vector3.Distance(player.transform.position, transform.position)<30)
            {
                state = EnemyState.ATTACK;
            }
            else
            {
                state = EnemyState.MOVE;
            }
            yield return new WaitForSeconds(CHECK_DELAY);
        }
    }

    float timer = 0;
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (state == EnemyState.MOVE)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            transform.Translate(direction * speed * Time.deltaTime);
        }
        if(state == EnemyState.ATTACK && timer>1)
        {
            timer = 0;
            Instantiate(enemyweaponPrefab, transform.position, Quaternion.identity);
            
        }
    }
}
