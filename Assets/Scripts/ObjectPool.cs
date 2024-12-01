using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool instance;
    [SerializeField] private GameObject poolObj;
    [SerializeField] private int poolCount = 50;

    private Queue<GameObject> poolQueue = new Queue<GameObject>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        for (int i = 0; i < poolCount; i++)
        {
            CreateNewObject();
        }
    }

    private GameObject CreateNewObject()
    {
        GameObject newObj = Instantiate(poolObj, transform);
        newObj.SetActive(false);
        poolQueue.Enqueue(newObj);
        return newObj;
    }

    public GameObject GetObject()
    {
        if (poolQueue.Count > 0)
        {
            GameObject obj = poolQueue.Dequeue();
            obj.SetActive(true);
            obj.transform.SetParent(null);
            return obj;
        }
        else
        {
            return CreateNewObject();
        }
    }

    public void ReturnObject(GameObject obj)
    {
        if (!poolQueue.Contains(obj))
        {
            obj.SetActive(false);
            obj.transform.SetParent(transform);
            obj.transform.position = transform.position;
            poolQueue.Enqueue(obj);
        }
    }
}
