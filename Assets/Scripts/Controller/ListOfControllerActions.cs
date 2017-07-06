using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//[RequireComponent(typeof (PlayerControllerManager))]
public class ListOfControllerActions : MonoBehaviour {
    public Dictionary<ACTIONS, BUTTON_INPUT> buttonBinds = new Dictionary<ACTIONS, BUTTON_INPUT>();
    public Dictionary<ACTIONS, JOYSTICK_AXIS_INPUT> joystickBinds = new Dictionary<ACTIONS, JOYSTICK_AXIS_INPUT>();

    private PlayerControllerManager playerControllerManager;
    // Use this for initialization
    void Start () {
        playerControllerManager = GetComponent<PlayerControllerManager>();
        defaultButtonBinds();
        defaultAxisBinds();
        Debug.Log("Finished Initializing List of Controller Action for :" + playerControllerManager.playerID);
	}

    //*********************************************************************************************
    //getters
    //*********************************************************************************************
    public PlayerControllerManager GetControllerManager()
    {
        return playerControllerManager;
    }

    public bool getButtonAction(ACTIONS action)
    {
        return playerControllerManager.getIsKeyDown(buttonBinds[action]);
    }

    public bool getButtonHoldAction(ACTIONS action)
    {
        return playerControllerManager.getIsKeyDownHold(buttonBinds[action]);
    }

    public bool getButtonUpAction(ACTIONS action)
    {
        return playerControllerManager.getIsKeyDownUp(buttonBinds[action]);
    }

    public bool getIsKeyDown(BUTTON_INPUT button)
    {
        return playerControllerManager.getIsKeyDown(button);
    }

    public bool getIsKeyHold(BUTTON_INPUT button)
    {
        return playerControllerManager.getIsKeyDownHold(button);
    }

    public bool getIsKeyUp(BUTTON_INPUT button)
    {
        return playerControllerManager.getIsKeyDownUp(button);
    }

    public bool getAxisActionBool(ACTIONS action)
    {
        return playerControllerManager.getValueFromAxis(joystickBinds[action]).getBool();
    }
    public bool getAxisActionBoolDown(ACTIONS action)
    {
        return playerControllerManager.getAxisKeyDown(joystickBinds[action]);
    }

    public float getAxisActionFloat(ACTIONS action)
    {
        return playerControllerManager.getValueFromAxis(joystickBinds[action]).getFloat();
    }

    public bool getAxisBool(JOYSTICK_AXIS_INPUT axis)
    {
        return playerControllerManager.getValueFromAxis(axis).getBool();
    }

    public bool getAxisBoolDown(JOYSTICK_AXIS_INPUT axis)
    {
        return playerControllerManager.getAxisKeyDown(axis);
    }

    public float getAxisFloat(JOYSTICK_AXIS_INPUT axis)
    {
        return playerControllerManager.getValueFromAxis(axis).getFloat();
    }

    //*********************************************************************************************
    //bindz 
    //*********************************************************************************************
    public void defaultButtonBinds()
    {
        buttonBinds.Clear();
        buttonBinds.Add(ACTIONS.SKILL_ONE, BUTTON_INPUT.X);
        buttonBinds.Add(ACTIONS.SKILL_TWO, BUTTON_INPUT.Y);
        buttonBinds.Add(ACTIONS.SKILL_THREE, BUTTON_INPUT.B);
        buttonBinds.Add(ACTIONS.SKILL_FOUR, BUTTON_INPUT.A);
        buttonBinds.Add(ACTIONS.SKILL_REGEN, BUTTON_INPUT.R1);
        buttonBinds.Add(ACTIONS.SKILL_ULTIMATE, BUTTON_INPUT.R3);

        buttonBinds.Add(ACTIONS.START, BUTTON_INPUT.START);
        buttonBinds.Add(ACTIONS.BACK, BUTTON_INPUT.B);
        buttonBinds.Add(ACTIONS.SELECT, BUTTON_INPUT.A);
        buttonBinds.Add(ACTIONS.ACTIVATE, BUTTON_INPUT.L1);


        buttonBinds.Add(ACTIONS.PICK_CHARACTER, BUTTON_INPUT.A);
        buttonBinds.Add(ACTIONS.UNPICK_CHARACTER, BUTTON_INPUT.B);
        buttonBinds.Add(ACTIONS.SELECT_MAP, BUTTON_INPUT.A);
        buttonBinds.Add(ACTIONS.CANCEL_MAP_SELECT, BUTTON_INPUT.B);

        joystickBinds.Add(ACTIONS.MOVE_UP, JOYSTICK_AXIS_INPUT.DPAD_UP);
        joystickBinds.Add(ACTIONS.MOVE_DOWN, JOYSTICK_AXIS_INPUT.DPAD_DOWN);
        joystickBinds.Add(ACTIONS.MOVE_LEFT, JOYSTICK_AXIS_INPUT.DPAD_LEFT);
        joystickBinds.Add(ACTIONS.MOVE_RIGHT, JOYSTICK_AXIS_INPUT.DPAD_RIGHT);
        
    }

    public void bindButton(ACTIONS action, BUTTON_INPUT button)
    {
        buttonBinds.Remove(action);
        buttonBinds.Add(action, button);
    }

    public void deleteButtonBind(ACTIONS action)
    {
        buttonBinds.Remove(action);
    }


   

    public void defaultAxisBinds()
    {
        joystickBinds.Clear();
        joystickBinds.Add(ACTIONS.MOVE_UP, JOYSTICK_AXIS_INPUT.DPAD_UP);
        joystickBinds.Add(ACTIONS.MOVE_DOWN, JOYSTICK_AXIS_INPUT.DPAD_DOWN);
        joystickBinds.Add(ACTIONS.MOVE_LEFT, JOYSTICK_AXIS_INPUT.DPAD_LEFT);
        joystickBinds.Add(ACTIONS.MOVE_RIGHT, JOYSTICK_AXIS_INPUT.DPAD_RIGHT);

        //joystickBinds.Add(ACTIONS.SKILL_ULTIMATE, JOYSTICK_AXIS_INPUT.R2);

        joystickBinds.Add(ACTIONS.GAME_UP, JOYSTICK_AXIS_INPUT.DPAD_UP);
        joystickBinds.Add(ACTIONS.GAME_DOWN, JOYSTICK_AXIS_INPUT.DPAD_DOWN);
        joystickBinds.Add(ACTIONS.GAME_LEFT, JOYSTICK_AXIS_INPUT.DPAD_LEFT);
        joystickBinds.Add(ACTIONS.GAME_RIGHT, JOYSTICK_AXIS_INPUT.DPAD_RIGHT);

        joystickBinds.Add(ACTIONS.GAME_MOVE_UP, JOYSTICK_AXIS_INPUT.L3_UP);
        joystickBinds.Add(ACTIONS.GAME_MOVE_DOWN, JOYSTICK_AXIS_INPUT.L3_DOWN);
        joystickBinds.Add(ACTIONS.GAME_MOVE_LEFT, JOYSTICK_AXIS_INPUT.L3_LEFT);
        joystickBinds.Add(ACTIONS.GAME_MOVE_RIGHT, JOYSTICK_AXIS_INPUT.L3_RIGHT);
    }

    public void bindAxis(ACTIONS action, JOYSTICK_AXIS_INPUT axis)
    {
        joystickBinds.Remove(action);
        joystickBinds.Add(action, axis);
    }

    public void deleteAxisBind(ACTIONS action)
    {
        joystickBinds.Remove(action);
    }
}
