using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ExpBar : MonoBehaviour
{
    [SerializeField] Slider expBar;
    [SerializeField] TMP_Text levelText;
    [SerializeField] GameObject cardSystem;
    float maxExp = 5f;
    float currentExp = 0;
    const float increasingExp = 5f;
    int level = 1;


    void Start()
    {
        UpdateLevel();
        expBar.maxValue = maxExp;
        expBar.value = currentExp;
        
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("smallExp"))
        {
            AddExp(1);
            Destroy(collision.gameObject);
        }
        if (collision.CompareTag("largeExp"))
        {
            AddExp(5);
            Destroy(collision.gameObject);
        }
    }

    void AddExp(int exp)
    {
        CardSelect cardSelect = cardSystem.GetComponent<CardSelect>();
        currentExp += exp;
        expBar.value = currentExp;
        if (expBar.value >= expBar.maxValue && !cardSelect.isSelecting)
        {
            cardSelect.SelectCards();
            level += 1;
            UpdateLevel();
            currentExp = currentExp - expBar.maxValue;
            expBar.value = currentExp;
            expBar.maxValue += increasingExp;
        }
    }

    void UpdateLevel()
    {
        levelText.text = "Lv. " + level.ToString();
    }
}
