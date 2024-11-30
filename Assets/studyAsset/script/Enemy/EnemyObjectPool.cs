using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyObjectPool : MonoBehaviour, IObjectPool
{
    public static EnemyObjectPool Instance;
    int MeleepoolCount = 100;
    int RangedpoolCount = 100;

    [SerializeField]
    private GameObject MeleeEnemy;
    [SerializeField]
    private GameObject RangedEnemy;

    Queue<GameObject> MeleepoolQueue = new Queue<GameObject>();
    Queue<GameObject> RangedpoolQueue = new Queue<GameObject>();
    
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
        for (int i = 0; i < MeleepoolCount; i++)
        {
            GameObject poolobj = Instantiate(MeleeEnemy, transform);
            poolobj.SetActive(false);
            MeleepoolQueue.Enqueue(poolobj);
        }
        for (int i = 0; i < RangedpoolCount; i++)
        {
            GameObject poolobj = Instantiate(RangedEnemy, transform);
            poolobj.SetActive(false);
            RangedpoolQueue.Enqueue(poolobj);
        }
    }
    public GameObject GetObject()
    {
        GameObject obj = MeleepoolQueue.Dequeue();
        obj.GetComponent<EnemyMovement>().hp = 100;
        obj.SetActive(true);
        obj.transform.parent = null;
        return obj;
    }
    public GameObject GetRangedObject()
    {
        GameObject obj = RangedpoolQueue.Dequeue();
        obj.SetActive(true);
        obj.transform.parent = null;
        return obj;
    }
    public void ReturnObject(GameObject obj)
    {
        obj.GetComponent<EnemyMovement>().hp = 100;
        obj.transform.SetParent(transform);
        obj.transform.position = transform.position;
        obj.SetActive(false);
        MeleepoolQueue.Enqueue(obj);
    }
    public void RangedReturnObject(GameObject obj)
    {
        obj.transform.SetParent(transform);
        obj.transform.position = transform.position;
        obj.SetActive(false);
        RangedpoolQueue.Enqueue(obj);
    }
}
