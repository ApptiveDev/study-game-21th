using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    // 레벨업 마다 코인 20개씩 증정.
    private static CoinManager _instance = null;
    public int ShopPlusHp = 0;
    public float ShopPlusAttackSpeed = 0;
    public float ShopPlusSpeed = 0;
    public static CoinManager Instance
    {
        get
        {
            return _instance;
        }
    }
    public int Coin = 0;
    // Start is called before the first frame update
    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject); 
        }
        else
        {
            Destroy(this.gameObject);
        }

    }
    public void Add20()
    {
        Coin += 20;
    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
