using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenuScript : MonoBehaviour
{
    private BlackScreenFade blackScreenFade;

    [SerializeField] private string sceneName;
    [SerializeField] private float fadeWaitTime = 1f;

    private void Start()
    {
        blackScreenFade = FindObjectOfType<BlackScreenFade>();
    }

    public void PlayButton()
    {
        LoadSelectedScene();
    }

    public void LoadSelectedScene()
    {
        StartCoroutine(LoadSceneWithFade(sceneName));
    }

    private IEnumerator LoadSceneWithFade(string targetScene)
    {
        if (blackScreenFade != null)
            blackScreenFade.FadeIn();

        yield return new WaitForSeconds(fadeWaitTime);

        SceneManager.LoadScene(targetScene);
    }
}