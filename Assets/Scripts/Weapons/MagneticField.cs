using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagneticField : Weapon
{
    private CircleCollider2D circleCollider2D;
    private Dictionary<GameObject, Coroutine> enemyDict = new Dictionary<GameObject, Coroutine>();

    private float shockPeriod = 0.75f;
    private float originalRadius;
    public override void Upgrade()
    { 
        Damage = Damage + 1f;
        transform.localScale = new Vector3(transform.localScale.x * 1.2f, transform.localScale.y * 1.2f, 1); // 트랜스폼 크기 늘리면 콜라이더도 자동으로 커지더라
        WeaponInfo = "범위안의 적에게 주기적으로 " + Damage.ToString() + "만큼의 데미지를 줌";
    }
    
    // Start is called before the first frame update
    void Start()
    {
        Damage = 1f;
        WeaponName = "전기장";
        WeaponInfo = "범위안의 적에게 주기적으로 " + Damage.ToString() + "만큼의 데미지를 줌";
        circleCollider2D = GetComponent<CircleCollider2D>();
        originalRadius = circleCollider2D.radius;
    }

    IEnumerator Shock(GameObject enemy)
    {
        while (enemy != null)
        {
            enemy.GetComponent<Enemy>().BeAttacked(Damage);
            yield return new WaitForSeconds(shockPeriod);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") && !enemyDict.ContainsKey(collision.gameObject))
        {
            // 전기장 범위 안의 여러 적들에 대한 피격 처리를 위해, 각 적에 대해 개별 코루틴 시작
            Coroutine shockCoroutine = StartCoroutine(Shock(collision.gameObject));
            enemyDict.Add(collision.gameObject, shockCoroutine);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") && enemyDict.ContainsKey(collision.gameObject))
        {
            // 적이 범위를 벗어나면 해당 코루틴 종료
            StopCoroutine(enemyDict[collision.gameObject]);
            enemyDict.Remove(collision.gameObject);
        }
    }
}
