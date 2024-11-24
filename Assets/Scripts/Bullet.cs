using UnityEngine;

public class Bullet : MonoBehaviour
{ // 근접무기 프리펩의 Order in Layer를 몬스터보다 높이기
    public float damage;
    public int per; // 관통
    public int id;

    Rigidbody2D rigid; // 총탄은 속도가 필요하므로 RigidBody2D 추가

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    public void Init(float damage, int per, int id, Vector3 dir)
    {
        this.damage = damage;
        this.per = per;
        this.id = id;

        if (per > -1) { // 관통이 -1 (플레이어의 근접무기)이외의 것에 대해서는 속도 적용
            rigid.velocity = dir * 15f; // velocity = 속도, 속력을 곱해주어 총알이 날아가는 속도 증가시키기
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy") || per == -1) // 관통 로직 이전에 if 문으로 조건 추가
            return;
        
        per--;

        if (per == -1) { // 관통 값이 하나씩 줄어들면서 -1이 되면 비활성화
            rigid.velocity = Vector2.zero; // 나중에 재사용할것이므로 비활성화 이전에 미리 물리속도를 초기화한다
            gameObject.SetActive(false); // 총알도 오브젝트 풀링으로 관리되고 있기 때문에 Destroy를 사용하면 안된다

        }    
    }

}