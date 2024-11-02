using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.UIElements;

public class BlackHoleSystem : MonoBehaviour
{
    [SerializeField] GameObject BlackHole2;
    GameObject player;
    Vector3 direction;
    float curTimeBlackHole;
    float Speed = 10f;
    void Start()
    {
        player = GameObject.Find("Player");
    }

 
    void Update()
    {
        BlackHoleMove();
    }

    public void BlackHoleDirection(Quaternion playerRotation)
    {
        direction = playerRotation * Vector3.up;
    }

    void BlackHoleMove()
    {
        curTimeBlackHole += Time.deltaTime;

        if (curTimeBlackHole <= 1)
        {
            transform.position += direction * Speed * Time.deltaTime;
        }
        else
        {
            GameObject blackhole2Instance = Instantiate(BlackHole2);
            blackhole2Instance.transform.position = BlackHolePosition();
            Destroy(gameObject);
        }
    }

    Vector3 BlackHolePosition()
    {
        return new Vector3(transform.position.x, transform.position.y, 0);
    }
}
