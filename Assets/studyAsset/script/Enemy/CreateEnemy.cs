using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateEnemy : MonoBehaviour
{
    // Start is called before the first frame update
    public float timer;
    public float waitingTime;
    // 인덱스가 늘어날 수록 더 강한 적이라 가정.

    private int EnemyLength;
    [SerializeField]
    private GameObject[] enemy;
    [SerializeField]
    private float[] Weights;
    public enum EnemyType
    {
        MELEE,
        RANGED,
    }
    
    private EnemyType RandomEnemy()
    {
        var wrPicker = new Rito.WeightedRandomPicker<EnemyType>();
        var values = Enum.GetValues(typeof(EnemyType));
        for (int i = 0; i < values.Length; i++)
        {
            wrPicker.Add((EnemyType) values.GetValue(i), Weights[i]);
        }

        return wrPicker.GetRandomPick();
    }
    private GameObject GetRandomEnemy()
    {
        float totalWeight = 0;
        foreach (float weight in Weights)
        {
            totalWeight += weight;
        }

        float RandomValue = UnityEngine.Random.Range(0, totalWeight);
        float CumulativeWeight = 0;

        for(int i=0; i < enemy.Length; i++)
        {
            CumulativeWeight += Weights[i];
            if(RandomValue <= CumulativeWeight)
            {
                return enemy[i];
            }
        }
        return enemy[enemy.Length - 1];
    }
    public void createEnemy()
    {
        //if(timer > waitingTime)
        //    try
        //    {
        //        Vector3 MyPosition = transform.position;
        //        GameObject enemySelected = GetRandomEnemy();
        //        Instantiate(enemySelected, new Vector3(MyPosition.x, MyPosition.y, MyPosition.z), Quaternion.identity);
        //        timer = 0.0f;
        //    }
        //    catch (UnassignedReferenceException)
        //    {
        //    }
        //else
        //{
        //    timer += Time.deltaTime;
        //}
        // obj pool
        if(timer > waitingTime)
            try
            {
                EnemyType enemyType = RandomEnemy();
                if(enemyType == EnemyType.MELEE)
                {
                    GameObject temp = EnemyObjectPool.Instance.GetObject();
                    temp.transform.position = transform.position;
                }
                else if(enemyType == EnemyType.RANGED)
                {
                    GameObject temp = EnemyObjectPool.Instance.GetRangedObject();
                    temp.transform.position = transform.position;
                }
                timer = 0;
            }
            catch (UnassignedReferenceException)
            {
            }
        else
        {
            timer += Time.deltaTime;
        }

    } 
    void Start()
    {
        EnemyLength = enemy.Length;
        waitingTime = 2.0f;
    }

    // Update is called once per frame
    void Update()
    {
        createEnemy(); 
    }
}
