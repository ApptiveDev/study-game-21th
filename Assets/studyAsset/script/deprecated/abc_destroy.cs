//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//
//public class abc_destroy : MonoBehaviour
//{
//    Transform transform;
//    public float speed;
//    public int left_or_right;
//    public float up_speed;
//    // Start is called before the first frame update
//    void Start()
//    {
//        Destroy(gameObject, 2.0f);
//        transform = GetComponent<Transform>();
//        speed = 1.0f;
//        up_speed = 0.0f;
//        left_or_right = Random.Range(0, 2);
//    }
//
//    // Update is called once per frame
//    void Update()
//    {
//        transform.position += Vector3.down * speed * Time.deltaTime;
//        speed += Time.deltaTime*9.6f;
//        if(left_or_right == 1)
//        {
//            transform.position += Vector3.left * 3.0f * Time.deltaTime;
//        }
//        else
//        {
//            transform.position += Vector3.right * 3.0f * Time.deltaTime;
//        }
//
//    }
//}
