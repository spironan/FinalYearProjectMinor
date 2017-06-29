using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class BackToMainScript : MonoBehaviour 
{
    enum BUTTON_OPTIONS
    {
        NO,
        YES
    };

    BUTTON_OPTIONS button = BUTTON_OPTIONS.NO;
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
    public void Reset()
    {
        button = BUTTON_OPTIONS.NO;
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
                }
            }
            else if (controller.getAxisActionBoolDown(ACTIONS.MOVE_LEFT))
            {

                if (button != BUTTON_OPTIONS.YES)
                {
                    button = BUTTON_OPTIONS.YES;
                    buttons[(int)button].Select();
                }
            }

            if (controller.getButtonAction(ACTIONS.SELECT))
            {
                ExecuteEvents.Execute(buttons[(int)button].gameObject, pointer, ExecuteEvents.submitHandler);
                //buttons[(int)button].onClick.Invoke();
            }
        }
    }
}
