﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//[RequireComponent(typeof(ListOfControllerActions))]
public class PlayerControllerManager : MonoBehaviour 
{
    public ControllerInput XBox360;
    public ControllerInput PS4;

    public ControllerInput currController;
    public PLAYER playerID;

    public bool disableControls;

    //private string nameOfController;
    //private int numberOfControllers;
    private string[] listOfConnectedController;

    public void Start()
    {
        disableControls = false;
        currController = XBox360;
        if (detectController())
            listOfConnectedController = Input.GetJoystickNames();

        currController.Start();
        Debug.Log("Finished Initializing Player Controller for :" + playerID);
        //numberOfControllers = Input.GetJoystickNames().Length;
        //if (Input.GetJoystickNames().Length > (int)playerID)
        //{
        //    nameOfController = Input.GetJoystickNames()[(int)playerID];
        //}
        //Debug.Log(Input.GetJoystickNames().Length);
    }

    public void Update()
    {
     
        if (listOfConnectedController != Input.GetJoystickNames())
        {
            if(detectController())
                listOfConnectedController = Input.GetJoystickNames();
        }

        //Debug.Log(Input.GetJoystickNames().Length);
        //Debug.Log(Input.GetJoystickNames()[1]);
    }

    public void init(PLAYER number)
    {
        playerID = number;
        detectController();
    }

    public void DisableController()
    {
        disableControls = true;
    }
    public void EnableController()
    {
        disableControls = false;
    }

    public bool detectController()
    {
        if (Input.GetJoystickNames().Length > 0 && Input.GetJoystickNames().Length > (int)playerID)
        {
            //Debug.Log(Input.GetJoystickNames());
            if (Input.GetJoystickNames()[(int)playerID].Contains("360") || Input.GetJoystickNames()[(int)playerID].Contains("GamepadF310"))
            {
                currController = XBox360;
                return true;
            }
            else if (Input.GetJoystickNames()[(int)playerID].Contains("Wireless Controller"))
            {
                currController = PS4;
                return true;
            }
            else
            {
                currController = XBox360;
                return false;
            }
        }
        return false;
    }


    public bool getIsKeyDown(BUTTON_INPUT input)
    {
        if (!disableControls)
            return currController.CheckForKeyPress(input, playerID);
        else
            return false;
    }

    public bool getIsKeyDownHold(BUTTON_INPUT input)
    {
        if (!disableControls)
            return currController.CheckForKeyPressHold(input, playerID);
        else
            return false;
    }

    public bool getIsKeyDownUp(BUTTON_INPUT input)
    {
        if (!disableControls)
            return currController.CheckForKeyPressUp(input, playerID);
        else
            return false;
    }


    public FloatAndBool getValueFromAxis(JOYSTICK_AXIS_INPUT input)
    {
        if (!disableControls)
            return currController.CheckForJoyStickAxis(input, playerID);
        else
        {
            currController.setFloatBool(0, false);
            return currController.getFloatBool();
        }
            
    }
    public bool getAxisKeyDown(JOYSTICK_AXIS_INPUT input)
    {
        if (!disableControls)
            return currController.CheckForJoyStickAxisDown(input, playerID);
        else
            return false;
    }

    public bool isControllerDisabled()
    {
        return disableControls;
    }

    public string nameOfController()
    {
        return currController.getControllerName();
    }
}

//public Dictionary<int, int> controllerAssigned = new Dictionary<int, int>();// key is going to be player, value is gonna be controller number // public for now to test
//public Dictionary<int, ControllerInput> orderOfController = new Dictionary<int, ControllerInput>();// key is going to be the number assigned to the controller is connected, value is gonna be the controller type

//// Use this for initialization
//void Awake () {
//    controllerAssigned.Add(1,0);
//    controllerAssigned.Add(2, 1);
//    //controllerAssigned[2] = 1;
//    orderOfController.Add(0,PS4);
//    orderOfController.Add(1, PS4);
//    Debug.Log("HI");
//}

// Update is called once per frame
//void Update () {

//       int index = 0;
//       foreach (string joystick in Input.GetJoystickNames())
//       {
//           if (joystick.Contains("360") || joystick.Contains("GamepadF310"))
//               orderOfController[index] = XBox360;
//           else if (joystick.Contains("Wireless Controller"))
//               orderOfController[index] = PS4;

//           index += 1;
//       }
//   }