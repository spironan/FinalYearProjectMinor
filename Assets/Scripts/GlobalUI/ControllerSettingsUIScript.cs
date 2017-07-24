using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;


public class ControllerSettingsUIScript : MonoBehaviour {
    enum CONTROLLER_OPTIONS
    {
        SKILL1_INPUT,
        SKILL2_INPUT,
        SKILL3_INPUT,
        SKILL4_INPUT,
        ACTIVE_INPUT,
        CHARGE_INPUT,
        MOVEMENT_INPUT,
        CONFIRM_EXIT,
    };

    public PLAYER playerID;
    CONTROLLER_OPTIONS button = CONTROLLER_OPTIONS.SKILL1_INPUT;
    Button[] buttons = null;
    ListOfControllerActions controller = null;
    PointerEventData pointer;
    EventSystem eventSystem;

    void Awake()
    {
        eventSystem = GameObject.FindWithTag("EventSystem").GetComponent<EventSystem>();
        pointer = new PointerEventData(EventSystem.current); // pointer event for Execute
    }
    

    // Use this for initialization
    public void OpenSettings()
    {
        button = CONTROLLER_OPTIONS.SKILL1_INPUT;
        if (buttons == null)
            buttons = GetComponentsInChildren<Button>();

        controller = GameManager.Instance.GetPlayer(playerID).controller;
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
                if (button < CONTROLLER_OPTIONS.CONFIRM_EXIT)
                {
                    button++;
                    buttons[(int)button].Select();
                    SoundSystem.Instance.PlayClip(AUDIO_TYPE.SOUND_EFFECTS, AudioClipManager.GetInstance().GetAudioClip("SelectOption"));
                }
            }
            else if (controller.getAxisActionBoolDown(ACTIONS.MOVE_UP))
            {
                if (button > CONTROLLER_OPTIONS.SKILL1_INPUT)
                {
                    button--;
                    buttons[(int)button].Select();
                    SoundSystem.Instance.PlayClip(AUDIO_TYPE.SOUND_EFFECTS, AudioClipManager.GetInstance().GetAudioClip("SelectOption"));
                }
            }

            if (controller.getButtonAction(ACTIONS.SELECT))
            {
                SoundSystem.Instance.PlayClip(AUDIO_TYPE.SOUND_EFFECTS, AudioClipManager.GetInstance().GetAudioClip("ExecuteOption"));

                ExecuteEvents.Execute(buttons[(int)button].gameObject, pointer, ExecuteEvents.submitHandler);
            }
        }
    }

}
