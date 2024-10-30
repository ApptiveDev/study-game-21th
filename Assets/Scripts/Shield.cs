using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    private float angularVelocity = 100f;
    private float radius = 3f;
    private float angle = 0f;
    private GameObject player;

    private void Start()
    {
        player = GameObject.Find("Player");
    }

    private void MoveOnCircle()
    {
        angle = (angle + angularVelocity * Time.deltaTime) % 360;
        Vector3 relativePosition = new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad) * radius, Mathf.Cos(angle * Mathf.Deg2Rad) * radius, 0);
        transform.position = player.transform.position + relativePosition;
    }

    void Update()
    {
        MoveOnCircle();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
        }
    }
}
