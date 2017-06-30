using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class ConfirmationDisplayScript : MonoBehaviour 
{
    public enum BUTTON_OPTIONS
    {
        NO,
        YES
    };
    BUTTON_OPTIONS button = BUTTON_OPTIONS.NO;
    Button[] buttons = null;
    ListOfControllerActions controller = null;
    PointerEventData pointer;
    EventSystem eventSystem;
    SoundSystem soundSystem;
    AudioClip selectOption, executeOption;

    void Awake()
    {
        soundSystem = GameObject.FindWithTag("SoundSystem").GetComponent<SoundSystem>();
    }

    // Use this for initialization
    public void Reset(BUTTON_OPTIONS defaultButton = BUTTON_OPTIONS.NO)
    {
        eventSystem = GameObject.FindWithTag("EventSystem").GetComponent<EventSystem>();
        pointer = new PointerEventData(EventSystem.current); // pointer event for Execute

        if (selectOption == null)
            selectOption = AudioClipManager.GetInstance().GetAudioClip("SelectOption");
        if (executeOption == null)
            executeOption = AudioClipManager.GetInstance().GetAudioClip("ExecuteOption");

        button = defaultButton;
        if (buttons == null)
            buttons = GetComponentsInChildren<Button>(); 

        StartCoroutine(HighlightButton());
    }

    IEnumerator HighlightButton()
    {
        eventSystem.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        eventSystem.SetSelectedGameObject(buttons[(int)button].gameObject);
    }

    public void SetControllerToReadFrom(ListOfControllerActions newController)
    {
        if(controller != newController)
            controller = newController;
    }

    // Update is called once per frame
    void Update()
    {
        if (controller != null)
        {
            if (controller.getAxisActionBoolDown(ACTIONS.MOVE_RIGHT))
            {
                if (button != BUTTON_OPTIONS.NO)
                {
                    button = BUTTON_OPTIONS.NO;
                    buttons[(int)button].Select();
                    //soundSystem.PlayClip(AUDIO_TYPE.SOUND_EFFECTS, selectOption);
                    soundSystem.PlayClip(AUDIO_TYPE.SOUND_EFFECTS, AudioClipManager.GetInstance().GetAudioClip("SelectOption"));
                }
            }
            else if (controller.getAxisActionBoolDown(ACTIONS.MOVE_LEFT))
            {
                if (button != BUTTON_OPTIONS.YES)
                {
                    button = BUTTON_OPTIONS.YES;
                    buttons[(int)button].Select();
                    soundSystem.PlayClip(AUDIO_TYPE.SOUND_EFFECTS, selectOption);
                    //soundSystem.PlayClip(AUDIO_TYPE.SOUND_EFFECTS, AudioClipManager.GetInstance().GetAudioClip("SelectOption"));
                }
            }

            if (controller.getButtonAction(ACTIONS.SELECT))
            {
                soundSystem.PlayClip(AUDIO_TYPE.SOUND_EFFECTS, executeOption);
                ExecuteEvents.Execute(buttons[(int)button].gameObject, pointer, ExecuteEvents.submitHandler);
                //soundSystem.PlayClip(AUDIO_TYPE.SOUND_EFFECTS, AudioClipManager.GetInstance().GetAudioClip("ExecuteOption"));
                //buttons[(int)button].onClick.Invoke();
            }
        }
    }

}
