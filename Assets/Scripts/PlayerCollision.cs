using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] TMP_Text hpText;
    public int PlayerHP = 100;
    int hpLevel;

    private void Awake()
    {
        hpLevel = StoreSystem.Instance.hpLevel;
    }

    void Start()
    {
        PlayerHP += hpLevel * 5;
        UpdateHp();
    }

    void Update()
    {
        if (PlayerHP <= 0) Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy")) TakeDamage(1);
    }

    public void TakeDamage (int damage)
    {
        PlayerHP -= damage;
        UpdateHp();
    }

    void UpdateHp()
    {
        hpText.text = "HP : " + PlayerHP.ToString();
    }
}
