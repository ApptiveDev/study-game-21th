using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float damage;
    public float per;
    Rigidbody2D rigid;
    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    public void Init(float damage, Vector3 dir)
    {
        this.damage = damage;
        rigid.velocity = dir * 7f;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;
        rigid.velocity = Vector2.zero;
        gameObject.SetActive(false);
        GameManager.instance.curHp -= damage;
        if (GameManager.instance.curHp < 0)
        {
            collision.gameObject.GetComponent<Player>().Dead();
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Area")) return;
        gameObject.SetActive(false);
    }
}
