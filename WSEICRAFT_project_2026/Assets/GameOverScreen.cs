using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CanvasGroup))]
public class GameOverScreen : MonoBehaviour
{
    [Header("Fade")]
    [SerializeField] private float fadeDuration = 1.5f;
    [SerializeField] private bool ignoreTimeScale = true;

    [Header("Next Scene")]
    [SerializeField] private float loadSceneDelay = 3f;
    [SerializeField] private MainMenuScript mainMenuScript;

    [Header("Audio")]
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private float deathSoundVolume = 1f;

    private CanvasGroup canvasGroup;
    private Coroutine fadeCoroutine;
    private Coroutine loadCoroutine;
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

        HideInstantly();
    }

    private void Start()
    {
        if (mainMenuScript == null)
            mainMenuScript = FindObjectOfType<MainMenuScript>();
    }


    public void Die()
    {
        if (isDead)
            return;

        isDead = true;

        if (!gameObject.activeSelf)
            gameObject.SetActive(true);

        if (fadeCoroutine != null)
            StopCoroutine(fadeCoroutine);

        if (loadCoroutine != null)
            StopCoroutine(loadCoroutine);

        if (deathSound != null)
            OneShotAudio.Play2D(deathSound, deathSoundVolume);

        fadeCoroutine = StartCoroutine(FadeInRoutine());
        loadCoroutine = StartCoroutine(LoadSceneAfterDelay());
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
        fadeCoroutine = null;
    }

    private IEnumerator LoadSceneAfterDelay()
    {
        if (ignoreTimeScale)
            yield return new WaitForSecondsRealtime(loadSceneDelay);
        else
            yield return new WaitForSeconds(loadSceneDelay);

        if (mainMenuScript != null)
        {
            mainMenuScript.LoadSelectedScene();
        }
        else
        {
            Debug.LogWarning("MainMenuScript not found!");
        }

        loadCoroutine = null;
    }

    public void ResetDeathFade()
    {
        isDead = false;

        if (fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine);
            fadeCoroutine = null;
        }

        if (loadCoroutine != null)
        {
            StopCoroutine(loadCoroutine);
            loadCoroutine = null;
        }

        HideInstantly();
    }

    private void HideInstantly()
    {
        canvasGroup.alpha = 0f;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.interactable = false;
    }
}