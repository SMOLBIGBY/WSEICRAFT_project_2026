using UnityEngine;
using System.Collections;

public class Anomaly : MonoBehaviour
{
    public enum AnomalyType
    {
        MoveObject,
        ChangeColor,
        RotateObject,
        ToggleObject,
        SpawnPrefab
    }

    public Transform playerPosition; // Reference to the player's position

    public AnomalyType type;

    public Transform BossSpawnPosition;

    public Transform targetObject;
    public GameObject removeObject;

    public Vector3 movePosition;
    public float rotationZ;

    public Color newColor;

    public GameObject toggleObject;

    public float ToggleDelay = 1f;
    public float SpawnDelay = 5f;

    public bool ObjectShouldBeRemoved = false;

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            playerPosition = player.transform;
        }
    }

    public void Activate()
    {
        switch (type)
        {
            case AnomalyType.MoveObject:

                if (targetObject != null)
                    targetObject.position = movePosition;

                break;

            case AnomalyType.RotateObject:

                if (targetObject != null)
                {
                    float randomRotation = Random.Range(20f, 30f);
                    targetObject.localRotation = Quaternion.Euler(0, 0, randomRotation);

                    Debug.Log($"Rotated {targetObject.name} by {randomRotation}°");
                }

                break;

            case AnomalyType.ChangeColor:

                if (targetObject != null)
                {
                    SpriteRenderer sr = targetObject.GetComponent<SpriteRenderer>();

                    if (sr != null)
                    {
                        Color colorToSet = newColor;
                        colorToSet.a = 1f;

                        sr.color = colorToSet;

                        Debug.Log($"Changed color of {targetObject.name} to {colorToSet}");
                    }
                }

                break;

            case AnomalyType.ToggleObject:

                StartCoroutine(ToggleAfterDelay(ToggleDelay));

                break;

            case AnomalyType.SpawnPrefab:

                StartCoroutine(SpawnAfterDelay(SpawnDelay));

                break;
        }

        Debug.Log("Anomaly triggered: " + gameObject.name);
    }

    IEnumerator SpawnAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (targetObject == null || playerPosition == null)
            yield break;

        Vector3 spawnPosition = BossSpawnPosition.position;

        Instantiate(targetObject.gameObject, spawnPosition, Quaternion.identity);

        Debug.Log($"Spawned {targetObject.name} at {spawnPosition} after delay");
    }

    IEnumerator ToggleAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (toggleObject != null)
        {
            toggleObject.SetActive(!toggleObject.activeSelf);

            if (ObjectShouldBeRemoved && removeObject != null)
            {
                removeObject.SetActive(false);
            }

            Debug.Log($"Toggled {toggleObject.name} to {(toggleObject.activeSelf ? "active" : "inactive")} after delay");
        }
    }
}