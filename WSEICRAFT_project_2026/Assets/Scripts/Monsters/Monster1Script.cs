using UnityEngine;

public class Monster1Script : MonoBehaviour
{
    GameOverScreen gameOverScreen;
    Rigidbody2D rb;
    public float moveSpeed = 2f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameOverScreen = FindAnyObjectByType<GameOverScreen>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(-1 * moveSpeed, rb.linearVelocity.y);

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out Player player))
        {
            gameOverScreen.Die();
        }
    }
}
