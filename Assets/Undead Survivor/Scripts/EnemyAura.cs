using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAura : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            Debug.Log("enter");
        }
    }
}
