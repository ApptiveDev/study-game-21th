using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeEnemy : ChasingEnemy
{
    private GameObject player;
    private GameObject bullet;
    private IEnemyState currentState;

    private bool isShooting = false;
    const float ATTACK_RANGE = 4f;
    const float CHECK_DELAY = 0.5f;

    public interface IEnemyState
    {
        public void EnterState(RangeEnemy enemy); // 상태 진입할 때 수행하는 행동
        public void ExecuteAction(); // 각 상태일 때 수행하는 행동
        public void ExitState(); // 상태 빠져나갈 때 수행하는 행동
    }

    public class MoveState : IEnemyState
    {
        private RangeEnemy enemy;

        public void EnterState(RangeEnemy enemy)
        {
            this.enemy = enemy;
        }

        public void ExecuteAction()
        {
            enemy.ChasePlayer();
        }

        public void ExitState() { }
    }

    public class AttackState : IEnemyState
    {
        private RangeEnemy enemy;

        public void EnterState(RangeEnemy enemy)
        {
            this.enemy = enemy;
        }

        public void ExecuteAction()
        {
            if (!enemy.isShooting)
            {
                enemy.StartCoroutine(enemy.Shot());
            }
        }

        public void ExitState() 
        {
            enemy.StopCoroutine(enemy.Shot());
        }
    }

    void Start()
    {
        Hp = 10f;
        MoveSpeed = 1f;
        Damage = 2f;
        bullet = GameManager.Instance.GetRangeEnemyBulletPrefab();
        player = GameManager.Instance.GetPlayer();

        currentState = new MoveState(); // 초기 상태는 Move
        currentState.EnterState(this);

        StartCoroutine(CheckAndChangeState());
    }

    protected override void Update()
    {
        if (Hp <= 0)
        {
            Die();
        }
        currentState.ExecuteAction();
    }

    protected IEnumerator CheckAndChangeState()
    {
        while (true)
        {
            IEnemyState newState = (Vector3.Distance(player.transform.position, transform.position) <= ATTACK_RANGE)
                ? new AttackState() : new MoveState();

            if (newState.GetType() != currentState.GetType())
            {
                currentState.ExitState();
                currentState = newState;
                currentState.EnterState(this);
            }
            yield return new WaitForSeconds(CHECK_DELAY);
        }
    }

    protected IEnumerator Shot()
    {
        isShooting = true;
        Instantiate(bullet, this.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(1f);
        isShooting = false;
    }
}