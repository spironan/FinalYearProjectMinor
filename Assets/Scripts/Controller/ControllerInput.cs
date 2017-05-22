using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ControllerInput : MonoBehaviour {

    //protected int controllerNumber;
    protected FloatAndBool floatAndBool = new FloatAndBool();
    protected bool keyDown = false;
    protected bool keyDownHold = false;
    protected bool keyUp;
    protected string nameOfKey;
    protected Dictionary<JOYSTICK_AXIS_INPUT, bool> isJoystickKeyPressed = new Dictionary<JOYSTICK_AXIS_INPUT, bool>();
    //protected Dictionary<BUTTON_INPUT, XBOX360> ps4ToXbox360 = new Dictionary<BUTTON_INPUT, XBOX360>();
    //protected Dictionary<BUTTON_INPUT, KeyCode> buttonToKeyCode;
    //protected Dictionary<KeyCode, BUTTON_INPUT> keyCodeToButton;


    public virtual void Start()
    {
        //debug.log(1);
    }

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

    public virtual bool CheckForJoyStickAxisDown(JOYSTICK_AXIS_INPUT joyStickNumber, PLAYER player)
    {
        return false;
    }

    public virtual void updateAndReturnIfkeyIsDown(JOYSTICK_AXIS_INPUT joyStickNumber,string rawKeyName, float offset = 0,bool positiveAxis = true)
    {

    }

    public FloatAndBool getFloatBool()
    {
        return floatAndBool;
    }

    public void setFloatBool(float f, bool b)
    {
        floatAndBool.setFloatAndBool(f,b);
    }

}
