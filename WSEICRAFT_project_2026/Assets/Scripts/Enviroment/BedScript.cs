using UnityEngine;

public class BedScript : MonoBehaviour
{

    private bool inCollision = false;
    DayManager dayManager;
    GameOverScreen gameOverScreen;
    AnomalyManager anomalyManager;

    PlayerManager playerManager;
    void Start()
    {
        gameOverScreen = FindAnyObjectByType<GameOverScreen>();
        anomalyManager = FindAnyObjectByType<AnomalyManager>();
        dayManager = FindAnyObjectByType<DayManager>();
        playerManager = FindAnyObjectByType<PlayerManager>();
    }

    void Update()
    {
        if (!anomalyManager.AnomalyActive && inCollision && Input.GetKeyDown(KeyCode.E))
        {

            Debug.Log("BedScript: Player interacted with the bed. Advancing to the next day.");
            dayManager.NextDay();
            playerManager.CanMove = false;
        }
        else if (inCollision && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("BedScript: Player interacted with the bed but no anomaly is active. Game Over.");
            gameOverScreen.Die();
            playerManager.CanMove = false;
        }
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
