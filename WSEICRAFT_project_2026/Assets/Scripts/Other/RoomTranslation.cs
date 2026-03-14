using UnityEngine;
using System.Collections;

public class RoomTranslation : MonoBehaviour
{
    FollowCamera2D cameraScript;
    private Player playerScript;
    private bool inCollision = false;
    private BlackScreenFade blackScreen;

    [SerializeField] private GameObject teleportPoint;
    [SerializeField] private float teleportDelay = 1f;
    [SerializeField] private float blackScreenTime = 1f;

    void Start()
    {
        cameraScript = FindAnyObjectByType<FollowCamera2D>();
        playerScript = FindAnyObjectByType<Player>();
        blackScreen = FindAnyObjectByType<BlackScreenFade>();
    }

    void Update()
    {
        if (inCollision && Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(TeleportWithDelay());
        }
    }

    IEnumerator TeleportWithDelay()
    {
        blackScreen.FadeIn();
        
        yield return new WaitForSeconds(teleportDelay); // �������� ����� ����������
        cameraScript.EnableBounds = false; // Disable camera bounds during teleportation
        playerScript.transform.position = teleportPoint.transform.position;

        yield return new WaitForSeconds(blackScreenTime); // ������� ����� ������� ������
        cameraScript.EnableBounds = true; // Disable camera bounds during teleportation
        blackScreen.FadeOut();
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
