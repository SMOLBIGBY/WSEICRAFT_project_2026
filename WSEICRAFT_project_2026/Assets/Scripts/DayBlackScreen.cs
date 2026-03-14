using UnityEngine;
using System.Collections;
using TMPro;

[RequireComponent(typeof(CanvasGroup))]
public class DayBlackScreen : MonoBehaviour
{

    DayManager dayManager;
    public static bool IsTransitionActive { get; private set; }

    [Header("Main")]
    [SerializeField] private int dayNumber = 1;

    [Header("Timings")]
    [SerializeField] private float startDelay = 1f;
    [SerializeField] private float textFadeInDuration = 0.5f;
    [SerializeField] private float dayWordHoldTime = 0.7f;
    [SerializeField] private float randomSymbolsDuration = 1f;
    [SerializeField] private float randomSymbolInterval = 0.06f;
    [SerializeField] private float finalDayHoldTime = 1f;
    [SerializeField] private float fadeOutDuration = 1f;
    [SerializeField] private float disableAfterFadeOutDelay = 0.05f;

    [Header("Audio")]
    [SerializeField] private AudioClip randomSymbolsSound;
    [SerializeField] private float randomSymbolsVolume = 1f;

    [SerializeField] private AudioClip finalDaySound;
    [SerializeField] private float finalDayVolume = 1f;

    [Header("Optional References")]
    [SerializeField] private TextMeshProUGUI dayText;

    private CanvasGroup canvasGroup;
    private Coroutine sequenceCoroutine;
    private Coroutine disableCoroutine;
    private AudioSource randomSymbolsSource;

    private readonly char[] randomChars =
    {
        '#', '@', '%', '&', '?', '!', '*', '$', '+', '-', '/', 'X', '0', '9'
    };

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();

        if (canvasGroup == null)
        {
            Debug.LogError("CanvasGroup component not found on this GameObject!");
            enabled = false;
            return;
        }

        if (dayText == null)
            dayText = GetComponentInChildren<TextMeshProUGUI>(true);

        if (dayText == null)
        {
            Debug.LogError("TextMeshProUGUI not found in child objects!");
            enabled = false;
            return;
        }

        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.interactable = false;

        SetTextAlpha(0f);
        dayText.text = "";
    }

    private void Start()
    {
        PlayIntro();
    }

    void Update()
    {
        dayNumber = dayManager.currentDay;
    }

    


    public void PlayIntro()
    {
        if (!gameObject.activeSelf)
            gameObject.SetActive(true);

        StopAllRunningCoroutines();

        if (randomSymbolsSource != null)
        {
            OneShotAudio.StopAndDestroy(randomSymbolsSource);
            randomSymbolsSource = null;
        }

        sequenceCoroutine = StartCoroutine(IntroSequence());
    }

    private IEnumerator IntroSequence()
    {
        IsTransitionActive = true;

        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.interactable = false;

        dayText.text = "Day ";
        SetTextAlpha(0f);

        yield return new WaitForSecondsRealtime(startDelay);

        yield return StartCoroutine(FadeTextRoutine(0f, 1f, textFadeInDuration));

        yield return new WaitForSecondsRealtime(dayWordHoldTime);

        if (randomSymbolsSound != null)
            randomSymbolsSource = OneShotAudio.Play2D(randomSymbolsSound, randomSymbolsVolume, 1f, true);

        float elapsed = 0f;

        while (elapsed < randomSymbolsDuration)
        {
            char randomChar = randomChars[Random.Range(0, randomChars.Length)];
            dayText.text = "Day " + randomChar;

            yield return new WaitForSecondsRealtime(randomSymbolInterval);
            elapsed += randomSymbolInterval;
        }

        if (randomSymbolsSource != null)
        {
            OneShotAudio.StopAndDestroy(randomSymbolsSource);
            randomSymbolsSource = null;
        }

        dayText.text = "Day " + dayNumber;

        if (finalDaySound != null)
            OneShotAudio.Play2D(finalDaySound, finalDayVolume);

        yield return new WaitForSecondsRealtime(finalDayHoldTime);

        yield return StartCoroutine(FadeOutScreenAndTextRoutine());

        IsTransitionActive = false;
        disableCoroutine = StartCoroutine(DisableAfterDelay(disableAfterFadeOutDelay));
    }

    private IEnumerator FadeOutScreenAndTextRoutine()
    {
        float startScreenAlpha = canvasGroup.alpha;
        float startTextAlpha = GetTextAlpha();

        float elapsedTime = 0f;

        while (elapsedTime < fadeOutDuration)
        {
            elapsedTime += Time.unscaledDeltaTime;
            float t = Mathf.Clamp01(elapsedTime / fadeOutDuration);

            canvasGroup.alpha = Mathf.Lerp(startScreenAlpha, 0f, t);
            SetTextAlpha(Mathf.Lerp(startTextAlpha, 0f, t));

            yield return null;
        }

        canvasGroup.alpha = 0f;
        SetTextAlpha(0f);

        canvasGroup.blocksRaycasts = false;
        canvasGroup.interactable = false;
    }

    private IEnumerator FadeTextRoutine(float from, float to, float duration)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.unscaledDeltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration);
            SetTextAlpha(Mathf.Lerp(from, to, t));
            yield return null;
        }

        SetTextAlpha(to);
    }

    private void SetTextAlpha(float alpha)
    {
        Color color = dayText.color;
        color.a = alpha;
        dayText.color = color;
    }

    private float GetTextAlpha()
    {
        return dayText.color.a;
    }

    private void StopAllRunningCoroutines()
    {
        if (sequenceCoroutine != null)
        {
            StopCoroutine(sequenceCoroutine);
            sequenceCoroutine = null;
        }

        if (disableCoroutine != null)
        {
            StopCoroutine(disableCoroutine);
            disableCoroutine = null;
        }
    }

    private IEnumerator DisableAfterDelay(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);

        if (!IsTransitionActive && canvasGroup.alpha <= 0.001f)
            gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        if (randomSymbolsSource != null)
        {
            OneShotAudio.StopAndDestroy(randomSymbolsSource);
            randomSymbolsSource = null;
        }
    }
}