using UnityEngine;

public class Anomaly : MonoBehaviour
{
    public enum AnomalyType
    {
        MoveObject,
        ChangeColor,
        RotateObject,
        ToggleObject
    }

    public AnomalyType type;

    public Transform targetObject;

    public Vector3 movePosition;
    public float rotationZ; // 2D rotation only on Z axis

    public Color newColor;

    public GameObject toggleObject;

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
                    float randomRotation = Random.Range(45f, 180f);
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
                        colorToSet.a = 1f; // Ensure fully visible
                        sr.color = colorToSet;

                        Debug.Log($"Changed color of {targetObject.name} to {colorToSet}");
                    }
                }
                break;

            case AnomalyType.ToggleObject:
                if (toggleObject != null)
                    toggleObject.SetActive(!toggleObject.activeSelf);
                break;
        }

        Debug.Log("Anomaly triggered: " + gameObject.name);
    }
}