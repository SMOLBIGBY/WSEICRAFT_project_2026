using UnityEngine;
using UnityEngine.SceneManagement;

public class DayManager : MonoBehaviour
{
    public static DayManager Instance;

    public int currentDay;
    public int maxDays = 7;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // Load saved day (default = 1 if none exists)
        currentDay = PlayerPrefs.GetInt("CurrentDay", 1);
    }

    void Update()
    {
        // Debug reset
        if (Input.GetKeyDown(KeyCode.N))
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();

            currentDay = 1;

            Debug.Log("PlayerPrefs cleared. CurrentDay reset to 1.");
        }
    }

    public void NextDay()
    {
        currentDay += 1;

        if (currentDay > maxDays)
        {
            currentDay = maxDays;
        }

        PlayerPrefs.SetInt("CurrentDay", currentDay);
        PlayerPrefs.Save();

        // Reload scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ResetDays()
    {
        currentDay = 1;

        PlayerPrefs.SetInt("CurrentDay", currentDay);
        PlayerPrefs.Save();

        Debug.Log("Days reset to Day 1.");
    }
}