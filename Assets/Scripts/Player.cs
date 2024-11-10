using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float moveSpeed = 2;
    private int level = 1;
    private float currExp = 0f;
    private float totalExp = 100f;

    // property
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
        GameManager.Instance.UpdateLevelText(Level);
        GameManager.Instance.UpdateExpSlider(CurrExp / TotalExp);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Exp"))
        {
            CurrExp += 10;
            Destroy(collision.gameObject);
        }
    }
}
