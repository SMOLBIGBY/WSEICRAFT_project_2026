using UnityEngine;
using UnityEngine.UI;

public class StaminaBarScript : MonoBehaviour
{
    PlayerManager playerManager;

    public Image StaminaBarImage;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerManager = FindAnyObjectByType<PlayerManager>();

    }

    // Update is called once per frame
    void Update()
    {
        StaminaBarImage.fillAmount = playerManager.CurrentStamina / playerManager.MaxStamina;
    }
}

