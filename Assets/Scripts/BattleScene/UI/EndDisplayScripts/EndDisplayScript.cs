using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class EndDisplayScript : MonoBehaviour 
{
    enum ENDGAME_OPTIONS
    {
        REMATCH,
        MAP_SELECT,
        CHARACTER_SELECT,
        BACK_TO_MAIN
    };

    ENDGAME_OPTIONS button = ENDGAME_OPTIONS.REMATCH;
    Button[] buttons = null;
    ListOfControllerActions masterController = null;
    PointerEventData pointer;
    EventSystem eventSystem;
    PlayerWinScript winScript;

    void Awake()
    {
        eventSystem = GameObject.FindWithTag("EventSystem").GetComponent<EventSystem>();
        pointer = new PointerEventData(EventSystem.current); // pointer event for Execute
        winScript = transform.parent.gameObject.GetComponentInChildren<PlayerWinScript>();
    }

	// Use this for initialization
	public void Reset () {

        button = ENDGAME_OPTIONS.REMATCH;
        if(buttons == null)
            buttons = GetComponentsInChildren<Button>();
        if(masterController == null)
            masterController = GameObject.FindWithTag("GameManager").GetComponent<GameManager>().GetMasterPlayerData().controller;
        winScript.DisplayPlayerVictory();
        StartCoroutine(HighlightButton());
    }

    IEnumerator HighlightButton()
    {
        eventSystem.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        eventSystem.SetSelectedGameObject(buttons[(int)button].gameObject);
    }

	// Update is called once per frame
	void Update () {

        if (masterController.getAxisActionBoolDown(ACTIONS.MOVE_DOWN))
        {
            if (button < ENDGAME_OPTIONS.BACK_TO_MAIN)
            {
                button++;
                buttons[(int)button].Select();
            }
        }
        else if (masterController.getAxisActionBoolDown(ACTIONS.MOVE_UP))
        {
            if (button > ENDGAME_OPTIONS.REMATCH)
            {
                button--;
                buttons[(int)button].Select();
            }
        }

        if (masterController.getButtonAction(ACTIONS.SELECT))
        {
            ExecuteEvents.Execute(buttons[(int)button].gameObject, pointer, ExecuteEvents.submitHandler);
            //buttons[(int)button].onClick.Invoke();
        }
	}

}
