using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] TMP_Text hpText;
    int PlayerHP = 100;
    void Start()
    {
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

    void TakeDamage (int damage)
    {
        PlayerHP -= damage;
        UpdateHp();
    }

    void UpdateHp()
    {
        hpText.text = "HP : " + PlayerHP.ToString();
    }
}
