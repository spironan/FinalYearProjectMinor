using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TimerScript : MonoBehaviour 
{
    Text timerText;
    BattleSceneManager battleSceneManager;
	// Use this for initialization
	void Start () 
    {
        battleSceneManager = GameObject.FindGameObjectWithTag("UserInterface").GetComponent<BattleSceneManager>();
        timerText = this.gameObject.GetComponent<Text>();
    }
	
	// Update is called once per frame
	void FixedUpdate() 
    {
        timerText.text = ((int)battleSceneManager.GetCurrentBattleTimer()).ToString();
	}
}
