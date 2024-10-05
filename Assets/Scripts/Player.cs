using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
   public float moveSpeed = 2;

   private float hAxis;
   private float vAxis;
   
   bool isBorder; // 플레이어 앞의 벽을 감지하기 위한 변수

   Vector3 moveVec;

   void Update() // 매 프레임마다 동작을 한다. 컴퓨터나 동작하는 환경의 성능에 따라서 1초당 몇 프레임인지가 달라짐.
   {
      GetInput();
      Move();
   }
   
   void FixedUpdate() 
   {
      StopToWall();
   }

   void GetInput()
   {
      hAxis = Input.GetAxisRaw("Horizontal"); // GetAxisRaw() = Axis값을 정수로 반환하는 함수, Horizontal = left/ right, Unity의 Input Manager에서 수정가능
      vAxis = Input.GetAxisRaw("Vertical"); // Vertical = up/ down
   }

   void Move()
   {
      moveVec = new Vector3(hAxis, vAxis, 0).normalized;

      if (!isBorder)
         transform.position += moveVec * moveSpeed * Time.deltaTime;
   }

   void StopToWall()
    {
        Debug.DrawRay(transform.position, transform.forward * 2, Color.green); // DrawRay = Scene내에서 Ray를 보여주는 함수
                                                                                // DrawRay(시작위치, 쏘는방향 * 길이, 색깔)
                                                                                // Ray를 통해 플레이어 앞의 물체를 빠르게 감지할 수 있다
        isBorder = Physics.Raycast(transform.position, transform.forward, 5, LayerMask.GetMask("Wall")); // Ray를 쏘아 닿는 오브젝트를 감지하는 함수 
                                                                                                        // (시작위치, 방향, 길이, 충돌한 물체의 LayerMask가 'Wall'인가)                                                                     
    }
}
