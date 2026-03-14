using UnityEngine;

public class AnomalyManager : MonoBehaviour
{
    
    public Anomaly[] anomalies;
    public bool AnomalyActive { get; private set; }

    void Start()
    {
        int day = DayManager.Instance.currentDay;

        if (day == 1)
        {
            Debug.Log("Day 1: No anomaly");
            AnomalyActive = false;
            return;
        }

        float chance = Random.value;

        if (chance <= 0.5f)
        {
            Debug.Log("No anomaly today");
            AnomalyActive = false;
        }
        else
        {
            AnomalyActive = true;
            SpawnRandomAnomaly();
        }
    }

    void SpawnRandomAnomaly()
    {
        int index = Random.Range(0, anomalies.Length);

        anomalies[index].Activate();

        Debug.Log("Anomaly triggered: " + anomalies[index].name);
    }
}