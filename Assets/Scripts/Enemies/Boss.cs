using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : ChasingEnemy
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    public override void Die()
    {
        base.Die();
        GameManager.Instance.OnBossDefeated();
    }
}
