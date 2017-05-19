using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ControllerInput : MonoBehaviour {

    //protected int controllerNumber;
    protected FloatAndBool floatAndBool = new FloatAndBool();
    //protected Dictionary<BUTTON_INPUT, KeyCode> buttonToKeyCode;
    //protected Dictionary<KeyCode, BUTTON_INPUT> keyCodeToButton;
   

    //public virtual void Start()
    //{

    //}

    //public virtual void 
    public virtual bool CheckForKeyPress(BUTTON_INPUT keyNumber, PLAYER player)
    {
        return false;
    }

    public virtual bool CheckForKeyPressHold(BUTTON_INPUT keyNumber, PLAYER player)
    {
        return false;
    }

    public virtual bool CheckForKeyPressUp(BUTTON_INPUT keyNumber, PLAYER player)
    {
        return false;
    }

    public virtual FloatAndBool CheckForJoyStickAxis(JOYSTICK_AXIS_INPUT joyStickNumber, PLAYER player)
    {
        return null;
    }
}
