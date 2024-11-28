using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyState
{
    public void EnterState(EnemyMovement enemy);
    public void ExecuteAction(EnemyMovement enemy);
    public void ExitState(EnemyMovement enemy);

}
