using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
   public Vector2 inputVec;
   public float speed;
   public Scanner scanner; // 플레이어 스크립트에서 Scanner클래스 타입 변수 선언 및 초기화
   public LayerMask WallLayer; // 감지할 레이어

   bool isWall; // 플레이어 앞의 벽을 감지하기 위한 변수
   bool isIgnoreWall; // 벽을 통과하는 물체에 닿아있는지를 확인
   bool isObstacle; // 장애물이 있는지를 확인

   Vector3 moveVec;
   Rigidbody2D rigid;
   SpriteRenderer spriter; // SpriteRenderer 값을 받아올 변수

   void Awake()
   {
      rigid = GetComponent<Rigidbody2D>(); // GetComponent<컴포넌트 이름> = 오브젝트에서 컴포넌트를 가져오는 함수
      spriter = GetComponent<SpriteRenderer>();
      scanner = GetComponent<Scanner>();
   }

   void Start()
   {
      speed = DataManager.instance.nowPlayer.speed;
   }

   void Update() // 매 프레임마다 동작을 한다. 컴퓨터나 동작하는 환경의 성능에 따라서 1초당 몇 프레임인지가 달라짐.
   {
      if (!GameManager.instance.isLive) {
         return; // isLive가 false이면(시간이 멈추면) 동작하지 못하도록 조건 추가
      }

      GetInput();
      Move();
   }
   
   void FixedUpdate() // 물리연산 프레임마다 호출되는 생명주기 함수
   {
      if (!GameManager.instance.isLive) {
         return; // isLive가 false이면(시간이 멈추면) 동작하지 못하도록 조건 추가
      }

      StopToWall();
      IgnoreWall();
      // StopToObstacle();
   }

   void GetInput()
   {
      inputVec.x = Input.GetAxisRaw("Horizontal"); 
      // input = 유니티에서 받는 모든 입력을 관리하는 클래스
      // Unity의 Inpuy Manager에서 Horizontal로 저장되있는 키가 눌렸는지를 확인 
      // GetAxis가 아닌 GetAxisRaw로 더욱 명확한 컨트롤 구현 가능
      inputVec.y = Input.GetAxisRaw("Vertical");
   }

   void Move()
   {
      moveVec = new Vector3(inputVec.x, inputVec.y, 0).normalized;
      if (!isWall && !isObstacle)
         transform.position += moveVec * speed * Time.deltaTime;
   }

   void StopToWall()
   {
      
      // Debug.DrawRay(transform.position, moveVec* 0.2f, Color.green); // DrawRay = Scene내에서 Ray를 보여주는 함수
                                                                        // DrawRay(시작위치, 쏘는방향 * 길이, 색깔)
                                                                        // Ray를 통해 플레이어 앞의 물체를 빠르게 감지할 수 있다
                                                                        // 2D에서 forward 대신 up 사용
      
      isWall = Physics2D.Raycast(transform.position, moveVec, 0.2f, LayerMask.GetMask("Wall")); // Ray를 쏘아 닿는 오브젝트를 감지하는 함수 
                                                                                                        // (시작위치, 방향, 길이, 충돌한 물체의 LayerMask가 'Wall'인가)    
                                                                                                        // 2D에서는 Physics.Raycast() 대신 Physics2D.Raycast() 사용                                                                

      // isWall과 isIgnoreWall이 동시에 true가 되지 않도록 조정
      if (isIgnoreWall) {
        isWall = false; // IgnoreWall이 감지되면 Wall 판정을 무시하도록 설정
      }
   }

   void IgnoreWall() 
   {
      // Debug.DrawRay(transform.position, moveVec* 0.2f, Color.green);
      isIgnoreWall = Physics2D.Raycast(transform.position, moveVec, 0.2f, LayerMask.GetMask("IgnoreWall"));
   }

   void StopToObstacle()
   {
      Debug.DrawRay(transform.position, moveVec* 0.2f, Color.green);
      isObstacle = Physics2D.Raycast(transform.position, moveVec, 0.2f, LayerMask.GetMask("Obstacle"));   
   }

   void OnTriggerStay2D(Collider2D collision) 
   {
      if (!GameManager.instance.isLive || !collision.CompareTag("Enemy")) {
         return;
      }

      float distance = Vector3.Distance(transform.position, collision.transform.position);
      if (collision.CompareTag("Enemy") && (distance < 1f)) {
         GameManager.instance.health -= Time.deltaTime * 10;
         // Time.dletaTime을 활용하여 적절한 피격 데미지 계산, 그냥 -= 10을 하게 되면 프레임마다 체력이 10씩 닳기 때문에 빠르게 죽어버림
      }
      
      if (GameManager.instance.health < 0) { 
         // Player는 자식오브젝트를 많이 가지고있는데 플레이어 사망시 이 오브젝트들을 비활성화 시켜줘야한다
         for (int index=2; index < transform.childCount; index++) { // childCout = 자식오브젝트의 개수
               transform.GetChild(index).gameObject.SetActive(false); // GetChild = 주어진 인덱스의 자식 오브젝트를 반환하는 함수 
               // GetChild의 반환값으로 transform이 나오므로 .gameObject로 다시 접근하여 SetActive를 false로 설정한다
         }

         GameManager.instance.GameOver();
      }
   }   
}
