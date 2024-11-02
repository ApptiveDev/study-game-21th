using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reposition : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Area"))
            return;
        Vector3 playerPos = GameManager.instance.player.transform.position;
        Vector3 playerDir = GameManager.instance.player.inputVector;
        Vector3 myPos = transform.position;

        float diffX = Mathf.Abs(playerPos.x - myPos.x);
        float diffY = Mathf.Abs(playerPos.y - myPos.y);
        float dirX = playerDir.x > 0 ? 1 : -1;
        float dirY = playerDir.y > 0 ? 1 : -1;

        if (diffX > diffY)
        {
            transform.Translate(Vector3.right * dirX * 40);
        } else if(dirX < diffY)
        {
            transform.Translate(Vector3.up * dirY * 40);
        }
    }
}
