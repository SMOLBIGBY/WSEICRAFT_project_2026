using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class TriggerText : MonoBehaviour
{
    private TextMeshProUGUI text;
    private Image image;

    [SerializeField] private float fadeTime = 1f;

    private Coroutine currentFade;

    void Awake()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
        image = GetComponentInChildren<Image>();

        SetAlpha(0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>())
        {
            if (currentFade != null) StopCoroutine(currentFade);
            currentFade = StartCoroutine(Fade(1f));
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>())
        {
            if (currentFade != null) StopCoroutine(currentFade);
            currentFade = StartCoroutine(Fade(0f));
        }
    }

    IEnumerator Fade(float targetAlpha)
    {
        float startAlpha = text.color.a;
        float time = 0;

        while (time < fadeTime)
        {
            time += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, targetAlpha, time / fadeTime);

            SetAlpha(alpha);

            yield return null;
        }

        SetAlpha(targetAlpha);
    }

    void SetAlpha(float alpha)
    {
        if (text != null)
        {
            Color c = text.color;
            c.a = alpha;
            text.color = c;
        }

        if (image != null)
        {
            Color c = image.color;
            c.a = alpha;
            image.color = c;
        }
    }
}