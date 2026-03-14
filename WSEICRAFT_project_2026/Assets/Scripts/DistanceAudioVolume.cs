using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class DistanceAudioVolume : MonoBehaviour
{
    [Header("References")]
    private AudioSource audioSource;
    private Transform player;

    [Header("Distance Settings")]
    [SerializeField] private float minDistance = 2f;   // на этой дистанции громкость максимальная
    [SerializeField] private float maxDistance = 10f;  // после этой дистанции звука нет

    [Header("Volume Settings")]
    [SerializeField][Range(0f, 1f)] private float maxVolume = 1f;
    [SerializeField][Range(0f, 1f)] private float minVolume = 0f;

    [Header("Options")]
    [SerializeField] private bool stopAudioWhenTooFar = false;

    void Awake()
    {
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        FindPlayer();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (player == null)
        {
            FindPlayer();
            return;
        }

        UpdateVolume();
    }

    private void FindPlayer()
    {
        Player playerScript = FindAnyObjectByType<Player>();

        if (playerScript != null)
            player = playerScript.transform;
    }

    private void UpdateVolume()
    {
        float distance = Vector2.Distance(transform.position, player.position);

        if (distance >= maxDistance)
        {
            audioSource.volume = 0f;

            if (stopAudioWhenTooFar && audioSource.isPlaying)
                audioSource.Pause();

            return;
        }

        if (stopAudioWhenTooFar && !audioSource.isPlaying)
            audioSource.UnPause();

        if (distance <= minDistance)
        {
            audioSource.volume = maxVolume;
            return;
        }

        float t = Mathf.InverseLerp(maxDistance, minDistance, distance);
        audioSource.volume = Mathf.Lerp(minVolume, maxVolume, t);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, minDistance);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, maxDistance);
    }
}