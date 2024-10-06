using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    private GameObject player;
    private Vector3 playerPosition;
    private Vector3 direction;
    private float moveSpeed = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        // FindWithTag �÷��̾� ������Ʈ ��������
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        // Player ��ġ ��������
        playerPosition = player.transform.position;
        // Player ������ �ٰ����� ���� ���⺤�� ���ϱ�
        direction = (playerPosition - transform.position).normalized;
        // ���� ���⺤�͸� ���� �÷��̾� �߰��ϱ�
        transform.position += direction * moveSpeed * Time.deltaTime;
    }
}
