using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PreBattleTextScript : MonoBehaviour 
{
    BattleSceneManager battleSceneManager;
    Animator preBattleAnimator;

	// Use this for initialization
	void Start () 
    {
        battleSceneManager = GameObject.FindWithTag("UserInterface").GetComponent<BattleSceneManager>();
        preBattleAnimator = GetComponent<Animator>();
	}

    public void PlayAnim()
    {
        GetComponent<Image>().CrossFadeAlpha(1.0f,0.0f,false);

        int currRound = battleSceneManager.GetCurrentRound();
        preBattleAnimator.SetInteger("Round",currRound);
        preBattleAnimator.Play(currRound);
    }

    public void DeActivateControllers()
    {
        foreach (GameObject player in battleSceneManager.GetPlayers())
        {
            player.GetComponent<PlayerCharacterLogicScript>().GetController().DisableController();
        }
        battleSceneManager.PauseTimer();
    }

    public void ActivateControllers()
    {
        foreach (GameObject player in battleSceneManager.GetPlayers())
        {
            player.GetComponent<PlayerCharacterLogicScript>().GetController().EnableController();
        }
        battleSceneManager.UnPauseTimer();
    }

}
