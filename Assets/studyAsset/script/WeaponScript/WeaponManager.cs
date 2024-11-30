using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    static WeaponManager _instance;


    public enum Weapon
    {
        BOMB,
        CIRCLE
    }
    public static WeaponManager Instance
    {
         get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<WeaponManager>();
                    if (_instance == null)
                    {
                        GameObject obj = new GameObject("WeaponManager");
                        _instance = obj.AddComponent<WeaponManager>();
                    }
                }
                return _instance;
            }
    }
    // Start is called before the first frame update
    [SerializeField]
    private Dictionary<Weapon, bool> WeaponList = new Dictionary<Weapon, bool>();

    public void AddWeapon(Weapon weapon)
    {
        WeaponList.Add(weapon, true);
    }

    public bool IsWeaponTrue(Weapon weapon)
    {
        if (WeaponList.ContainsKey(weapon))
        {
            return WeaponList[weapon];
        }
        return false;
    }

    /* ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ */
    GameObject player;
    /* ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ */
    // Basic Weapon 관련 변수
    [SerializeField]
    private GameObject BasicWeapon;

    private float AttackRange = 3.0f; // 사정거리
    private float AttackSpeed = 0.5f; // 몇초마다 발사 할 건지
    private float AttackFlySpeed = 1.0f;
    public enum Direction
    {
        UP,
        DOWN,
        LEFT,
        RIGHT
    }
    [SerializeField]
    private Direction currentDir = Direction.LEFT;
    private float timer;

    private float waitingtime;
    
    // Basic Weapon 관련 함수
    void AttackDirection()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            currentDir = Direction.UP;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            currentDir = Direction.DOWN;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            currentDir = Direction.LEFT;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            currentDir = Direction.RIGHT;
        }
    }
    IEnumerator BasicAttack()
    {
        while (true)
        {
            yield return new WaitForSeconds(AttackSpeed);
            Vector3 MyPosition = player.transform.position;
            //GameObject weaponCreate = Instantiate(BasicWeapon, new Vector3(MyPosition.x, MyPosition.y, MyPosition.z), Quaternion.identity);
            GameObject weaponCreate = WeaponObjPool.Instance.GetObject();
            weaponCreate.transform.position = MyPosition;
            weaponCreate.GetComponent<WeaponMovement>().SetDefault(AttackFlySpeed, AttackRange, currentDir.ToString());
            weaponCreate.GetComponent<WeaponMovement>().StartAction();
        }  
    }
	public void AddAttackSpeed(float AttackSpeed)
	{
		// Add but real action is subtract
		this.AttackSpeed -= AttackSpeed;
	}
	public void AddAttackRange(float AttackRange)
	{
		this.AttackRange += AttackRange;
	}

    /* ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ */
    // Circle Weapon 관련 변수
    public GameObject[] CircleWeapon;

    [SerializeField]
    private GameObject CircleEntity;
    private int CircleCount = 3; // 기본 원운동 3개.
    private float CircleRad = 1f; // 회전 반지름
    private float CircleSpeed = 50f; // 회전 속도

    private float CircleCurrentDegree = 0f; // 현재 회전 각도

    // Circle Weapon 관련 함수
    public void CreateCircle(GameObject Player)
    {
        CircleWeapon = new GameObject[CircleCount];
        Vector3 newPosition = Player.transform.position;
        for (int i = 0; i < CircleCount; i++)
        {
            CircleWeapon[i] = Instantiate(CircleEntity, newPosition, Quaternion.identity);
        }
        StartCoroutine(CircleUpdate(Player));
    }
    public IEnumerator CircleUpdate(GameObject Player)
    {
        while (true)
        {
            CircleCurrentDegree += Time.deltaTime * CircleSpeed;
            if (CircleCurrentDegree >= 360f)
            {
                CircleCurrentDegree -= 360f;
            }

            for (int i = 0; i < CircleCount; i++)
            {
                if (CircleWeapon[i] == null) continue;

                float angle = CircleCurrentDegree + i * (360f / CircleCount);
                float rad = angle * Mathf.Deg2Rad;

                var x = CircleRad * Mathf.Sin(rad);
                var y = CircleRad * Mathf.Cos(rad);
                Vector3 newPosition = Player.transform.position + new Vector3(x, y, 0f);
                CircleWeapon[i].transform.position = newPosition;

                CircleWeapon[i].transform.rotation = Quaternion.Euler(0, 0, -angle);
            }
            yield return null;
        }
    }
    // Circle 끝
    /* ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ */

    /* ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ */
    // Bomb Weapon 관련 변수
    public GameObject Bomb;
    private bool isBombActive = false;

    private float BombSpeed = 2.0f; // Bomb 폭발 주기

    // Bomb Weapon 관련 함수
    [ContextMenu("BombCall")]
    public void BombCall()
    {
        if (!isBombActive)
        {
            Debug.Log("BombCall method called");
            StartCoroutine(BombUpdate());
            isBombActive = true;
        }
    }


    public IEnumerator BombUpdate()
    {
        while (true)
        {
            Debug.Log("Bomb Active");
            yield return new WaitForSeconds(BombSpeed);
            try
            {
                Vector3 MyPosition = transform.position;
                GameObject weaponCreate = WeaponObjPool.Instance.GetBombObject();
                weaponCreate.transform.position = MyPosition;
                weaponCreate.GetComponent<BombWeaponMovement>().StartAction();
            }
            catch (UnassignedReferenceException)
            {
            }
        }
    }
    // Bomb 끝
    /* ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ */

    void Start()
    {
        // 모든 무기는 사용자 중심으로 나가니까 하나 받아오기
        AttackFlySpeed += CoinManager.Instance.ShopPlusAttackSpeed;
        player = GameMgr.Instance.GetPlayer();
        if (player == null)
        {
            Debug.LogError("플레이어 못잡음");
            return;
        }
        // Circle create
        //CreateCircle(player);
        StartCoroutine(BasicAttack());
    }

    // Update is called once per frame
    void Update()
    {
        if(player == null)
        {
            return;
        }
        AttackDirection();
    }
}
