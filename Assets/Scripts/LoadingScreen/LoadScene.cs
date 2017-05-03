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

    //How To Quit Correctly in Android Version Not Done Yet
    public void ExitApplication()
    {
        if (SceneManager.GetActiveScene().name != "Main")
            Debug.Log("Can Only Exit From Main Menu Safety Check");

        //Save Character Data
        //PlayerDataScript.SavePlayerData(Int64.Parse(PlayerManager.GetInstance().GetCurrentPlayer().GetUserID()));

        //LoopThrough All DataBase and Drop them all
        DatabaseSystem.GetInstance().Clear();

        //Do Check to Quit correctly on Android
        Debug.Log("Quitting Successful");
        Application.Quit();
    }

}
