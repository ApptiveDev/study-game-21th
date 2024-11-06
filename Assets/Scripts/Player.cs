using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Player : MonoBehaviour
{
    private static Player _instance;

    public static Player Instance
    {
        get
        {
            return _instance;
        }
    }
    
    protected virtual void Awake()
    {
        if (_instance == null)
        {
	        _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
    }
    
    Transform playerTransform;
    public GameObject player;
    private float speed = 3;
    public float playerHealth = 100f; // 플레이어 체력 변수
    void Start()
    {
        playerTransform = GetComponent<Transform>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.W)) {
            playerTransform.position += Vector3.up * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A)) {
            playerTransform.position += Vector3.left * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S)) {
            playerTransform.position += Vector3.down * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D)) {
            playerTransform.position += Vector3.right * speed * Time.deltaTime;
        }                        
    }

   public void TakeDamage(float enemyDamage) { // 적에게 부딪혔을 때 체력을 깎음
        playerHealth -= enemyDamage;
        if (playerHealth <= 0) {
            Debug.Log("사망");
            gameObject.SetActive(false); // 비활성화
        }
    }
}


