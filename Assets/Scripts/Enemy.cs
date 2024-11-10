using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private GameObject player;
    private GameObject expPrefab;
    private SpriteRenderer spriteRenderer;
    private Vector3 playerPosition;
    private Vector3 direction;
    private float moveSpeed = 1.0f;
    private float hp = 10.0f;

    public float Hp 
    {
        get { return hp; }
        set { hp = value; } 
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameManager.Instance.GetPlayer();
        expPrefab = GameManager.Instance.GetExpPrefab();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        if (this.hp <= 0)
        {
            Die();
        }
    }
    private void Move()
    {
        // Player 위치 가져오기
        playerPosition = player.transform.position;
        // Player 쪽으로 다가가기 위한 방향벡터 구하기
        direction = (playerPosition - transform.position).normalized;
        // 구한 방향벡터를 통해 플레이어 추격하기
        transform.position += direction * moveSpeed * Time.deltaTime;
    }
    IEnumerator BeAttackEffect()
    {
        Color originalColor = spriteRenderer.color;
        spriteRenderer.color = Color.red;

        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = originalColor;
    }
    // HP -= damage
    public void BeAttacked(float damage)
    {
        this.hp -= damage;
        StartCoroutine(BeAttackEffect());
    }

    void Die()
    {
        Instantiate(expPrefab, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}
