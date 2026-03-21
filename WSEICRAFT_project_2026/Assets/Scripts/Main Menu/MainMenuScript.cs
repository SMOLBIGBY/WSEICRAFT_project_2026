using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenuScript : MonoBehaviour
{
    private BlackScreenFade blackScreenFade;
    DayManager dayManager;

    [SerializeField] private string sceneName;
    [SerializeField] private float fadeWaitTime = 1f;

    private void Start()
    {
        dayManager = FindObjectOfType<DayManager>();
        blackScreenFade = FindObjectOfType<BlackScreenFade>();
    }

    public void PlayButton()
    {

        PlayerPrefs.SetInt("CurrentDay", 1);
        LoadSelectedScene();
    }

    public void LoadSelectedScene()
    {
        StartCoroutine(LoadSceneWithFade(sceneName));
    }
    public void Exit()
    {
        Application.Quit();
    }

    private IEnumerator LoadSceneWithFade(string targetScene)
    {
        if (blackScreenFade != null)
            blackScreenFade.FadeIn();

        yield return new WaitForSeconds(fadeWaitTime);

        SceneManager.LoadScene(targetScene);
    }
}