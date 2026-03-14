using UnityEngine;

public class BedScript : MonoBehaviour
{

    private bool inCollision = false;
    DayManager dayManager;
    void Start()
    {
        dayManager = FindAnyObjectByType<DayManager>();
    }

    void Update()
    {
        if (inCollision && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("BedScript: Player interacted with the bed. Advancing to the next day.");
            dayManager.NextDay();
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
