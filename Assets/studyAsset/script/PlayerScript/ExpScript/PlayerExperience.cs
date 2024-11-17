using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerExperience : MonoBehaviour
{
    [SerializeField]
    private int Level = 1;
    public float Exp;
    private float nextLevelTarget;
    // Start is called before the first frame update

    void SetNextLevelTarget(float nextLevelTarget)
    {
        this.nextLevelTarget = nextLevelTarget;
    } 

    public void AddExp(float exp)
    {
        Exp += exp;
    }

    public float GetLevel()
    {
        return Level;
    }
    public float GetnextLevelTarget()
    {
        return this.nextLevelTarget;
    }

    void GetNextLevel()
    {
        if (nextLevelTarget < Exp)
        {
            // 값 초기화
            Level += 1;
            Exp = 0.0f;
            SetNextLevelTarget(nextLevelTarget + 10.0f);

            // 신호 보내기
            GameMgr.Instance.SetIsLevelUp(true);
            GameMgr.Instance.ManageLevel();
        }
    }
    void Start()
    {
        Exp = 0.0f;
        SetNextLevelTarget(100.0f);
    }

    

    // Update is called once per frame
    void Update()
    {
        GetNextLevel();
    }
}
