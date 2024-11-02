using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Player player;
    public static GameManager instance;
    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
    }
}
