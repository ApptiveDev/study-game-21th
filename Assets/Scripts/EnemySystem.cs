using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemySystem : MonoBehaviour
{
    GameObject Player;
    void Start()
    {
        Player = GameObject.Find("Player");
    }

    void Update()
    {
        transform.position += Vector3.right * (Player.transform.position.x - transform.position.x) * Time.deltaTime;
        transform.position += Vector3.up * (Player.transform.position.y - transform.position.y) * Time.deltaTime;
    }
}
