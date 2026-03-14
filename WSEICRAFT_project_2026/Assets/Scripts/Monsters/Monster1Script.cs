using UnityEngine;

public class Monster1Script : MonoBehaviour
{

    Rigidbody2D rb;
    public float moveSpeed = 2f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
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
}
