using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CanvasGroup))]
public class GameOverScreen : MonoBehaviour
{
    DayManager dayManager;
    [Header("Fade")]
    [SerializeField] private float fadeDuration = 1.5f;
    [SerializeField] private bool ignoreTimeScale = true;

    [Header("Audio")]
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private float deathSoundVolume = 1f;

    private CanvasGroup canvasGroup;
    private Coroutine fadeCoroutine;
    private bool isDead;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();

        if (canvasGroup == null)
        {
            Debug.LogError("CanvasGroup not found!");
            enabled = false;
            return;
        }

        canvasGroup.alpha = 0f;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.interactable = false;
    }
    void Start()
    {
        dayManager = FindAnyObjectByType<DayManager>();
    }
    public void Die()
    {
        dayManager.ResetDays();
        if (isDead)
            return;

        isDead = true;

        if (!gameObject.activeSelf)
            gameObject.SetActive(true);

        if (fadeCoroutine != null)
            StopCoroutine(fadeCoroutine);

        if (deathSound != null)
            OneShotAudio.Play2D(deathSound, deathSoundVolume);

        fadeCoroutine = StartCoroutine(FadeInRoutine());
    }

    private IEnumerator FadeInRoutine()
    {
        canvasGroup.blocksRaycasts = true;
        canvasGroup.interactable = false;

        float startAlpha = canvasGroup.alpha;
        float elapsed = 0f;

        while (elapsed < fadeDuration)
        {
            elapsed += ignoreTimeScale ? Time.unscaledDeltaTime : Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / fadeDuration);

            canvasGroup.alpha = Mathf.Lerp(startAlpha, 1f, t);

            yield return null;
        }

        canvasGroup.alpha = 1f;
    }

    public void ResetDeathFade()
    {
        isDead = false;

        if (fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine);
            fadeCoroutine = null;
        }

        canvasGroup.alpha = 0f;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.interactable = false;
    }
}