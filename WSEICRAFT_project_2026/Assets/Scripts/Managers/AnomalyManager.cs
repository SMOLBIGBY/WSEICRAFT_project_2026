using UnityEngine;

public class AnomalyManager : MonoBehaviour
{
    public Anomaly[] anomalies;

    void Start()
    {
        int day = DayManager.Instance.currentDay;

        if (day == 1)
        {
            Debug.Log("Day 1: No anomaly");
            return;
        }

        float chance = Random.value;

        if (chance <= 0.5f)
        {
            Debug.Log("No anomaly today");
        }
        else
        {
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