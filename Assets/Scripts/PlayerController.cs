using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f; // 플레이어 이동 속도
    public GameObject weaponPrefab; // 무기 프리팹
    public GameObject missilePrefab;
    public float fireRate1 = 1f; // 무기 발사 간격
    public float fireRate2 = 1f;
    public int experience = 0;

    void Start()
    {
        StartCoroutine(FireWeapons());
        StartCoroutine(FireMissiles());
    }

    void Update()
    {
        // 방향키 입력을 통한 플레이어 이동
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        transform.Translate(movement * moveSpeed * Time.deltaTime);
    }

    public IEnumerator FireWeapons()
    {
        while (true) // 무한 반복
        {
            Instantiate(weaponPrefab, transform.position, Quaternion.identity);
            
            yield return new WaitForSeconds(fireRate1); 
        }
    }
    public IEnumerator FireMissiles()
    {
        while (true) // 무한 반복
        {
            Instantiate(missilePrefab, transform.position, Quaternion.identity);

            yield return new WaitForSeconds(fireRate2);
        }
    }
  

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("ExpOrb"))
        {
            experience++;
        }
    }
    
}



