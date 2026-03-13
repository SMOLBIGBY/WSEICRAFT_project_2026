using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int currentDay;
    public int maxDays = 7;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        LoadDay();
    }

    void LoadDay()
    {
        currentDay = PlayerPrefs.GetInt("CurrentDay", 2);
    }

    public void NextDay()
    {
        currentDay++;

        if (currentDay > maxDays)
        {
            currentDay = maxDays;
        }

        PlayerPrefs.SetInt("CurrentDay", currentDay);
        PlayerPrefs.Save();
    }

    public void ResetDays()
    {
        currentDay = 2;
        PlayerPrefs.SetInt("CurrentDay", currentDay);
        PlayerPrefs.Save();
    }
}