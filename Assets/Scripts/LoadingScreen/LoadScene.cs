using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System;

public class LoadScene : MonoBehaviour
{
    public void LoadSceneByName(string sceneName)
    {
        LoadingScreenManager.LoadScene(sceneName);
    }
}
