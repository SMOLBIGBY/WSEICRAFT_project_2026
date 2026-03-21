using UnityEngine;

public class AnomalyManager : MonoBehaviour
{
    
    public Anomaly[] anomalies;
    public bool AnomalyActive { get; private set; }
    DayManager dayManager;

    void Start()
    {
        dayManager = FindObjectOfType<DayManager>();

        int day = dayManager.currentDay;

        // Day 1 → no anomaly
        if (day == 1)
        {
            Debug.Log("Day 1: No anomaly");
            AnomalyActive = false;
            return;
        }

        // Day 5 → ALWAYS anomaly[3]
        if (day == 5) // or dayManager.maxDays
        {
            Debug.Log("Final day: Forced anomaly");
            ActivateAnomaly(4);
            AnomalyActive = true;
            return;
        }

        // Other days → random chance
        float chance = Random.value;

        if (day != 1 && day != 5 && chance <= 0.3f)
        {
            Debug.Log("No anomaly today");
            AnomalyActive = false;
        }
        else if (day != 1 && day != 5)
        {
            AnomalyActive = true;
            SpawnRandomAnomaly();
        }
    }

    void ActivateAnomaly(int index)
    {
        if (index >= 0 && index < anomalies.Length)
        {
            anomalies[index].Activate();
            AnomalyActive = true;
            Debug.Log("Anomaly triggered: " + anomalies[index].name);
        }
    }

    void SpawnRandomAnomaly()
    {
        int index = Random.Range(0, anomalies.Length);
        anomalies[index].Activate();
        Debug.Log("Anomaly triggered: " + anomalies[index].name);
    }
}
