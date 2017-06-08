using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TimerScript : MonoBehaviour 
{
    Text timerText;
    BattleSceneManager battleSceneManager;
    bool resetCond;
	// Use this for initialization
	void Start () 
    {
        battleSceneManager = GameObject.FindGameObjectWithTag("UserInterface").GetComponent<BattleSceneManager>();
        timerText = this.gameObject.GetComponent<Text>();
        resetCond = false;
    }
	
	// Update is called once per frame
	void FixedUpdate() 
    {
        int timer = (int)battleSceneManager.GetCurrentBattleTimer();
        if (timer <= 10)
        {
            timerText.color = new Color(1, 0, 0);
            resetCond = true;
        }
        else if (resetCond && timer > 10)
        {
            timerText.color = new Color(1, 1, 1);
            resetCond = false;
        }
        
        timerText.text = timer.ToString();
	}
}
