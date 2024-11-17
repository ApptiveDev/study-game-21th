using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombWeaponMovement : MonoBehaviour
{
    private float BombTimer = 1.0f;
    private float BombRad = 3.0f;

    private float Counter = 0.0f;
    private int BombEnemyRange = 10; // 기본 10으로 설정

    [SerializeField]
    private Collider2D[] colliders;

    [SerializeField]
    private LayerMask EnemyLayer;

    IEnumerator BombAttack()
    {
        yield return new WaitForSeconds(BombTimer);
        colliders = Physics2D.OverlapCircleAll(transform.position, BombRad, EnemyLayer);
        int limit = colliders.Length > BombEnemyRange ? BombEnemyRange : colliders.Length;
        if(colliders.Length > 0)
        {
            for(int i = 0; i<colliders.Length; i++)
            {
                try
                {
                    colliders[i].gameObject.GetComponent<EnemyMovement>().hp -= 100;
                }
                catch(NullReferenceException)
                {
                    Destroy(gameObject);
                }
            } 
        }
        // 삭제한다.
        SoundManager.Instance.PlaySound(SoundManager.Instance.bombWeaponSound);
        Destroy(gameObject);

    }

    // Start is called before the first frame update
    void Start()
    {
       StartCoroutine(BombAttack()); 
    }

    // Update is called once per frame
    void Update()
    {
    }
}
