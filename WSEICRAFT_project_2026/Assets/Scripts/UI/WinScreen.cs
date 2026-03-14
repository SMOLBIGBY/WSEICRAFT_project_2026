using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CanvasGroup))]
public class WinScreen : MonoBehaviour
{
    [Header("Fade")]
    [SerializeField] private float fadeDuration = 1.5f;

    [Header("Next Scene")]
    [SerializeField] private float loadSceneDelay = 3f;
    [SerializeField] private MainMenuScript mainMenuScript;

    [Header("Audio")]
    [SerializeField] private AudioClip winSound;
    [SerializeField] private float winSoundVolume = 1f;

    private CanvasGroup canvasGroup;
    private Coroutine fadeCoroutine;
    private Coroutine loadCoroutine;
    private bool isShown;

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



    public void Win()
    {
        if (isShown)
            return;

        isShown = true;
        Time.timeScale = 1f;

        if (!gameObject.activeSelf)
            gameObject.SetActive(true);

        if (fadeCoroutine != null)
            StopCoroutine(fadeCoroutine);

        if (loadCoroutine != null)
            StopCoroutine(loadCoroutine);

        if (winSound != null)
            OneShotAudio.Play2D(winSound, winSoundVolume);

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
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / fadeDuration);
            canvasGroup.alpha = Mathf.Lerp(startAlpha, 1f, t);

            yield return null;
        }

        canvasGroup.alpha = 1f;
        fadeCoroutine = null;
    }

    private IEnumerator LoadSceneAfterDelay()
    {
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

    public void ResetWinScreen()
    {
        isShown = false;

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

        Time.timeScale = 1f;
        HideInstantly();
    }

    private void HideInstantly()
    {
        canvasGroup.alpha = 0f;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.interactable = false;
    }
}