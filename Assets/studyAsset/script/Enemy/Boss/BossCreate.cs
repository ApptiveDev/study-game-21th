using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCreate : MonoBehaviour
{
    [SerializeField]
    private GameObject FirstBoss;

    public void CreateBoss()
    {
        Instantiate(FirstBoss, transform.position, Quaternion.identity);
    }

    private GameObject player;
    private float playerLevel;
    public IEnumerator CheckLevelFive()
    {
        while (true)
        {
            playerLevel = player.GetComponent<PlayerExperience>().GetLevel();
            if(playerLevel == 5)
            {
                CreateBoss();
                break;
            }
            yield return new WaitForSeconds(1);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameMgr.Instance.GetPlayer(); 
        playerLevel = player.GetComponent<PlayerExperience>().GetLevel();
        StartCoroutine(CheckLevelFive());
    }
    // Update is called once per frame
    void Update()
    {
    }
}
