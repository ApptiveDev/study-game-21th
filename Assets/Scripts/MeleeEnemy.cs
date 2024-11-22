using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : EnemyBase
{
    public GameObject player;
    // void Start()
    // {
    //     player = GameManager.instance.player.transform;    
    // }

    void Update()
    {
        if(player == null || !GameManager.instance.isLive)
            return;

        MoveTowardsPlayer();        
    }

    void MoveTowardsPlayer()
    {
        Vector3 direction = (player.transform.position - transform.position).normalized;    
        transform.position += direction * speed * Time.deltaTime;
    }
}
