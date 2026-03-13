using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenuScript : MonoBehaviour
{
    BlackScreenFade blackScreenFade;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        blackScreenFade = FindObjectOfType<BlackScreenFade>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayButton()
    {
        // Load the next scene (assuming the next scene is at index 1)
        StartCoroutine(LoadSceneWithFade(1));
    }

    IEnumerator LoadSceneWithFade(int sceneIndex)
    {
        // Start the fade-out effect
        blackScreenFade.FadeIn();

        // Wait for the fade-out to complete (assuming it takes 1 second)
        yield return new WaitForSeconds(1f);

        // Load the next scene
        SceneManager.LoadScene(1);
    }
}
