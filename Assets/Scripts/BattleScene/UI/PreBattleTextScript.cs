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

    public void PlayAnim(int currRound)
    {
        if(preBattleAnimator == null)
            preBattleAnimator = GetComponent<Animator>();
        preBattleAnimator.SetInteger("Round",currRound);
        preBattleAnimator.Play("PreBattleTextImage");
    }
    
    public void FinishAnim()
    {
        preBattleAnimator.SetBool("SetFinish", true);
    }

    public void ResetAnim()
    {
        preBattleAnimator.SetBool("SetFinish", false);
    }

    public void DeActivateControllers()
    {
        foreach (GameObject player in battleSceneManager.GetPlayers())
        {
            player.GetComponent<PlayerCharacterLogicScript>().GetController().DisableController();
        }
        battleSceneManager.PauseTimer();
        Debug.Log("Deactivate controllers");
    }

    public void ActivateControllers()
    {
        foreach (GameObject player in battleSceneManager.GetPlayers())
        {
            player.GetComponent<PlayerCharacterLogicScript>().GetController().EnableController();
        }
        battleSceneManager.UnPauseTimer();
        Debug.Log("Activate controllers");
    }

}
