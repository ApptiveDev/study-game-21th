using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBoss : Boss
{
    // Start is called before the first frame update
    void Start()
    {
        Hp = 50f;
        Damage = 10f;
        MoveSpeed = 1f;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
}
