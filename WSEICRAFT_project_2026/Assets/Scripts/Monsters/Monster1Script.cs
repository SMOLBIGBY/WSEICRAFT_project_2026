using UnityEngine;
using System.Collections;

public class Monster1Script : MonoBehaviour
{
    GameOverScreen gameOverScreen;
    Rigidbody2D rb;
    public float moveSpeed = 4f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameOverScreen = FindAnyObjectByType<GameOverScreen>();
        rb = GetComponent<Rigidbody2D>();
        moveSpeed = 0f;
        StartCoroutine(SpeedCoroutine(2f));
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

    IEnumerator SpeedCoroutine(float delay)
    {
        yield return new WaitForSeconds(delay);
        moveSpeed = 4f;
    }
}
