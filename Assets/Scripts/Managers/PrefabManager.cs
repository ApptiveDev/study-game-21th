using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabManager : MonoBehaviour
{
    // GameSystem Prefabs
    private GameObject expPrefab;

    // Weapon Prefabs
    private GameObject arrowPrefab;
    private GameObject magneticFieldPrefab;
    private GameObject explosiveBottlePrefab;
    private GameObject explosionPrefab;

    // Enemy Prefabs
    private GameObject chasingEnemyPrefab;
    private GameObject rangeEnemyPrefab;
    private GameObject rangeEnemyBulletPrefab;
    private GameObject slimeBossPrefab;

    private void Awake()
    {
        LoadPrefabs();
    }

    private void LoadPrefabs()
    {
        LoadGameSystemPrefabs();
        LoadWeaponPrefabs();
        LoadEnemyPrefabs();
    }

    private void LoadGameSystemPrefabs()
    {
        expPrefab = Resources.Load<GameObject>("Prefabs/ExpPrefab");
        if (expPrefab == null)
        {
            Debug.LogError("No expPrefab in Resources.");
        }
    }
    private void LoadWeaponPrefabs()
    {
        arrowPrefab = Resources.Load<GameObject>("Prefabs/Weapons/ArrowPrefab");
        if (arrowPrefab == null)
        {
            Debug.LogError("No arrowPrefab in Resources.");
        }

        magneticFieldPrefab = Resources.Load<GameObject>("Prefabs/Weapons/MagneticField");
        if (magneticFieldPrefab == null)
        {
            Debug.LogError("No magneticFieldPrefab in Resources.");
        }

        explosiveBottlePrefab = Resources.Load<GameObject>("Prefabs/Weapons/ExplosiveBottle");
        if (explosiveBottlePrefab == null)
        {
            Debug.LogError("No explosiveBottlePrefab in Resources.");
        }

        explosionPrefab = Resources.Load<GameObject>("Prefabs/Weapons/ExplosionPrefab");
        if (explosionPrefab == null)
        {
            Debug.LogError("No explosionPrefab in Resources.");
        }
    }
    private void LoadEnemyPrefabs()
    {
        chasingEnemyPrefab = Resources.Load<GameObject>("Prefabs/Enemies/ChasingEnemyPrefab");
        if (chasingEnemyPrefab == null)
        {
            Debug.LogError("No chasingEnemyPrefab in Resources.");
        }

        rangeEnemyPrefab = Resources.Load<GameObject>("Prefabs/Enemies/RangeEnemyPrefab");
        if (rangeEnemyPrefab == null)
        {
            Debug.LogError("No rangeEnemyPrefab in Resources.");
        }

        rangeEnemyBulletPrefab = Resources.Load<GameObject>("Prefabs/Enemies/RangeEnemyBulletPrefab");
        if (rangeEnemyBulletPrefab == null)
        {
            Debug.LogError("No rangeEnemyBulletPrefab in Resources.");
        }

        slimeBossPrefab = Resources.Load<GameObject>("Prefabs/Enemies/SlimeBossPrefab");
        if (slimeBossPrefab == null)
        {
            Debug.LogError("No slimeBossPrefab in Resources.");
        }
    }

    // getter
    public GameObject GetExpPrefab() { return expPrefab; }
    public GameObject GetArrowPreafb() { return arrowPrefab; }
    public GameObject GetMagneticFieldPrefab() { return magneticFieldPrefab; }
    public GameObject GetExplosiveBottlePrefab() { return explosiveBottlePrefab; }
    public GameObject GetExplosionPrefab() { return explosionPrefab; }
    public GameObject GetChasingEnemyPrefab() { return chasingEnemyPrefab; }
    public GameObject GetRangeEnemyPrefab() { return rangeEnemyPrefab; }
    public GameObject GetRangeEnemyBulletPrefab() { return rangeEnemyBulletPrefab; }
    public GameObject GetSlimeBossPrefab() { return slimeBossPrefab; }
}
