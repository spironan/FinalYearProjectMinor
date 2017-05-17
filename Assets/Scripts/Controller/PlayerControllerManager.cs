using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerControllerManager : MonoBehaviour {

    
    public ControllerInput XBox360;
    public ControllerInput PS4;

    public ControllerInput currController;
    public PLAYER playerID;

    public void Awake()
    {
        currController = XBox360;
    }

    public void init(PLAYER number)
    {
        playerID = number;
        detectController();
    }

    public void detectController()
    {
        if (Input.GetJoystickNames().Length > 0)
        {
            if (Input.GetJoystickNames()[(int)playerID].Contains("360") || Input.GetJoystickNames()[(int)playerID].Contains("GamepadF310"))
                currController = XBox360;
            else if (Input.GetJoystickNames()[(int)playerID].Contains("Wireless Controller"))
                currController = PS4;
            else
                currController = XBox360;
        }
    }


    public bool getIsKeyDown(BUTTON_INPUT input)
    {
        return currController.CheckForKeyPress(input, playerID);
    } 

    public FloatAndBool getValueFromAxis(JOYSTICK_AXIS_INPUT input)
    {
        return currController.CheckForJoyStickAxis(input, playerID);
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