using UnityEngine;
using System.Collections;

public class PreBattleTextScript : MonoBehaviour 
{
    BattleSceneManager battleSceneManager;
    Animator preBattleAnimator;

	// Use this for initialization
	void Start () {
        battleSceneManager = GameObject.FindWithTag("UserInterface").GetComponent<BattleSceneManager>();
        preBattleAnimator = GetComponent<Animator>();
	}

    void PlayAnim()
    {
        int currRound = battleSceneManager.GetCurrentRound();
        preBattleAnimator.SetInteger("Round",currRound);
        preBattleAnimator.Play(currRound);
    }
}
