using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RangedEnemyMovement : MonoBehaviour
{
    private GameObject player;
    private float ATTACK_RANGE = 3.0f;
    private float CHECK_DELAY = 1.0f;
    private float speed = 2.5f;

    private float timer = 0.0f;
    private float AttackSpeed = 1.0f;

    public GameObject RangedWeapon;
    public enum RangedEnemyState
    {
        MOVE,
        ATTACK,
    }

    public RangedEnemyState state;
    // Start is called before the first frame update
    public IEnumerator CheckState()
    {
        while (true)
        {
            if (Vector3.Distance(player.transform.position, transform.position) <= ATTACK_RANGE)
            {
                state = RangedEnemyState.ATTACK;
            }
            else
            {
                state = RangedEnemyState.MOVE;
            }
            yield return new WaitForSeconds(CHECK_DELAY);
        }
    }

    public void Movement()
    {
        if(state == RangedEnemyState.ATTACK)
        {
            if(timer >= AttackSpeed)
            {
                timer = 0.0f;
                Instantiate(RangedWeapon, transform.position, Quaternion.identity);        
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        }
        timer += Time.deltaTime;
    }
    void Start()
    { 
        player = GameMgr.Instance.GetPlayer();
        StartCoroutine(CheckState());
    }

    // Update is called once per frame
    void Update()
    {
        Movement();    
    }
}
