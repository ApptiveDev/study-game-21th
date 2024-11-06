using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class StatManager : MonoBehaviour
{
    private static StatManager _instance;
    public static StatManager Instance
    {
        get 
        {
            return _instance;
        }
    }
    public List<string> weaponData = new List<string>();
    public GameObject weaponChoicePanel;
    

    private void Awake() {
        if (_instance == null) {
            _instance = this;
        } else {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        weaponData.Add("Arrow\nDamage + 2");
        weaponData.Add("Guard\nSpeed + 5");
        weaponData.Add("Ice\nFrozen Time + 1");

        weaponChoicePanel.SetActive(false);
    }

    private void RandomWeaponSelect() 
    {
        List<int> selectedIndexes = new List<int>();

        while (selectedIndexes.Count < 3) {
            int i = Random.Range(0, weaponData.Count); 
            if (selectedIndexes.Contains(i))
            {
                continue;
            } else 
            {
                selectedIndexes.Add(i);
            }
        }
        transform.Find("Choice1").GetComponentInChildren<TMP_Text>().text = weaponData[selectedIndexes[0]];
        transform.Find("Choice2").GetComponentInChildren<TMP_Text>().text = weaponData[selectedIndexes[1]];
        transform.Find("Choice3").GetComponentInChildren<TMP_Text>().text = weaponData[selectedIndexes[2]];
        transform.Find("Choice1").GetComponent<Button>().onClick.AddListener(() => {
            ApplyweaponChoice(selectedIndexes[0]);
        });
        transform.Find("Choice2").GetComponent<Button>().onClick.AddListener(() => {
            ApplyweaponChoice(selectedIndexes[1]);
        });
        transform.Find("Choice3").GetComponent<Button>().onClick.AddListener(() => {
            ApplyweaponChoice(selectedIndexes[2]);
        });
    }

    private void ApplyweaponChoice(int weaponIndex)
    {
        string selectedWeapon = weaponData[weaponIndex];

        if (selectedWeapon.Contains("Arrow\nDamage + 2"))
        {
            Arrow arrow = FindAnyObjectByType<Arrow>();
            if (arrow != null) {
                arrow.IncreaseDamage(2f);
            }
            CloseWeaponSelection();
        }

        if (selectedWeapon.Contains("Guard\nSpeed + 5"))
        {
            Guard guard = FindAnyObjectByType<Guard>();
            if (guard != null) {
                guard.IncreaseSpeed(5);
            }
            CloseWeaponSelection();
        }

        if (selectedWeapon.Contains("Ice\nFrozen Time + 1"))
        {
            {
            Enemy enemy = FindAnyObjectByType<Enemy>();
            
                enemy.IncreaseFrozenTime(1f);
            }
            CloseWeaponSelection();
        }
    }
    public void ShowWeaponSelection()  // 스탯 창 보이게 하는 함수
    {
        weaponChoicePanel.SetActive(true);
        Time.timeScale = 0f;
        RandomWeaponSelect();
    }

    public void CloseWeaponSelection() // 스탯 창 닫는 함수
    {
        weaponChoicePanel.SetActive(false);
        Time.timeScale = 1f;
    }
}
