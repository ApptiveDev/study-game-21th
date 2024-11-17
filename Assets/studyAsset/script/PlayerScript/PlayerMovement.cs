using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    new
        // Start is called before the first frame update
        Transform transform;
    private float speed = 5f;
    public GameObject abc;
    public GameObject Weapon;


    // Weapon 관련 변수
    public float AttackRange = 5.0f; // 얼마나 갈 건지 시간으로 사정거리를 늘리는 느낌.
    public float AttackSpeed = 0.5f; // 몇초마다 발사 할 건지

    // 움직임 관련 변수
    private Animator animator_;
    int CheckMove = 0;
    public enum Direction
    {
        UP,
        DOWN,
        LEFT,
        RIGHT
    }
    public Direction currentDir = Direction.LEFT;
    public float timer;

    public float waitingtime;

    // Bomb Weapon 관련 변수
    public float BombSpeed = 2.0f;
    public float BombTimer = 0f;


    int getLeft()
    {
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
            transform.localScale = new Vector3(-3, 3, 3);
            SetisMoveTrue();
            return 1;
        }
        return 0;
    }

    int getRight()
    {
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
            transform.localScale = new Vector3(3, 3, 3);
            SetisMoveTrue();
            return 1;
        }
        return 0;
    }

    int getDown()
    {
        if (Input.GetKey(KeyCode.S))
        {
            transform.position += Vector3.down * speed * Time.deltaTime;
            SetisMoveTrue();
            return 1;
        }
        return 0;
    }

    int getUp()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += Vector3.up * speed * Time.deltaTime;
            SetisMoveTrue();
            return 1;
        }
        return 0;
    }

    void SetisMoveTrue()
    {
        animator_.SetBool("isMove", true);
    }
    
    void SetisMoveFalse()
    {
        animator_.SetBool("isMove", false);
    }
    
//    void Attack()
//    {
//        if(timer > AttackSpeed)
//            try
//            {
//                Vector3 MyPosition = transform.position;
//                GameObject weaponCreate =  Instantiate(Weapon, new Vector3(MyPosition.x, MyPosition.y, MyPosition.z), Quaternion.identity);
//                weaponCreate.GetComponent<WeaponMovement>().SetDefault(AttackSpeed, AttackRange, currentDir.ToString());
//                timer = 0.0f;
//            }
//            catch (UnassignedReferenceException)
//            {
//            }
//        else
//        {
//            timer += Time.deltaTime;
//        }
//    }
//
//    void AttackDirection()
//    {
//        if (Input.GetKey(KeyCode.UpArrow))
//        {
//            currentDir = Direction.UP;
//        }
//
//        else if (Input.GetKey(KeyCode.DownArrow))
//        {
//            currentDir = Direction.DOWN;
//        }
//        else if (Input.GetKey(KeyCode.LeftArrow))
//        {
//            currentDir = Direction.LEFT;
//        }
//        else if (Input.GetKey(KeyCode.RightArrow))
//        {
//            currentDir = Direction.RIGHT;
//        }
//
//    }

    void Start()
    {
        transform = GetComponent<Transform>();
        transform.position = new Vector3(0, 0, 0);
        timer = 0.0f;
        waitingtime = 1.0f;
        animator_ = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckMove += getUp();
        CheckMove += getRight();
        CheckMove += getDown();
        CheckMove += getLeft();
        if(CheckMove == 0)
        {
            SetisMoveFalse();
        }
        CheckMove = 0;
        //Attack();
        //AttackDirection();
    }
}
