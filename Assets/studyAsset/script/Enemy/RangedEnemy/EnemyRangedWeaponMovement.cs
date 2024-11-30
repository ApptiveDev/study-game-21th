using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangedWeaponMovement : MonoBehaviour
{
    private GameObject player;
    private Vector3 ThatPosition;
    private float range = 1.0f;
    private float speed = 3.0f;

    [SerializeField]
    private float DAMAGE = 1;

    IEnumerator Delete()
    {
        yield return new WaitForSeconds(range);
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
       if (collision.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerHp>().SubtractHp(DAMAGE);
            Destroy(gameObject);
        } 
    }
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameMgr.Instance.GetPlayer();
        ThatPosition = player.transform.position;
        StartCoroutine(Delete());
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, ThatPosition, speed * Time.deltaTime);
    }
}
