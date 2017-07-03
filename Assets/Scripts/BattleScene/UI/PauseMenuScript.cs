using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class PauseMenuScript : MonoBehaviour
{
    enum PAUSE_OPTIONS
    {
        RESUME,
        MOVE_LIST,
        SOUND_OPTION,
        CONTROLLER_SETTINGS,
        CHARACTER_SELECT,
        BACK_TO_MAIN
    };

    PAUSE_OPTIONS button = PAUSE_OPTIONS.RESUME;
    Image backgroundImage = null;
    Button[] buttons = null;
    Text textDisplay = null;
    ListOfControllerActions controller = null;
    PointerEventData pointer;
    EventSystem eventSystem;

    void Awake()
    {
        backgroundImage = transform.parent.GetComponent<Image>();
        eventSystem = GameObject.FindWithTag("EventSystem").GetComponent<EventSystem>();
        pointer = new PointerEventData(EventSystem.current); // pointer event for Execute
    }

    // Use this for initialization
    public void Pause(int playerID)
    {
        button = PAUSE_OPTIONS.RESUME;
        if (buttons == null)
            buttons = GetComponentsInChildren<Button>();

        controller = GameManager.Instance.GetPlayer(playerID).controller;

        backgroundImage.sprite = SpriteManager.GetInstance().GetSprite("PauseBG_Player" + (playerID+1));
        if (textDisplay == null)
            textDisplay = gameObject.transform.parent.GetComponentInChildren<Text>();
        textDisplay.text = "Player " + (playerID + 1) + " Paused";

        StartCoroutine(HighlightButton());
    }

    IEnumerator HighlightButton()
    {
        eventSystem.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        eventSystem.SetSelectedGameObject(buttons[(int)button].gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (!GlobalUI.Instance.GetConfirmationDisplayActive())
        {
            if (controller.getAxisActionBoolDown(ACTIONS.MOVE_DOWN))
            {
                if (button < PAUSE_OPTIONS.BACK_TO_MAIN)
                {
                    button++;
                    buttons[(int)button].Select();
                    SoundSystem.Instance.PlayClip(AUDIO_TYPE.SOUND_EFFECTS, AudioClipManager.GetInstance().GetAudioClip("SelectOption"));
                }
            }
            else if (controller.getAxisActionBoolDown(ACTIONS.MOVE_UP))
            {
                if (button > PAUSE_OPTIONS.RESUME)
                {
                    button--;
                    buttons[(int)button].Select();
                    SoundSystem.Instance.PlayClip(AUDIO_TYPE.SOUND_EFFECTS, AudioClipManager.GetInstance().GetAudioClip("SelectOption"));
                }
            }

            if (controller.getButtonAction(ACTIONS.SELECT))
            {
                SoundSystem.Instance.PlayClip(AUDIO_TYPE.SOUND_EFFECTS, AudioClipManager.GetInstance().GetAudioClip("ExecuteOption"));
                
                if(button == PAUSE_OPTIONS.CHARACTER_SELECT)
                {
                    GlobalUI.Instance.ToggleConfirmationDisplay(controller, buttons[(int)button], EXECUTE_ACTION.BACK_TO_CHARSELECT);
                }
                else if(button == PAUSE_OPTIONS.BACK_TO_MAIN)
                {
                    GlobalUI.Instance.ToggleConfirmationDisplay(controller, buttons[(int)button], EXECUTE_ACTION.BACK_TO_MAIN);
                }
                else
                {
                    ExecuteEvents.Execute(buttons[(int)button].gameObject, pointer, ExecuteEvents.submitHandler);
                }
            }
        }
    }

}
