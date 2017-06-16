using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class EndDisplayScript : MonoBehaviour 
{
    enum BUTTON_OPTIONS
    {
        REMATCH,
        MAP_SELECT,
        CHARACTER_SELECT,
        BACK_TO_MAIN
    };

    BUTTON_OPTIONS button = BUTTON_OPTIONS.REMATCH;
    Button[] buttons = null;
    ListOfControllerActions masterController = null;
    bool reset = false;

	// Use this for initialization
	public void Reset () {
        button = BUTTON_OPTIONS.REMATCH;
        if(buttons == null)
            buttons = GetComponentsInChildren<Button>();
        if(masterController == null)
            masterController = GameObject.FindWithTag("GameManager").GetComponent<GameManager>().GetMasterPlayerData().controller;
        reset = true;
    }
	
	// Update is called once per frame
	void Update () {

        if (reset)
        {
            buttons[(int)button].Select();
            reset = false;
        }

        if (masterController.getAxisActionBoolDown(ACTIONS.MOVE_DOWN))
        {
            if(button < BUTTON_OPTIONS.BACK_TO_MAIN)
            {
                button++;
                buttons[(int)button].Select();
            }
        }
        else if (masterController.getAxisActionBoolDown(ACTIONS.MOVE_UP))
        {
            if (button > BUTTON_OPTIONS.REMATCH)
            {
                button--;
                buttons[(int)button].Select();
            }
        }

        if (masterController.getButtonAction(ACTIONS.SELECT))
            buttons[(int)button].onClick.Invoke();
	}

}
