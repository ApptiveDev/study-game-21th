using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    private List<GameObject> weapons = new List<GameObject>();
    private float spawnRate = 1f;
    private int weaponIndex = 0;

    public Weapon[] GetRandomWeapons(int count = 3)
    {
        List<Weapon> selectedWeapons = new List<Weapon>();
        List<GameObject> copyOfWeapons = new List<GameObject>(weapons);

        int selectCount = Mathf.Min(count, copyOfWeapons.Count); // 선택될 무기의 개수는 현재 가지고 있는 무기 갯수를 초과할 수 없음

        for (int i = 0; i < selectCount; i++)
        {
            int index = Random.Range(0, copyOfWeapons.Count);
            selectedWeapons.Add(copyOfWeapons[index].GetComponent<Weapon>());
            copyOfWeapons.RemoveAt(index); // 중복 방지를 위해 선택한 무기를 리스트에서 제거
        }

        return selectedWeapons.ToArray();
    }

    void Start()
    {
        InitWeapons();
        StartCoroutine(SpawnWeaponPeriodically());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void InitWeapons()
    {
        GameObject magneticFieldInstance = Instantiate(GameManager.Instance.GetMagneticFieldPrefab(), this.transform.position, Quaternion.identity);
        magneticFieldInstance.transform.SetParent(GameManager.Instance.GetPlayer().transform);
        weapons.Add(GameManager.Instance.GetArrowPreafb());
        weapons.Add(GameManager.Instance.GetExplosiveBottlePrefab());
        weapons.Add(magneticFieldInstance);
    }

    IEnumerator SpawnWeaponPeriodically()
    {
        while (true)
        {
            while (weapons.Count == 0)
            {
                yield return null;  // 무기가 없으면 한 프레임 대기 후 다시 확인
            }

            yield return new WaitForSeconds(spawnRate);

            while (!weapons[weaponIndex].CompareTag("SpawnableWeapon")) // 전기장처럼 소환하는 무기가 아니라 종속된 무기면 소환하면 안됨
            {
                weaponIndex = (weaponIndex + 1) % weapons.Count; 
            }
            Instantiate(weapons[weaponIndex], this.transform.position, Quaternion.identity);
            weaponIndex = (weaponIndex + 1) % weapons.Count;
        }
    }

}
