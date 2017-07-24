using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PreBattleTextScript : MonoBehaviour 
{
    BattleSceneManager battleSceneManager;
    Animator preBattleAnimator;

	// Use this for initialization
	void Awake () 
    {
        battleSceneManager = GameObject.FindWithTag("UserInterface").GetComponent<BattleSceneManager>();
        preBattleAnimator = GetComponent<Animator>();
	}

    public void PlayAnim(int currRound)
    {
        if(preBattleAnimator == null)
            preBattleAnimator = GetComponent<Animator>();
        preBattleAnimator.SetInteger("Round", currRound);
        StartCoroutine(WaitForAnimFinish(2.1f));
        //preBattleAnimator.Play("PreBattleTextImage");
    }

    IEnumerator WaitForAnimFinish(float duration)
    {
        DeActivateControllers();
        //Debug.Log("waiting, time left : " + duration);
        yield return new WaitForSeconds(duration);
        //Debug.Log("Finished waiting, allowing players to move");
        ActivateControllers();
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
        battleSceneManager.PauseTimer();
        foreach (GameObject player in battleSceneManager.GetPlayers())
        {
            player.GetComponent<PlayerCharacterLogicScript>().GetController().DisableController();
        }
        Debug.Log("Deactivate controllers");
    }

    public void ActivateControllers()
    {
        battleSceneManager.UnPauseTimer();
        foreach (GameObject player in battleSceneManager.GetPlayers())
        {
            player.GetComponent<PlayerCharacterLogicScript>().GetController().EnableController();
        }
        Debug.Log("Activate controllers");
    }

}
