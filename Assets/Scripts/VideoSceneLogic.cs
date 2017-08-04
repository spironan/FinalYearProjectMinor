using UnityEngine;
using System.Collections;

public class VideoSceneLogic : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (GameManager.Instance.GetMasterPlayerData().controller.getAxisActionBoolDown(ACTIONS.START))
        {
            LoadingScreenManager.LoadScene("MainMenuScene");
        }
	}
}
