using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBarController : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject player;
    private float MaxHp;
    protected float CurHp;

    public Slider HpSlider;
    void Start()
    {
        player = GameMgr.Instance.GetPlayer();
        MaxHp = player.GetComponent<PlayerHp>().GetMaxHp();
        CurHp = player.GetComponent<PlayerHp>().GetNowHp();
        HpSlider.value = CurHp / MaxHp;
    }

    // Update is called once per frame
    void Update()
    {
        if (HpSlider != null)
        {
            if (CurHp != player.GetComponent<PlayerHp>().GetNowHp())
            {
                MaxHp = player.GetComponent<PlayerHp>().GetMaxHp();
                CurHp = player.GetComponent<PlayerHp>().GetNowHp();
                HpSlider.value = CurHp / MaxHp;
            }
        }
    }
}
