using UnityEngine;

public class TriggerObject : MonoBehaviour
{
    private GameObject ebutton;
    private void Start()
    {
        ebutton = FindAnyObjectByType<Ebutton>().gameObject;   
        ebutton.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (ebutton != null)
        {
            ebutton.gameObject.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (ebutton != null)
        {
            ebutton.gameObject.SetActive(false);
        }
    }
}
