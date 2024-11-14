using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    const float MAX_HP = 30f;

    private float moveSpeed = 2;
    private float hp = 30f;
    private int level = 1;
    private float currExp = 0f;
    private float totalExp = 100f;

    // property
    public float Hp
    {
        get { return hp; }
        set { hp = value; }
    }
    public int Level
    {
        get { return level; }
        set { level = value; }
    }

    public float CurrExp
    {
        get { return currExp; }  
        set { currExp = value; }   
    }

    public float TotalExp
    {
        get { return totalExp; }
        set { totalExp = value; }
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        Move();
        UpdateLevelAndExpCanvas();
        if (currExp >= totalExp)
        {
            LevelUpAndUpdateExp();
            GameManager.Instance.OpenUpgradeWindow();
        }
    }

    void Move()
    {
        //Input.GetKeyDown()
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += Vector3.up * moveSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += Vector3.left * moveSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position += Vector3.down * moveSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += Vector3.right * moveSpeed * Time.deltaTime;
        }
    }
    public void LevelUpAndUpdateExp()
    {
        level += 1;
        currExp = 0;
        totalExp += 50;
    }
    void UpdateLevelAndExpCanvas()
    {
        UIManager.Instance.UpdateLevelText(Level);
        UIManager.Instance.UpdateExpSlider(CurrExp / TotalExp);
    }

    void CheckDie()
    {
        if (Hp > 0) return;

        GameManager.Instance.GameOver();
    }
    public void BeAttacked(float damage)
    {
        this.hp -= damage;
        UIManager.Instance.UpdateHpSlider(Hp / MAX_HP);
        CheckDie(); 
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Exp"))
        {
            CurrExp += 10;
            Destroy(collision.gameObject);
        }
        else if (collision.CompareTag("Enemy"))
        {
            BeAttacked(collision.gameObject.GetComponent<Enemy>().Damage);
        }
    }
}
