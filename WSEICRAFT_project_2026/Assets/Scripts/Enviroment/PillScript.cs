using UnityEngine;

public class PillScript : MonoBehaviour
{

    private bool inCollision = false;
    GameOverScreen gameOverScreen;
    DayManager dayManager;
    AnomalyManager anomalyManager;
    void Start()
    {
        gameOverScreen = FindAnyObjectByType<GameOverScreen>();
        dayManager = FindAnyObjectByType<DayManager>();
        anomalyManager = FindAnyObjectByType<AnomalyManager>();
    }

    void Update()
    {
        if (anomalyManager.AnomalyActive && inCollision && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("PillScript: Player interacted with the pill.");
            dayManager.NextDay();
        }
        else if (inCollision && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("PillScript: Player interacted with the pill but no anomaly is active. Game Over.");
            gameOverScreen.Die();
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
