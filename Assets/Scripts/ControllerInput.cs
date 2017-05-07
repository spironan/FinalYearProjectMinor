using UnityEngine;
using System.Collections;

public class ControllerInput : MonoBehaviour {

    //protected int controllerNumber;

    public virtual bool CheckForKeyPress(BUTTON_INPUT keyNumber, int controllerNumber)
    {
        return false;
    }
    public virtual float CheckForJoyStickAxis(JOYSTICK_AXIS_INPUT joyStickNumber, int controllerNumber)
    {
        return 0;
    }
}
