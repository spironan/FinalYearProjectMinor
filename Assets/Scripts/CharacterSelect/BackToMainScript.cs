using UnityEngine;
using UnityEngine.UI;
using System.Collections;

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
    bool reset = false;

    // Use this for initialization
    public void Reset()
    {
        button = BUTTON_OPTIONS.NO;
        if (buttons == null)
            buttons = GetComponentsInChildren<Button>();
        reset = true;
    }

    public void SetControllerToReadFrom(ListOfControllerActions newController)
    {
        controller = newController;
    }

    // Update is called once per frame
    void Update()
    {
        if (reset)
        {
            buttons[(int)button].Select();
            reset = false;
        }
        if (controller != null)
        {
            if (controller.getAxisActionBoolDown(ACTIONS.MOVE_RIGHT))
            {
                if (button != BUTTON_OPTIONS.NO)
                {
                    button = BUTTON_OPTIONS.NO;
                    buttons[(int)button].Select();
                }

                //if (button < BUTTON_OPTIONS.NO)
                //{
                //    button++;
                //    buttons[(int)button].Select();
                //}
            }
            else if (controller.getAxisActionBoolDown(ACTIONS.MOVE_LEFT))
            {

                if (button != BUTTON_OPTIONS.YES)
                {
                    button = BUTTON_OPTIONS.YES;
                    buttons[(int)button].Select();
                }

                //if (button > BUTTON_OPTIONS.REMATCH)
                //{
                //    button--;
                //    buttons[(int)button].Select();
                //}
            }

            if (controller.getButtonAction(ACTIONS.SELECT))
                buttons[(int)button].onClick.Invoke();
        }
    }
}
