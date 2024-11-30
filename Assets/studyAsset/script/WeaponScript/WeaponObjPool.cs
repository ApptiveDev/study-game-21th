using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponObjPool : MonoBehaviour, IObjectPool
{
    public static WeaponObjPool Instance;
    int BasicWeaponpoolCount = 50;
    int BombpoolCount = 5;

    [SerializeField]
    private GameObject BasicWeapon;
    [SerializeField]
    private GameObject Bomb;

    Queue<GameObject> BasicWeaponpoolQueue = new Queue<GameObject>();
    Queue<GameObject> BombpoolQueue = new Queue<GameObject>();
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        // pool »ý¼º
        for (int i = 0; i < BasicWeaponpoolCount; i++)
        {
            GameObject poolobj = Instantiate(BasicWeapon, transform);
            poolobj.SetActive(false);
            BasicWeaponpoolQueue.Enqueue(poolobj);
        }
        for (int i = 0; i < BombpoolCount; i++)
        {
            GameObject poolobj = Instantiate(Bomb, transform);
            poolobj.SetActive(false);
            BombpoolQueue.Enqueue(poolobj);
        }
    }

    public GameObject GetObject()
    {
        GameObject obj = BasicWeaponpoolQueue.Dequeue();
        obj.SetActive(true);
        obj.transform.parent = null;
        return obj;
    }
    public GameObject GetBombObject()
    {
        GameObject obj = BombpoolQueue.Dequeue();
        obj.SetActive(true);
        obj.transform.parent = null;
        return obj;
    }

    public void ReturnObject(GameObject obj)
    {
        obj.transform.SetParent(transform);
        obj.transform.position = transform.position;
        obj.SetActive(false);
        BasicWeaponpoolQueue.Enqueue(obj);
    }
    public void BombReturnObject(GameObject obj)
    {
        obj.transform.SetParent(transform);
        obj.transform.position = transform.position;
        obj.SetActive(false);
        BombpoolQueue.Enqueue(obj);
    }
}
