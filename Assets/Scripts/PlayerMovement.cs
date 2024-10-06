using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    float speed = 4f;
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
    }
}
