using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class SideSelectScript : MonoBehaviour
{
    enum SIDES
    {
        RED_TEAM,
        BLUE_TEAM,
    };

    SIDES button = SIDES.RED_TEAM;
    Vector3 standardSize, SelectedSize;
    Button[] buttons = null;
    PointerEventData pointer;
    EventSystem eventSystem;
    ListOfControllerActions controller = null;
    bool selectedTeam = false;

    FlipObjectScript flipText;

    public bool SelectedTeam() { return selectedTeam; }

    public void Reset() { selectedTeam = false; }

    void Start()
    {
        selectedTeam = false;
        eventSystem = GameObject.FindWithTag("EventSystem").GetComponent<EventSystem>();
        pointer = new PointerEventData(EventSystem.current); // pointer event for Execute

        flipText = GameObject.Find("FlipArrow").GetComponent<FlipObjectScript>();
        button = SIDES.RED_TEAM;
        if (buttons == null)
            buttons = GetComponentsInChildren<Button>();

        controller = GameManager.Instance.GetMasterPlayerData().controller;
        standardSize = new Vector3(1, 1, 1);
        SelectedSize = new Vector3(1.2f, 1.2f, 1.0f);
        buttons[(int)button].transform.localScale = SelectedSize;
    }
    
    // Update is called once per frame
    void Update()
    {
        if (!GlobalUI.Instance.GetConfirmationDisplayActive())
        {
            if (controller.getAxisActionBoolDown(ACTIONS.MOVE_RIGHT))
            {
                if (button < SIDES.BLUE_TEAM)
                {
                    buttons[(int)button].transform.localScale = standardSize;
                    button++;
                    buttons[(int)button].Select();
                    buttons[(int)button].transform.localScale = SelectedSize;
                    flipText.Flip();
                    SoundSystem.Instance.PlayClip(AUDIO_TYPE.SOUND_EFFECTS, AudioClipManager.GetInstance().GetAudioClip("SelectOption"));
                }
            }
            else if (controller.getAxisActionBoolDown(ACTIONS.MOVE_LEFT))
            {
                if (button > SIDES.RED_TEAM)
                {
                    buttons[(int)button].transform.localScale = standardSize;
                    button--;
                    buttons[(int)button].Select();
                    buttons[(int)button].transform.localScale = SelectedSize;
                    flipText.Flip();
                    SoundSystem.Instance.PlayClip(AUDIO_TYPE.SOUND_EFFECTS, AudioClipManager.GetInstance().GetAudioClip("SelectOption"));
                }
            }

            if (controller.getButtonAction(ACTIONS.SELECT))
            {
                SoundSystem.Instance.PlayClip(AUDIO_TYPE.SOUND_EFFECTS, AudioClipManager.GetInstance().GetAudioClip("ExecuteOption"));
                ExecuteEvents.Execute(buttons[(int)button].gameObject, pointer, ExecuteEvents.submitHandler);
                selectedTeam = true;
            }
            else if (controller.getButtonAction(ACTIONS.BACK))
            {
                GlobalUI.Instance.ToggleConfirmationDisplay(controller, GameObject.FindWithTag("ChangeSceneButton").GetComponent<Button>(), EXECUTE_ACTION.BACK_TO_MAIN);
            }
        }
    }

    
}
