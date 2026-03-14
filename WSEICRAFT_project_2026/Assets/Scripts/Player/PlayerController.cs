using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(AudioSource))]
public class PlayerController : MonoBehaviour
{
    private PlayerManager playerManager;

    [Header("Movement")]
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float runSpeed = 8f;
    [SerializeField] private GameObject playerSprite;

    [Header("Footstep Sound")]
    [SerializeField] private AudioClip footstepClip;
    [SerializeField][Range(0f, 1f)] private float footstepVolume = 0.7f;
    [SerializeField] private float minPitch = 0.9f;
    [SerializeField] private float maxPitch = 1.1f;
    [SerializeField] private float walkStepInterval = 0.45f;
    [SerializeField] private float runStepInterval = 0.3f;

    private float moveInput;
    private Rigidbody2D rb;
    private AudioSource audioSource;
    private bool isFacingRight = true;
    private bool isRunning = false;

    private float currentSpeed;
    private float stepTimer;

    void Awake()
    {
        playerManager = FindAnyObjectByType<PlayerManager>();
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();

        audioSource.playOnAwake = false;
        audioSource.loop = false;
        audioSource.spatialBlend = 0f;

        currentSpeed = walkSpeed;
    }

    void Update()
    {
        moveInput = Input.GetAxisRaw("Horizontal");

        if (isFacingRight && moveInput < 0)
        {
            Flip();
        }
        else if (!isFacingRight && moveInput > 0)
        {
            Flip();
        }

        Run();
        HandleFootsteps();
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(moveInput * currentSpeed, rb.linearVelocity.y);
    }

    void Run()
    {
        if (Input.GetKey(KeyCode.LeftShift) && playerManager.CurrentStamina > 0 && moveInput != 0)
        {
            playerManager.CurrentStamina -= Time.deltaTime * 20f;
            isRunning = true;
            currentSpeed = runSpeed;
        }
        else
        {
            isRunning = false;
            currentSpeed = walkSpeed;
        }
    }

    void HandleFootsteps()
    {
        if (footstepClip == null)
            return;

        if (moveInput == 0)
        {
            stepTimer = 0f;
            return;
        }

        stepTimer -= Time.deltaTime;

        float currentStepInterval = isRunning ? runStepInterval : walkStepInterval;

        if (stepTimer <= 0f)
        {
            PlayFootstep();
            stepTimer = currentStepInterval;
        }
    }

    void PlayFootstep()
    {
        audioSource.pitch = Random.Range(minPitch, maxPitch);
        audioSource.PlayOneShot(footstepClip, footstepVolume);
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;

        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}