using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public bool isHolding = false;
    [SerializeField] private float speed;
    [SerializeField] private GameObject playerSprite;
    [SerializeField] private AudioClip runSound;
    private AudioSource audioSource;
    private float MoveInput;
    private Rigidbody2D rb;
    private bool isFacingRight;
    private Animator anim;

    public GameObjectHandler handler;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = playerSprite.GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (MoveInput != 0)
        {
            anim.SetInteger("State", 1);

        }
        else if (MoveInput == 0)
        {
            anim.SetInteger("State", 0);
        }
        if (MoveInput != 0)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.clip = runSound;
                audioSource.loop = true;
                audioSource.Play();
            }
        }
        else
        {
            if (audioSource.isPlaying) 
            {
                audioSource.Stop();
            }
        }
        MoveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(MoveInput * speed, rb.velocity.y);
        if (isFacingRight == true && MoveInput > 0)
        {
            Flip();
        }
        if (isFacingRight == false && MoveInput < 0)
        {
            Flip();
        }
    }
    void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 Scaler = gameObject.transform.localScale;
        Scaler.x *= -1;
        gameObject.transform.localScale = Scaler;
    }  
}


