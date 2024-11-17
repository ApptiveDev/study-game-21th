using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceOrbMovement : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            // 여기 수정 바람.
            collision.gameObject.GetComponent<PlayerExperience>().AddExp(10);
            Destroy(gameObject);
        }
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}