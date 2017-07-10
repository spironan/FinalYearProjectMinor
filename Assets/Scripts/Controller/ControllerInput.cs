using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ControllerInput : MonoBehaviour {

    //protected int controllerNumber;
    protected string controller_name = "";
    protected FloatAndBool floatAndBool = new FloatAndBool();
    protected bool keyDown = false;
    protected bool keyDownHold = false;
    protected bool keyUp;
    protected string nameOfKey;
    protected string nameOfJoystick;
    protected float valueFromAxis;
    protected Dictionary<JOYSTICK_AXIS_INPUT, bool> isJoystickKeyPressed_player1 = new Dictionary<JOYSTICK_AXIS_INPUT, bool>();
    protected Dictionary<JOYSTICK_AXIS_INPUT, bool> isJoystickKeyPressed_player2 = new Dictionary<JOYSTICK_AXIS_INPUT, bool>();
    protected Dictionary<JOYSTICK_AXIS_INPUT, string> joystickEnumToString = new Dictionary<JOYSTICK_AXIS_INPUT, string>();
    protected Dictionary<PLAYER, Dictionary<JOYSTICK_AXIS_INPUT, bool>> isJoystickKeyPressedWithPlayer = new Dictionary<PLAYER, Dictionary<JOYSTICK_AXIS_INPUT, bool>>();
    //protected Dictionary<JOYSTICK_AXIS_INPUT, bool> isJoystickKeyPressed = new Dictionary<JOYSTICK_AXIS_INPUT, bool>();
    //protected Dictionary<BUTTON_INPUT, XBOX360> ps4ToXbox360 = new Dictionary<BUTTON_INPUT, XBOX360>();
    //protected Dictionary<BUTTON_INPUT, KeyCode> buttonToKeyCode;
    //protected Dictionary<KeyCode, BUTTON_INPUT> keyCodeToButton;


    public virtual void Start()
    {
        //debug.log(1);
        isJoystickKeyPressedWithPlayer.Clear();
        isJoystickKeyPressedWithPlayer.Add(PLAYER.PLAYER_ONE, isJoystickKeyPressed_player1);
        isJoystickKeyPressedWithPlayer.Add(PLAYER.PLAYER_TWO, isJoystickKeyPressed_player2);
        joystickEnumToString.Clear();
        joystickEnumToString.Add(JOYSTICK_AXIS_INPUT.L2, "L2_button");
        joystickEnumToString.Add(JOYSTICK_AXIS_INPUT.R2, "R2_button");
        joystickEnumToString.Add(JOYSTICK_AXIS_INPUT.L3_UP, "leftStick_Y");
        joystickEnumToString.Add(JOYSTICK_AXIS_INPUT.L3_DOWN, "leftStick_Y");
        joystickEnumToString.Add(JOYSTICK_AXIS_INPUT.L3_LEFT, "leftStick_X");
        joystickEnumToString.Add(JOYSTICK_AXIS_INPUT.L3_RIGHT, "leftStick_X");
        joystickEnumToString.Add(JOYSTICK_AXIS_INPUT.L3_Y, "leftStick_Y");
        joystickEnumToString.Add(JOYSTICK_AXIS_INPUT.L3_X, "leftStick_X");
        joystickEnumToString.Add(JOYSTICK_AXIS_INPUT.R3_UP, "rightStick_Y");
        joystickEnumToString.Add(JOYSTICK_AXIS_INPUT.R3_DOWN, "rightStick_Y");
        joystickEnumToString.Add(JOYSTICK_AXIS_INPUT.R3_LEFT, "rightStick_X");
        joystickEnumToString.Add(JOYSTICK_AXIS_INPUT.R3_RIGHT, "rightStick_X");
        joystickEnumToString.Add(JOYSTICK_AXIS_INPUT.R3_Y, "rightStick_Y");
        joystickEnumToString.Add(JOYSTICK_AXIS_INPUT.R3_X, "rightStick_X");
        joystickEnumToString.Add(JOYSTICK_AXIS_INPUT.DPAD_UP, "DPad_Y");
        joystickEnumToString.Add(JOYSTICK_AXIS_INPUT.DPAD_DOWN, "DPad_Y");
        joystickEnumToString.Add(JOYSTICK_AXIS_INPUT.DPAD_LEFT, "DPad_X");
        joystickEnumToString.Add(JOYSTICK_AXIS_INPUT.DPAD_RIGHT, "DPad_X");
        joystickEnumToString.Add(JOYSTICK_AXIS_INPUT.DPAD_Y, "DPad_Y");
        joystickEnumToString.Add(JOYSTICK_AXIS_INPUT.DPAD_X, "DPad_X");
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

    public virtual void updateAndReturnIfkeyIsDown(JOYSTICK_AXIS_INPUT joyStickNumber, PLAYER player_num, float valueFromAxis, float offset = 0,bool positiveAxis = true)
    {
        if (!isJoystickKeyPressedWithPlayer[player_num].ContainsKey(joyStickNumber))
        {
            isJoystickKeyPressedWithPlayer[player_num].Add(joyStickNumber, false);
        }
        if (positiveAxis)
        {
            if (valueFromAxis + offset > 0.9f && !isJoystickKeyPressedWithPlayer[player_num][joyStickNumber])
            {
                keyDown = true;
                isJoystickKeyPressedWithPlayer[player_num][joyStickNumber] = true;
            }
            else if (valueFromAxis + offset > 0.9f && isJoystickKeyPressedWithPlayer[player_num][joyStickNumber])
            {
                keyDown = false;
            }
            else
            {
                keyDown = false;
                isJoystickKeyPressedWithPlayer[player_num][joyStickNumber] = false;
            }
        }
        else
        {
            if (valueFromAxis + offset < -0.9f && !isJoystickKeyPressedWithPlayer[player_num][joyStickNumber])
            {
                keyDown = true;
                isJoystickKeyPressedWithPlayer[player_num][joyStickNumber] = true;
            }
            else if (valueFromAxis + offset < -0.9f && isJoystickKeyPressedWithPlayer[player_num][joyStickNumber])
            {
                keyDown = false;
            }
            else
            {
                keyDown = false;
                isJoystickKeyPressedWithPlayer[player_num][joyStickNumber] = false;
            }
        }
        floatAndBool.setFloatAndBool(0, keyDown);

    }

    public FloatAndBool getFloatBool()
    {
        return floatAndBool;
    }

    public void setFloatBool(float f, bool b)
    {
        floatAndBool.setFloatAndBool(f,b);
    }

    public string getControllerName()
    {
        return controller_name;
    }
}
