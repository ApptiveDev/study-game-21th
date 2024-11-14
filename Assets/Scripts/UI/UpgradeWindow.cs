using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeWindow : MonoBehaviour
{
    private GameObject[] upgradeSlots;  // 3개의 업그레이드 슬롯 UI 요소 (예: 버튼 등)
    private Weapon[] selectedWeapons;

    void Start()
    {
        List<GameObject> childObjectsList = new List<GameObject>();

        foreach (Transform child in transform)
        {
            childObjectsList.Add(child.gameObject);
        }

        upgradeSlots = childObjectsList.ToArray();
        gameObject.SetActive(false);
    }

    void Update()
    {
        
    }

    // 업그레이드 창에 무기 옵션을 표시
    public void ShowUpgradeOptions(Weapon[] weapons)
    {
        selectedWeapons = weapons;
        for (int i = 0; i < upgradeSlots.Length; i++)
        {
            if (i < selectedWeapons.Length)
            {
                upgradeSlots[i].SetActive(true);

                // 슬롯에 무기 이름과 정보 표시
                Text upgradeInfo = upgradeSlots[i].GetComponentInChildren<Text>();
                upgradeInfo.text = selectedWeapons[i].WeaponName + "\n\n" + selectedWeapons[i].WeaponInfo;

                // 각 슬롯에 클릭 이벤트 추가
                int index = i;  // 클로저 문제 방지
                Button button = upgradeSlots[i].GetComponent<Button>();
                if (button != null)
                {
                    button.onClick.RemoveAllListeners();  // 기존 리스너 제거
                    button.onClick.AddListener(() => OnUpgradeSelected(index));  // 업그레이드 선택 처리
                }
            }
            // 무기가 더 적은 경우 빈 슬롯에 대한 처리
            else
            {
                upgradeSlots[i].SetActive(false);
            }
        }
    }

    private void OnUpgradeSelected(int index)
    {
        // 선택된 무기 업그레이드
        GameManager.Instance.UpgradeWeapon(selectedWeapons[index]);
        gameObject.SetActive(false);
    }
}

