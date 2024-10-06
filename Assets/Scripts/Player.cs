using system.collections;
using system.collections.generic;
using unityengine;

public class player : monobehaviour
{
    private Rigidbody2D rb;

    [SerializeField] private float moveSpeed;

    private void Start()
    {
    }
    void Update()
    {
        transform.position += Vector3.right * Input.GetAxisRaw("Horizontal") * moveSpeed * Time.deltaTime;
        transform.position += Vector3.up * Input.GetAxisRaw("Vertical") * moveSpeed * Time.deltaTime;


    }
}
