using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.UIElements;

public class BlackHoleSystem : MonoBehaviour
{
    [SerializeField] GameObject BlackHole2;
    [SerializeField] GameObject bigBlackHole2;
    GameObject player;
    GameObject cardSystem;
    Vector3 direction;
    float curTimeBlackHole;
    float Speed = 10f;
    void Start()
    {
        player = GameObject.Find("Player");
        cardSystem = GameObject.Find("CardSystem");
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
        CardSelect cardSelect = cardSystem.GetComponent<CardSelect>();
        curTimeBlackHole += Time.deltaTime;

        if (curTimeBlackHole <= 0.5f)
        {
            transform.position += direction * Speed * Time.deltaTime;
        }
        else if (cardSelect.isbigBlackHole == false)
        {
            GameObject blackhole2Instance = Instantiate(BlackHole2);
            blackhole2Instance.transform.position = BlackHolePosition();
            Destroy(gameObject);
        }
        else if (cardSelect.isbigBlackHole == true)
        {
            GameObject bigblackhole2Instance = Instantiate(bigBlackHole2);
            bigblackhole2Instance.transform.position = BlackHolePosition();
            Destroy(gameObject);
        }
    }

    Vector3 BlackHolePosition()
    {
        return new Vector3(transform.position.x, transform.position.y, 0);
    }
}
