using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private Transform playerTransform;
    public float speed = 2.5f;
    public float hp = 100;
    public float TackleDAMAGE = 1.0f;
    [SerializeField]
    private GameObject experienceOrb;
    void Start()
    {
        GameObject player = GameMgr.Instance.GetPlayer();
        // 원거리 적일 때 예외
        if(this.gameObject.GetComponent<RangedEnemyMovement>() != null)
        {
            return;
        }
        if(player != null)
        {
            playerTransform = player.transform;
        }
        else
        {
            Debug.LogError("Not Player Tag");
        }
    }

    public void DeleteEnemy()
    {
        if(hp <= 0)
        {
            //  경험치 구슬 생성.
            Vector3 MyPosition = transform.position;
            // 만약 적에 따라 경험치를 달리해야 한다면, 게임오브젝트 받아오는 것도 나쁘지 않을 듯.
            GameObject ExperienceOrb =  Instantiate(experienceOrb, new Vector3(MyPosition.x, MyPosition.y, MyPosition.z), Quaternion.identity);
            EnemyObjectPool.Instance.ReturnObject(this.gameObject);
            //Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerHp>().SubtractHp(TackleDAMAGE);
            //Destroy(gameObject);
            EnemyObjectPool.Instance.ReturnObject(this.gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
        DeleteEnemy();
        if(playerTransform != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, speed * Time.deltaTime);
        }
    }
}
