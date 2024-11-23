using UnityEngine;

public class HealthUpgradeButton : MonoBehaviour
{
    public int upgradeCost = 100;

    public void UpgradePlayerHealth()
    {

        if (GameManager.Instance.SpendCoins(upgradeCost))
        {
            Player.Instance.IncreaseHealth(2f);
            Debug.Log("체력 업그레이드 완료!");
        }
        else
        {
            Debug.Log("코인이 부족합니다.");
        }
    }
}
