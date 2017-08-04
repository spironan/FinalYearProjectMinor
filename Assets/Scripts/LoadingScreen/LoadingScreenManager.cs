// LoadingScreenManager
// --------------------------------
// built by Martin Nerurkar (http://www.martin.nerurkar.de)
// for Nowhere Prophet (http://www.noprophet.com)
//
// Licensed under GNU General Public License v3.0
// http://www.gnu.org/licenses/gpl-3.0.txt

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class LoadingScreenManager : MonoBehaviour
{
    [Header("Loading Visuals")]
    public Image progressBar;
    public Image fadeOverlay;

    public Animator loadingAnimation;
    public Text tipsText;

    [Header("Timing Settings")]
    public float waitOnLoadEnd = 0.25f;
    public float fadeDuration = 0.25f;

    [Header("Loading Settings")]
    public LoadSceneMode loadSceneMode = LoadSceneMode.Single;
    public ThreadPriority loadThreadPriority;

    [Header("Other")]
    // If loading additive, link to the cameras audio listener, to avoid multiple active audio listeners
    public AudioListener audioListener;

    AsyncOperation operation;
    Scene currentScene;

    public static string sceneToLoad = "MainMenuScene";
    public static string loadingScene = "LoadingScreen";

    public static void LoadScene(string sceneName)
    {
        Application.backgroundLoadingPriority = ThreadPriority.High;
        sceneToLoad = sceneName;
        SceneManager.LoadScene(loadingScene);
    }

    void Start()
    {
        if (sceneToLoad == "")
            return;

        if (sceneToLoad == "BattleScene")
            GameObject.FindGameObjectWithTag("FightBackground").SetActive(true);
        else
            GameObject.FindGameObjectWithTag("FightBackground").SetActive(false);

        fadeOverlay.gameObject.SetActive(true); // Making sure it's on so that we can crossfade Alpha
        tipsText.gameObject.GetComponent<Text>().CrossFadeAlpha(0, 0, true);
        loadingAnimation.gameObject.GetComponent<Image>().CrossFadeAlpha(0, 0, true);
        currentScene = SceneManager.GetActiveScene();
        StartCoroutine(LoadAsync(sceneToLoad));
    }

    private IEnumerator LoadAsync(string levelName)
    {
        ShowLoadingVisuals();
        yield return null;

        FadeIn();
        StartOperation(levelName);

        float lastProgress = 0f;

        // operation does not auto-activate scene, so it's stuck at 0.9
        while (DoneLoading() == false)
        {
            yield return null;

            if (Mathf.Approximately(operation.progress, lastProgress) == false)
            {
                progressBar.fillAmount = operation.progress;
                lastProgress = operation.progress;
            }
        }

        if (loadSceneMode == LoadSceneMode.Additive)
            audioListener.enabled = false;

        ShowCompletionVisuals();

        yield return new WaitForSeconds(waitOnLoadEnd);

        FadeOut();

        yield return new WaitForSeconds(fadeDuration);

        if (loadSceneMode == LoadSceneMode.Additive)
            SceneManager.UnloadScene(currentScene.name);
        else
            operation.allowSceneActivation = true;
    }

    private void StartOperation(string levelName)
    {
        Application.backgroundLoadingPriority = loadThreadPriority;
        operation = SceneManager.LoadSceneAsync(levelName, loadSceneMode);

        if (loadSceneMode == LoadSceneMode.Single)
            operation.allowSceneActivation = false;
    }

    private bool DoneLoading()
    {
        return (loadSceneMode == LoadSceneMode.Additive && operation.isDone) || (loadSceneMode == LoadSceneMode.Single && operation.progress >= 0.9f);
    }

    void FadeIn()
    {
        fadeOverlay.CrossFadeAlpha(0, fadeDuration, true);
        loadingAnimation.gameObject.GetComponent<Image>().CrossFadeAlpha(1,fadeDuration,true);
        progressBar.CrossFadeAlpha(1, fadeDuration, true);
        tipsText.CrossFadeAlpha(1, fadeDuration, true);
    }

    void FadeOut()
    {
        loadingAnimation.gameObject.GetComponent<Image>().CrossFadeAlpha(0, fadeDuration, true);
        fadeOverlay.CrossFadeAlpha(1, fadeDuration, true);
        progressBar.CrossFadeAlpha(0, fadeDuration, true);
        tipsText.CrossFadeAlpha(0, fadeDuration, true);
    }

    void ShowLoadingVisuals()
    {
        //loadingAnimation.gameObject.SetActive(true);
        progressBar.fillAmount = 0f;
    }

    void ShowCompletionVisuals()
    {
        //loadingAnimation.gameObject.SetActive(false);
        progressBar.fillAmount = 1f;
    }

}