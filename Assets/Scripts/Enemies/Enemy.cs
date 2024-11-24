using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private float moveSpeed;
    private float hp;
    private float damage;

    public float MoveSpeed
    {
        get { return moveSpeed; }
        set { moveSpeed = value; }
    }
    public float Hp
    {
        get { return hp; }
        set { hp = value; }
    }
    public float Damage
    {
        get { return damage; }
        set { damage = value; }
    }
    public SpriteRenderer SpriteRenderer
    {
        get 
        { 
            if (spriteRenderer == null)
            {
                spriteRenderer = GetComponent<SpriteRenderer>();
            }
            return spriteRenderer; 
        }
        set { spriteRenderer = value; }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    // Update is called once per frame
    protected virtual void Update()
    {
        if (Hp <= 0)
        {
            Die();
        }
    }

    public IEnumerator BeAttackEffect()
    {
        Color originalColor = SpriteRenderer.color;
        SpriteRenderer.color = Color.red;

        yield return new WaitForSeconds(0.1f);
        SpriteRenderer.color = originalColor;
    }
    // HP -= damage
    public void BeAttacked(float damage)
    {
        Hp -= damage;
        StartCoroutine(BeAttackEffect());
    }
    public virtual void Die()
    {
        Instantiate(GameManager.Instance.GetExpPrefab(), transform.position, Quaternion.identity);
        GameManager.Instance.EnemyKilled();
        GameManager.Instance.Money += 500;
        Destroy(this.gameObject);
    }
}
