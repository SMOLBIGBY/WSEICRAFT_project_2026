using UnityEngine;

public class TriggerObject : MonoBehaviour
{
    private GameObject ebutton;
    void Awake()
    {
        ebutton = FindAnyObjectByType<Ebutton>().gameObject;
    }
    private void Start()
    { 
        ebutton.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (ebutton != null)
            {
                ebutton.gameObject.SetActive(true);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (ebutton != null)
            {
                ebutton.gameObject.SetActive(false);
            }
        }
    }
}
