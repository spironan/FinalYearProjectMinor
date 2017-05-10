using UnityEngine;
using System.Collections;

public class ControllerInput : MonoBehaviour {

    //protected int controllerNumber;
    protected FloatAndBool floatAndBool = new FloatAndBool();


    public virtual bool CheckForKeyPress(BUTTON_INPUT keyNumber, int controllerNumber)
    {
        return false;
    }
    public virtual FloatAndBool CheckForJoyStickAxis(JOYSTICK_AXIS_INPUT joyStickNumber, int controllerNumber)
    {
        return null;
    }
}
