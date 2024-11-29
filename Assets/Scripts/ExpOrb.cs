using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpOrb : MonoBehaviour
{
    public int expValue;
    public void Init(int value)
    {
        expValue = value;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            if(GameManager.instance != null){
                GameManager.instance.GetExp(expValue);
            }
            Destroy(gameObject);
        }
    }
}
