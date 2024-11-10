using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet1 : MonoBehaviour
{
    // Start is called before the first frame update
    public float damage;
    public float per;
    Rigidbody2D rigid;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }
    public void Init(float damage, float per, Vector2 dir)
    {
        this.damage = damage;
        this.per = per;
        rigid.velocity = dir * 15f;
    }
}
