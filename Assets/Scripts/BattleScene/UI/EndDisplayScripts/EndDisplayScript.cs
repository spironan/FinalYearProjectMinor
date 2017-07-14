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
    ListOfControllerActions loserController = null;
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
	public void Reset (ListOfControllerActions loserController, bool gameDraw = false) {
        button = ENDGAME_OPTIONS.REMATCH;
        if(buttons == null)
            buttons = GetComponentsInChildren<Button>();
        //if(loserController == null)
        //   loserController = GameManager.Instance.GetMasterPlayerData().controller;
        this.loserController = loserController;
        winScript.DisplayPlayerVictory(gameDraw);
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

        if (!GlobalUI.Instance.GetConfirmationDisplayActive())
        {
            if (loserController.getAxisActionBoolDown(ACTIONS.MOVE_DOWN))
            {
                if (button < ENDGAME_OPTIONS.BACK_TO_MAIN)
                {
                    button++;
                    buttons[(int)button].Select();
                    SoundSystem.Instance.PlayClip(AUDIO_TYPE.SOUND_EFFECTS, AudioClipManager.GetInstance().GetAudioClip("SelectOption"));
                }
            }
            else if (loserController.getAxisActionBoolDown(ACTIONS.MOVE_UP))
            {
                if (button > ENDGAME_OPTIONS.REMATCH)
                {
                    button--;
                    buttons[(int)button].Select();
                    SoundSystem.Instance.PlayClip(AUDIO_TYPE.SOUND_EFFECTS, AudioClipManager.GetInstance().GetAudioClip("SelectOption"));
                }
            }

            if (loserController.getButtonAction(ACTIONS.SELECT))
            {
                SoundSystem.Instance.PlayClip(AUDIO_TYPE.SOUND_EFFECTS, AudioClipManager.GetInstance().GetAudioClip("ExecuteOption"));
                switch (button)
                {
                    case ENDGAME_OPTIONS.REMATCH:
                        {
                            ExecuteEvents.Execute(buttons[(int)button].gameObject, pointer, ExecuteEvents.submitHandler);
                            //buttons[(int)button].onClick.Invoke();
                        }
                        break;
                    case ENDGAME_OPTIONS.MAP_SELECT:
                        {
                            GlobalUI.Instance.ToggleConfirmationDisplay(loserController, buttons[(int)button], EXECUTE_ACTION.BACK_TO_MAPSELECT);
                        }
                        break;
                    case ENDGAME_OPTIONS.CHARACTER_SELECT:
                        {
                            GlobalUI.Instance.ToggleConfirmationDisplay(loserController, buttons[(int)button], EXECUTE_ACTION.BACK_TO_CHARSELECT);
                        }
                        break;
                    case ENDGAME_OPTIONS.BACK_TO_MAIN:
                        {
                            GlobalUI.Instance.ToggleConfirmationDisplay(loserController, buttons[(int)button], EXECUTE_ACTION.BACK_TO_MAIN);
                        }
                        break;
                }
            }
        }
	}

}
