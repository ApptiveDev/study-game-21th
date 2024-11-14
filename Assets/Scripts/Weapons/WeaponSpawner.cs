using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSpawner : MonoBehaviour
{
    private List<GameObject> weapons = new List<GameObject>();
    private float spawnRate = 1f;
    private int weaponIndex = 0;

    [SerializeField] private AudioClip spawnSound;  // 인스펙터에서 오디오 파일 할당
    private AudioSource audioSource;

    void Start()
    {
        InitWeapons();
        audioSource = gameObject.GetComponent<AudioSource>();
        StartCoroutine(SpawnWeaponPeriodically());
    }

    public Weapon[] GetRandomWeapons(int count = 3)
    {
        List<Weapon> selectedWeapons = new List<Weapon>();
        List<GameObject> copyOfWeapons = new List<GameObject>(weapons);

        int selectCount = Mathf.Min(count, copyOfWeapons.Count);

        for (int i = 0; i < selectCount; i++)
        {
            int index = Random.Range(0, copyOfWeapons.Count);
            selectedWeapons.Add(copyOfWeapons[index].GetComponent<Weapon>());
            copyOfWeapons.RemoveAt(index);
        }

        return selectedWeapons.ToArray();
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
                yield return null;
            }

            yield return new WaitForSeconds(spawnRate);

            while (!weapons[weaponIndex].CompareTag("SpawnableWeapon"))
            {
                weaponIndex = (weaponIndex + 1) % weapons.Count;
            }

            // 무기 소환
            Instantiate(weapons[weaponIndex], this.transform.position, Quaternion.identity);

            // 소리 재생
            if (audioSource != null && spawnSound != null)
            {
                audioSource.PlayOneShot(spawnSound);
            }

            weaponIndex = (weaponIndex + 1) % weapons.Count;
        }
    }
}
