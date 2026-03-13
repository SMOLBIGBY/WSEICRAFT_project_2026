using UnityEngine;

public class PlayerController : MonoBehaviour
{

    PlayerManager playerManager;

    [SerializeField] private float speed = 5f;
    [SerializeField] private GameObject playerSprite;

    private float moveInput;
    private Rigidbody2D rb;
    private bool isFacingRight = true;

    private bool isRunning = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Get player input
        moveInput = Input.GetAxis("Horizontal");

        // Flip character
        if (isFacingRight && moveInput < 0)
        {
            Flip();
        }
        else if (!isFacingRight && moveInput > 0)
        {
            Flip();
        }
    }
    void FixedUpdate()
    {
        Run();
        rb.linearVelocity = new Vector2(moveInput * speed, rb.linearVelocity.y);
    }
    

    void Run()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            isRunning = true;
            speed = 8f; // Increase speed when running
        }
        else
        {
            isRunning = false;
            speed = 5f; // Reset to normal speed when not running
        }
        
    }



    void Flip()
    {
        isFacingRight = !isFacingRight;

        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}