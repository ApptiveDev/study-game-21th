using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public float cameraSpeed = 5f;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = player.transform.position - this.transform.position;
        Vector3 moveVector = new Vector3(dir.x * cameraSpeed * Time.unscaledDeltaTime, dir.y * cameraSpeed * Time.unscaledDeltaTime, 0.0f);
        this.transform.Translate(moveVector); 
    }
}
