using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    float speed = 6f;
    float BoundaryX = 19.5f;
    float BoundaryY = 19.5f;
    int speedLevel;

    private void Awake()
    {
        speedLevel = GameDataManager.Instance.GetSpeedLevel();
    }

    private void Start()
    {
        speed += speedLevel * 0.5f;
    }

    void Update()
    {
        transform.position += Vector3.right * Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime;
        transform.position += Vector3.up * Input.GetAxisRaw("Vertical") * speed * Time.deltaTime;

        if (Input.GetAxisRaw("Horizontal") == 1)
        {
            if (Input.GetAxisRaw("Vertical") == 1) transform.rotation = Quaternion.Euler(0, 0, -45);
            else if (Input.GetAxisRaw("Vertical") == -1) transform.rotation = Quaternion.Euler(0, 0, -135);
            else transform.rotation = Quaternion.Euler(0, 0, -90);
        }
        else if (Input.GetAxisRaw("Horizontal") == -1)
        {
            if (Input.GetAxisRaw("Vertical") == 1) transform.rotation = Quaternion.Euler(0, 0, 45);
            else if (Input.GetAxisRaw("Vertical") == -1) transform.rotation = Quaternion.Euler(0, 0, 135);
            else transform.rotation = Quaternion.Euler(0, 0, 90);
        }
        else if (Input.GetAxisRaw("Vertical") == 1) transform.rotation = Quaternion.Euler(0, 0, 0);
        else if (Input.GetAxisRaw("Vertical") == -1) transform.rotation = Quaternion.Euler(0, 0, 180);

        if (transform.position.x >= BoundaryX || transform.position.x <= -BoundaryX) transform.position += Vector3.right * Input.GetAxisRaw("Horizontal") * -speed * Time.deltaTime;
        if (transform.position.y >= BoundaryY || transform.position.y <= -BoundaryY) transform.position += Vector3.up * Input.GetAxisRaw("Vertical") * -speed * Time.deltaTime;
    }
}
