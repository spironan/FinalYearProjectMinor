using UnityEngine;
using System.Collections;

public class urgentChangeScene : MonoBehaviour 
{
    //public uint players;
    GameManager gameManager;
    SoundController soundController;
	// Use this for initialization
	void Start () {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        soundController = GameObject.FindGameObjectWithTag("SoundController").GetComponent<SoundController>();
        soundController.ChangeBGM(AudioClipManager.GetInstance().GetAudioClip("MainMenu"));
	}
	
	// Update is called once per frame
    void Update()
    {
        //for (int i = 0; i < gameManager.GetPlayerSize(); ++i)
        //{
        //    if (gameManager.GetPlayer(i).controller.getButtonAction(ACTIONS.SELECT))//getIsKeyDown(BUTTON_INPUT.A))
        //    {
        //        gameManager.SetGameMode(GAME_MODES.LOCAL_PVP);
        //        LoadingScreenManager.LoadScene("CharacterSelectScene");
        //    }
        //}

        for (int i = 0; i < gameManager.GetPlayerSize(); ++i)
        {
            gameManager.GetPlayer(i).UnAssign();//unassign player info
        }

        if (gameManager.GetMasterPlayerData().controller.getIsKeyDown(BUTTON_INPUT.A))
        {
            gameManager.SetGameMode(GAME_MODES.LOCAL_PVP);
            LoadingScreenManager.LoadScene("CharacterSelectScene");
        }
        else if (gameManager.GetMasterPlayerData().controller.getIsKeyDown(BUTTON_INPUT.B))
        {
            gameManager.SetGameMode(GAME_MODES.PRACTICE);
            LoadingScreenManager.LoadScene("CharacterSelectScene");
        }

        //    if (gameManager.GetPlayerSize() == players)
        //        LoadingScreenManager.LoadScene("CharacterSelectScene");
	}
}
