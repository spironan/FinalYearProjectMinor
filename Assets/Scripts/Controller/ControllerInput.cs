using UnityEngine;
using System.Collections;

public class ControllerInput : MonoBehaviour {

    //protected int controllerNumber;
    protected FloatAndBool floatAndBool = new FloatAndBool();


    public virtual bool CheckForKeyPress(BUTTON_INPUT keyNumber, PLAYER player)
    {
        return false;
    }
    public virtual FloatAndBool CheckForJoyStickAxis(JOYSTICK_AXIS_INPUT joyStickNumber, PLAYER player)
    {
        return null;
    }
}
