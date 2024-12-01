using UnityEngine;

public class Pet : MonoBehaviour
{
    public float speed;
    public float range;
    public GameObject petAttackPrefab;
    public float attackCooldown;
    Transform player;

    private Scanner scanner; // 스캐너 참조

    private float lastAttackTime;


    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        scanner = GetComponent<Scanner>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!GameManager.instance.isLive)
            return;
        
        float distance = Vector2.Distance(player.position, transform.position);
        
        if(distance > range){
            Vector2 direction = (player.position - transform.position).normalized;
            transform.Translate( direction * speed * Time.deltaTime);    
        }

        if (Time.time >= lastAttackTime + attackCooldown)
        {
            AttackEnemy();
            lastAttackTime = Time.time; // 공격 시간 갱신
        }
    }

    void AttackEnemy()
    {
        if (!GameManager.instance.isLive) // 게임이 진행 중이 아니면 발사체 생성 중단
            return;

        Transform nearestEnemy = scanner.nearestTarget;
        if (nearestEnemy == null){
            return;
        }
        if (petAttackPrefab != null){
            GameObject petAttack = Instantiate(petAttackPrefab, transform.position, Quaternion.identity);
            PetProjectile attack = petAttack.GetComponent<PetProjectile>();
            if (attack != null)
            {
                Vector2 directionToEnemy = (nearestEnemy.position - transform.position).normalized;
                 Debug.Log($"Attacking Enemy: {nearestEnemy.name} Direction: {directionToEnemy}");
                attack.Init(directionToEnemy); // 발사체 방향 설정
            }
        }
    }
}
