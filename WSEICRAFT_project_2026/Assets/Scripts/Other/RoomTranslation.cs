using UnityEngine;
using System.Collections;

public class RoomTranslation : MonoBehaviour
{
    PlayerManager playerManager;
    FollowCamera2D cameraScript;
    private Player playerScript;
    private bool inCollision = false;
    private BlackScreenFade blackScreen;

    [SerializeField] private GameObject teleportPoint;
    [SerializeField] private float teleportDelay = 1f;
    [SerializeField] private float blackScreenTime = 1f;

    [Header("Teleport Sound")]
    [SerializeField] private AudioClip teleportSound;
    [SerializeField] private float teleportVolume = 1f;

    void Start()
    {
        playerManager = FindAnyObjectByType<PlayerManager>();
        cameraScript = FindAnyObjectByType<FollowCamera2D>();
        playerScript = FindAnyObjectByType<Player>();
        blackScreen = FindAnyObjectByType<BlackScreenFade>();
    }

    void Update()
    {
        if (playerManager.CanTeleport && inCollision && Input.GetKeyDown(KeyCode.E))
        {
            playerManager.CanTeleport = false;
            StartCoroutine(TeleportWithDelay());
            playerManager.CanMove = false;
        }
    }

    IEnumerator TeleportWithDelay()
    {
        blackScreen.FadeIn();

        yield return new WaitForSeconds(teleportDelay);

        cameraScript.EnableBounds = false;

        playerScript.transform.position = teleportPoint.transform.position;

        // звук телепорта
        if (teleportSound != null)
        {
            OneShotAudio.Play2D(teleportSound, teleportVolume);
        }

        yield return new WaitForSeconds(blackScreenTime);

        cameraScript.EnableBounds = true;
        playerManager.CanMove = true;
        yield return new WaitForSeconds(0.2f);
        blackScreen.FadeOut();
        yield return new WaitForSeconds(2f);
        playerManager.CanTeleport = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out Player player))
        {
            inCollision = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        inCollision = false;
    }
}