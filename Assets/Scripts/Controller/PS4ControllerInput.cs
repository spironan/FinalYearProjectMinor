using UnityEngine;
using System.Collections;

public class PS4ControllerInput : ControllerInput {
    //public int playernum = 
    public override void Start()
    {
        base.Start();
        controller_name = "PS4";
    }

    public override bool CheckForKeyPress(BUTTON_INPUT keyNumber, PLAYER player)
    {
        if(keyNumber < BUTTON_INPUT.TOTAL && player < PLAYER.MAX_PLAYERS)
        {
            string button = "joystick " + ((int)player+1) + " button " + (int)keyNumber;
            return Input.GetKeyDown(button);
        }
        return false;
        //if (player == PLAYER.PLAYER_ONE)
        //{
        //    switch (keyNumber)
        //    {
        //        case BUTTON_INPUT.A:
        //            return Input.GetButtonDown("A_button_PS4");
        //        case BUTTON_INPUT.B:
        //            return Input.GetButtonDown("B_button_PS4");
        //        case BUTTON_INPUT.X:
        //            return Input.GetButtonDown("X_button_PS4");
        //        case BUTTON_INPUT.Y:
        //            return Input.GetButtonDown("Y_button_PS4");
        //        case BUTTON_INPUT.L1:
        //            return Input.GetButtonDown("L1_button_PS4");
        //        case BUTTON_INPUT.R1:
        //            return Input.GetButtonDown("R1_button_PS4");
        //        case BUTTON_INPUT.BACK:
        //            return Input.GetButtonDown("back_button_PS4");
        //        case BUTTON_INPUT.START:
        //            return Input.GetButtonDown("start_button_PS4");
        //        case BUTTON_INPUT.L3:
        //            return Input.GetButtonDown("L3_button_PS4");
        //        case BUTTON_INPUT.R3:
        //            return Input.GetButtonDown("R3_button_PS4");


        //    }
            
        //}
        //else if (player == PLAYER.PLAYER_TWO)
        //{
        //    switch (keyNumber)
        //    {
        //        case BUTTON_INPUT.A:
        //            return Input.GetButtonDown("A_button_PS4_player2");
        //        case BUTTON_INPUT.B:
        //            return Input.GetButtonDown("B_button_PS4_player2");
        //        case BUTTON_INPUT.X:
        //            return Input.GetButtonDown("X_button_PS4_player2");
        //        case BUTTON_INPUT.Y:
        //            return Input.GetButtonDown("Y_button_PS4_player2");
        //        case BUTTON_INPUT.L1:
        //            return Input.GetButtonDown("L1_button_PS4_player2");
        //        case BUTTON_INPUT.R1:
        //            return Input.GetButtonDown("R1_button_PS4_player2");
        //        case BUTTON_INPUT.BACK:
        //            return Input.GetButtonDown("back_button_PS4_player2");
        //        case BUTTON_INPUT.START:
        //            return Input.GetButtonDown("start_button_PS4_player2");
        //        case BUTTON_INPUT.L3:
        //            return Input.GetButtonDown("L3_button_PS4_player2");
        //        case BUTTON_INPUT.R3:
        //            return Input.GetButtonDown("R3_button_PS4_player2");


        //    }
        //}
        //return false;
    }

    public override bool CheckForKeyPressHold(BUTTON_INPUT keyNumber, PLAYER player)
    {
        if (keyNumber < BUTTON_INPUT.TOTAL && player < PLAYER.MAX_PLAYERS)
        {
            string button = "joystick " + ((int)player + 1) + " button " + (int)keyNumber;
            return Input.GetKey(button);
        }
        return false;
        //if (player == PLAYER.PLAYER_ONE)
        //{
        //    switch (keyNumber)
        //    {
        //        case BUTTON_INPUT.A:
        //            return Input.GetButton("A_button_PS4");
        //        case BUTTON_INPUT.B:
        //            return Input.GetButton("B_button_PS4");
        //        case BUTTON_INPUT.X:
        //            return Input.GetButton("X_button_PS4");
        //        case BUTTON_INPUT.Y:
        //            return Input.GetButton("Y_button_PS4");
        //        case BUTTON_INPUT.L1:
        //            return Input.GetButton("L1_button_PS4");
        //        case BUTTON_INPUT.R1:
        //            return Input.GetButton("R1_button_PS4");
        //        case BUTTON_INPUT.BACK:
        //            return Input.GetButton("back_button_PS4");
        //        case BUTTON_INPUT.START:
        //            return Input.GetButton("start_button_PS4");
        //        case BUTTON_INPUT.L3:
        //            return Input.GetButton("L3_button_PS4");
        //        case BUTTON_INPUT.R3:
        //            return Input.GetButton("R3_button_PS4");


        //    }

        //}
        //else if (player == PLAYER.PLAYER_TWO)
        //{
        //    switch (keyNumber)
        //    {
        //        case BUTTON_INPUT.A:
        //            return Input.GetButton("A_button_PS4_player2");
        //        case BUTTON_INPUT.B:
        //            return Input.GetButton("B_button_PS4_player2");
        //        case BUTTON_INPUT.X:
        //            return Input.GetButton("X_button_PS4_player2");
        //        case BUTTON_INPUT.Y:
        //            return Input.GetButton("Y_button_PS4_player2");
        //        case BUTTON_INPUT.L1:
        //            return Input.GetButton("L1_button_PS4_player2");
        //        case BUTTON_INPUT.R1:
        //            return Input.GetButton("R1_button_PS4_player2");
        //        case BUTTON_INPUT.BACK:
        //            return Input.GetButton("back_button_PS4_player2");
        //        case BUTTON_INPUT.START:
        //            return Input.GetButton("start_button_PS4_player2");
        //        case BUTTON_INPUT.L3:
        //            return Input.GetButton("L3_button_PS4_player2");
        //        case BUTTON_INPUT.R3:
        //            return Input.GetButton("R3_button_PS4_player2");


        //    }
        //}
        //return false;
    }

    public override bool CheckForKeyPressUp(BUTTON_INPUT keyNumber, PLAYER player)
    {
        if (keyNumber < BUTTON_INPUT.TOTAL && player < PLAYER.MAX_PLAYERS)
        {
            string button = "joystick " + ((int)player + 1) + " button " + (int)keyNumber;
            return Input.GetKeyUp(button);
        }
        return false;
        //if (player == PLAYER.PLAYER_ONE)
        //{
        //    switch (keyNumber)
        //    {
        //        case BUTTON_INPUT.A:
        //            return Input.GetButtonUp("A_button_PS4");
        //        case BUTTON_INPUT.B:
        //            return Input.GetButtonUp("B_button_PS4");
        //        case BUTTON_INPUT.X:
        //            return Input.GetButtonUp("X_button_PS4");
        //        case BUTTON_INPUT.Y:
        //            return Input.GetButtonUp("Y_button_PS4");
        //        case BUTTON_INPUT.L1:
        //            return Input.GetButtonUp("L1_button_PS4");
        //        case BUTTON_INPUT.R1:
        //            return Input.GetButtonUp("R1_button_PS4");
        //        case BUTTON_INPUT.BACK:
        //            return Input.GetButtonUp("back_button_PS4");
        //        case BUTTON_INPUT.START:
        //            return Input.GetButtonUp("start_button_PS4");
        //        case BUTTON_INPUT.L3:
        //            return Input.GetButtonUp("L3_button_PS4");
        //        case BUTTON_INPUT.R3:
        //            return Input.GetButtonUp("R3_button_PS4");


        //    }

        //}
        //else if (player == PLAYER.PLAYER_TWO)
        //{
        //    switch (keyNumber)
        //    {
        //        case BUTTON_INPUT.A:
        //            return Input.GetButtonUp("A_button_PS4_player2");
        //        case BUTTON_INPUT.B:
        //            return Input.GetButtonUp("B_button_PS4_player2");
        //        case BUTTON_INPUT.X:
        //            return Input.GetButtonUp("X_button_PS4_player2");
        //        case BUTTON_INPUT.Y:
        //            return Input.GetButtonUp("Y_button_PS4_player2");
        //        case BUTTON_INPUT.L1:
        //            return Input.GetButtonUp("L1_button_PS4_player2");
        //        case BUTTON_INPUT.R1:
        //            return Input.GetButtonUp("R1_button_PS4_player2");
        //        case BUTTON_INPUT.BACK:
        //            return Input.GetButtonUp("back_button_PS4_player2");
        //        case BUTTON_INPUT.START:
        //            return Input.GetButtonUp("start_button_PS4_player2");
        //        case BUTTON_INPUT.L3:
        //            return Input.GetButtonUp("L3_button_PS4_player2");
        //        case BUTTON_INPUT.R3:
        //            return Input.GetButtonUp("R3_button_PS4_player2");


        //    }
        //}
        //return false;
    }

    public override FloatAndBool CheckForJoyStickAxis(JOYSTICK_AXIS_INPUT joyStickNumber, PLAYER player)
    {
        nameOfJoystick = joystickEnumToString[joyStickNumber];
        nameOfJoystick += "_PS4";
        if (player == PLAYER.PLAYER_TWO)
            nameOfJoystick += "_player2";

        valueFromAxis = Input.GetAxis(nameOfJoystick);
        switch (joyStickNumber)
        {
            case JOYSTICK_AXIS_INPUT.L2:
                floatAndBool.setFloatAndBool(valueFromAxis + 1, valueFromAxis + 1 > 0);
                break;
            case JOYSTICK_AXIS_INPUT.R2:
                floatAndBool.setFloatAndBool(valueFromAxis + 1, valueFromAxis + 1 > 0);
                break;
            case JOYSTICK_AXIS_INPUT.L3_UP:
                floatAndBool.setFloatAndBool(valueFromAxis, valueFromAxis > 0);
                break;
            case JOYSTICK_AXIS_INPUT.L3_DOWN:
                floatAndBool.setFloatAndBool(valueFromAxis, valueFromAxis < 0);
                break;
            case JOYSTICK_AXIS_INPUT.L3_LEFT:
                floatAndBool.setFloatAndBool(valueFromAxis, valueFromAxis < 0);
                break;
            case JOYSTICK_AXIS_INPUT.L3_RIGHT:
                floatAndBool.setFloatAndBool(valueFromAxis, valueFromAxis > 0);
                break;
            case JOYSTICK_AXIS_INPUT.L3_Y:
                floatAndBool.setFloatAndBool(valueFromAxis, false);
                break;
            case JOYSTICK_AXIS_INPUT.L3_X:
                floatAndBool.setFloatAndBool(valueFromAxis, false);
                break;
            case JOYSTICK_AXIS_INPUT.R3_UP:
                floatAndBool.setFloatAndBool(valueFromAxis, valueFromAxis > 0);
                break;
            case JOYSTICK_AXIS_INPUT.R3_DOWN:
                floatAndBool.setFloatAndBool(valueFromAxis, valueFromAxis < 0);
                break;
            case JOYSTICK_AXIS_INPUT.R3_LEFT:
                floatAndBool.setFloatAndBool(valueFromAxis, valueFromAxis < 0);
                break;
            case JOYSTICK_AXIS_INPUT.R3_RIGHT:
                floatAndBool.setFloatAndBool(valueFromAxis, valueFromAxis > 0);
                break;
            case JOYSTICK_AXIS_INPUT.R3_Y:
                floatAndBool.setFloatAndBool(valueFromAxis, false);
                break;
            case JOYSTICK_AXIS_INPUT.R3_X:
                floatAndBool.setFloatAndBool(valueFromAxis, false);
                break;
            case JOYSTICK_AXIS_INPUT.DPAD_UP:
                floatAndBool.setFloatAndBool(valueFromAxis, valueFromAxis > 0);
                break;
            case JOYSTICK_AXIS_INPUT.DPAD_DOWN:
                floatAndBool.setFloatAndBool(valueFromAxis, valueFromAxis < 0);
                break;
            case JOYSTICK_AXIS_INPUT.DPAD_LEFT:
                floatAndBool.setFloatAndBool(valueFromAxis, valueFromAxis < 0);
                break;
            case JOYSTICK_AXIS_INPUT.DPAD_RIGHT:
                floatAndBool.setFloatAndBool(valueFromAxis, valueFromAxis > 0);
                break;
            case JOYSTICK_AXIS_INPUT.DPAD_Y:
                floatAndBool.setFloatAndBool(valueFromAxis, false);
                break;
            case JOYSTICK_AXIS_INPUT.DPAD_X:
                floatAndBool.setFloatAndBool(valueFromAxis, false);
                break;
            default:
                floatAndBool.setFloatAndBool(0, false);
                break;

        }
        return floatAndBool;
        

    }

    public override bool CheckForJoyStickAxisDown(JOYSTICK_AXIS_INPUT joyStickNumber, PLAYER player)
    {
        nameOfJoystick = joystickEnumToString[joyStickNumber];
        nameOfJoystick += "_PS4";
        if (player == PLAYER.PLAYER_TWO)
            nameOfJoystick += "_player2";

        valueFromAxis = Input.GetAxis(nameOfJoystick);
        switch (joyStickNumber)
        {
            case JOYSTICK_AXIS_INPUT.L2:
                //L2_button_PS4
                updateAndReturnIfkeyIsDown(joyStickNumber, valueFromAxis, 1.0f);
                break;
            case JOYSTICK_AXIS_INPUT.R2:
                updateAndReturnIfkeyIsDown(joyStickNumber, valueFromAxis, 1.0f);
                break;
            case JOYSTICK_AXIS_INPUT.L3_UP:
                updateAndReturnIfkeyIsDown(joyStickNumber, valueFromAxis);
                break;
            case JOYSTICK_AXIS_INPUT.L3_DOWN:
                updateAndReturnIfkeyIsDown(joyStickNumber, valueFromAxis, 0.0f, false);
                break;
            case JOYSTICK_AXIS_INPUT.L3_LEFT:
                updateAndReturnIfkeyIsDown(joyStickNumber, valueFromAxis, 0.0f, false);
                break;
            case JOYSTICK_AXIS_INPUT.L3_RIGHT:
                updateAndReturnIfkeyIsDown(joyStickNumber, valueFromAxis);
                break;
            case JOYSTICK_AXIS_INPUT.R3_UP:
                updateAndReturnIfkeyIsDown(joyStickNumber, valueFromAxis);
                break;
            case JOYSTICK_AXIS_INPUT.R3_DOWN:
                updateAndReturnIfkeyIsDown(joyStickNumber, valueFromAxis, 0.0f, false);
                break;
            case JOYSTICK_AXIS_INPUT.R3_LEFT:
                updateAndReturnIfkeyIsDown(joyStickNumber, valueFromAxis, 0.0f, false);
                break;
            case JOYSTICK_AXIS_INPUT.R3_RIGHT:
                updateAndReturnIfkeyIsDown(joyStickNumber, valueFromAxis);
                break;
            case JOYSTICK_AXIS_INPUT.DPAD_UP:
                updateAndReturnIfkeyIsDown(joyStickNumber, valueFromAxis);
                break;
            case JOYSTICK_AXIS_INPUT.DPAD_DOWN:
                updateAndReturnIfkeyIsDown(joyStickNumber, valueFromAxis, 0.0f, false);
                break;
            case JOYSTICK_AXIS_INPUT.DPAD_LEFT:
                updateAndReturnIfkeyIsDown(joyStickNumber, valueFromAxis, 0.0f, false);
                break;
            case JOYSTICK_AXIS_INPUT.DPAD_RIGHT:
                updateAndReturnIfkeyIsDown(joyStickNumber, valueFromAxis);
                break;
            default:
                floatAndBool.setFloatAndBool(0, false);
                break;

        }
        return floatAndBool.getBool();

    }

    public override void updateAndReturnIfkeyIsDown(JOYSTICK_AXIS_INPUT joyStickNumber, float valueFromAxis, float offset = 0, bool positiveAxis = true)
    {
        if (!isJoystickKeyPressed.ContainsKey(joyStickNumber))
        {
            isJoystickKeyPressed.Add(joyStickNumber, false);
        }
        if (positiveAxis)
        {
            if (valueFromAxis + offset > 0 && !isJoystickKeyPressed[joyStickNumber])
            {
                keyDown = true;
                isJoystickKeyPressed[joyStickNumber] = true;
            }
            else if (valueFromAxis + offset > 0 && isJoystickKeyPressed[joyStickNumber])
            {
                keyDown = false;
            }
            else
            {
                keyDown = false;
                isJoystickKeyPressed[joyStickNumber] = false;
            }
        }
        else
        {
            if (valueFromAxis + offset < 0 && !isJoystickKeyPressed[joyStickNumber])
            {
                keyDown = true;
                isJoystickKeyPressed[joyStickNumber] = true;
            }
            else if (valueFromAxis + offset < 0 && isJoystickKeyPressed[joyStickNumber])
            {
                keyDown = false;
            }
            else
            {
                keyDown = false;
                isJoystickKeyPressed[joyStickNumber] = false;
            }
        }
        floatAndBool.setFloatAndBool(0, keyDown);
    }
}
//if ((int)keyNumber < 5)
//{
//    switch (keyNumber)
//    {
//        case BUTTON_INPUT.A:
//            //A_button_PS4
//            return Input.GetButtonDown("A_button_PS4");
//        case BUTTON_INPUT.B:
//            return Input.GetButtonDown("B_button_PS4");
//        case BUTTON_INPUT.X:
//            //if(Input.GetButtonDown("X_button_PS4"))
//            //Debug.Log(Input.GetButtonDown("X_button_PS4"));
//            return Input.GetButtonDown("X_button_PS4");
//        case BUTTON_INPUT.Y:
//            return Input.GetButtonDown("Y_button_PS4");
//        case BUTTON_INPUT.L1:
//            return Input.GetButtonDown("L1_button_PS4");

//    }
//}
//else if ((int)keyNumber < 10)
//{
//    switch (keyNumber)
//    {
//        case BUTTON_INPUT.R1:
//            //A_button_PS4
//            return Input.GetButtonDown("R1_button_PS4");
//        case BUTTON_INPUT.BACK:
//            return Input.GetButtonDown("back_button_PS4");
//        case BUTTON_INPUT.START:
//            return Input.GetButtonDown("start_button_PS4");
//        case BUTTON_INPUT.L3:
//            return Input.GetButtonDown("L3_button_PS4");
//        case BUTTON_INPUT.R3:
//            return Input.GetButtonDown("R3_button_PS4");

//    }
//}
//else if ((int)keyNumber <= 15)
//{
//    switch (keyNumber)
//    {
//        case BUTTON_INPUT.R1:
//            //A_button_PS4
//            return Input.GetButtonDown("R1_button_PS4");
//        case BUTTON_INPUT.BACK:
//            return Input.GetButtonDown("back_button_PS4");
//        case BUTTON_INPUT.START:
//            return Input.GetButtonDown("start_button_PS4");
//        case BUTTON_INPUT.L3:
//            return Input.GetButtonDown("L3_button_PS4");
//        case BUTTON_INPUT.R3:
//            return Input.GetButtonDown("R3_button_PS4");

//    }
//}

//if ((int)keyNumber <= (int)BUTTON_INPUT.TOTAL / 2)
//{
//    switch (keyNumber)
//    {
//        case BUTTON_INPUT.A:
//            //A_button_PS4
//            return Input.GetButtonDown("A_button_PS4_player2");
//        case BUTTON_INPUT.B:
//            return Input.GetButtonDown("B_button_PS4_player2");
//        case BUTTON_INPUT.X:
//            return Input.GetButtonDown("X_button_PS4_player2");
//        case BUTTON_INPUT.Y:
//            return Input.GetButtonDown("Y_button_PS4_player2");
//        case BUTTON_INPUT.L1:
//            return Input.GetButtonDown("L1_button_PS4_player2");

//    }
//    return false;
//}
//else
//{
//    switch (keyNumber)
//    {
//        case BUTTON_INPUT.R1:
//            //A_button_PS4
//            return Input.GetButtonDown("R1_button_PS4_player2");
//        case BUTTON_INPUT.BACK:
//            return Input.GetButtonDown("back_button_PS4_player2");
//        case BUTTON_INPUT.START:
//            return Input.GetButtonDown("start_button_PS4_player2");
//        case BUTTON_INPUT.L3:
//            return Input.GetButtonDown("L3_button_PS4_player2");
//        case BUTTON_INPUT.R3:
//            return Input.GetButtonDown("R3_button_PS4_player2");

//    }
//    return false;
//}

//    if ((int)joyStickNumber <= (int)JOYSTICK_AXIS_INPUT.TOTAL / 2)
//    {
//        switch (joyStickNumber)
//        {
//            case JOYSTICK_AXIS_INPUT.L2:
//                //A_button_PS4
//                return Input.GetAxis("L2_button_PS4_player2") + 1;
//            case JOYSTICK_AXIS_INPUT.R2:
//                return Input.GetAxis("R2_button_PS4_player2") + 1;
//            case JOYSTICK_AXIS_INPUT.L3_X:
//                return Input.GetAxis("leftStick_X_PS4_player2");
//            case JOYSTICK_AXIS_INPUT.L3_Y:
//                return Input.GetAxis("leftStick_Y_PS4_player2");

//        }
//        return 0;
//    }
//    else
//    {
//        switch (joyStickNumber)
//        {
//            case JOYSTICK_AXIS_INPUT.R3_X:
//                return Input.GetAxis("rightStick_X_PS4_player2");
//            case JOYSTICK_AXIS_INPUT.R3_Y:
//                return Input.GetAxis("right_button_PS4_player2");
//            case JOYSTICK_AXIS_INPUT.DPAD_X:
//                return Input.GetAxis("DPad_X_PS4_player2");
//            case JOYSTICK_AXIS_INPUT.DPAD_Y:
//                return Input.GetAxis("DPad_Y_PS4_player2");
//        }
//        return 0;
//    }
//}
//return 0;

/*public override bool CheckForKeyPress(BUTTON_INPUT keyNumber, PLAYER player)
   {
       if(keyNumber < BUTTON_INPUT.TOTAL && player < PLAYER.MAX_PLAYERS)
       {
           string button = "joystick " + ((int)player+1) + " button " + (int)keyNumber;
           return Input.GetKeyDown(button);
       }
       return false;
       //if (player == PLAYER.PLAYER_ONE)
       //{
       //    switch (keyNumber)
       //    {
       //        case BUTTON_INPUT.A:
       //            return Input.GetButtonDown("A_button_PS4");
       //        case BUTTON_INPUT.B:
       //            return Input.GetButtonDown("B_button_PS4");
       //        case BUTTON_INPUT.X:
       //            return Input.GetButtonDown("X_button_PS4");
       //        case BUTTON_INPUT.Y:
       //            return Input.GetButtonDown("Y_button_PS4");
       //        case BUTTON_INPUT.L1:
       //            return Input.GetButtonDown("L1_button_PS4");
       //        case BUTTON_INPUT.R1:
       //            return Input.GetButtonDown("R1_button_PS4");
       //        case BUTTON_INPUT.BACK:
       //            return Input.GetButtonDown("back_button_PS4");
       //        case BUTTON_INPUT.START:
       //            return Input.GetButtonDown("start_button_PS4");
       //        case BUTTON_INPUT.L3:
       //            return Input.GetButtonDown("L3_button_PS4");
       //        case BUTTON_INPUT.R3:
       //            return Input.GetButtonDown("R3_button_PS4");


       //    }

       //}
       //else if (player == PLAYER.PLAYER_TWO)
       //{
       //    switch (keyNumber)
       //    {
       //        case BUTTON_INPUT.A:
       //            return Input.GetButtonDown("A_button_PS4_player2");
       //        case BUTTON_INPUT.B:
       //            return Input.GetButtonDown("B_button_PS4_player2");
       //        case BUTTON_INPUT.X:
       //            return Input.GetButtonDown("X_button_PS4_player2");
       //        case BUTTON_INPUT.Y:
       //            return Input.GetButtonDown("Y_button_PS4_player2");
       //        case BUTTON_INPUT.L1:
       //            return Input.GetButtonDown("L1_button_PS4_player2");
       //        case BUTTON_INPUT.R1:
       //            return Input.GetButtonDown("R1_button_PS4_player2");
       //        case BUTTON_INPUT.BACK:
       //            return Input.GetButtonDown("back_button_PS4_player2");
       //        case BUTTON_INPUT.START:
       //            return Input.GetButtonDown("start_button_PS4_player2");
       //        case BUTTON_INPUT.L3:
       //            return Input.GetButtonDown("L3_button_PS4_player2");
       //        case BUTTON_INPUT.R3:
       //            return Input.GetButtonDown("R3_button_PS4_player2");


       //    }
       //}
       //return false;
   }

   public override bool CheckForKeyPressHold(BUTTON_INPUT keyNumber, PLAYER player)
   {
       if (keyNumber < BUTTON_INPUT.TOTAL && player < PLAYER.MAX_PLAYERS)
       {
           string button = "joystick " + ((int)player + 1) + " button " + (int)keyNumber;
           return Input.GetKey(button);
       }
       return false;
       //if (player == PLAYER.PLAYER_ONE)
       //{
       //    switch (keyNumber)
       //    {
       //        case BUTTON_INPUT.A:
       //            return Input.GetButton("A_button_PS4");
       //        case BUTTON_INPUT.B:
       //            return Input.GetButton("B_button_PS4");
       //        case BUTTON_INPUT.X:
       //            return Input.GetButton("X_button_PS4");
       //        case BUTTON_INPUT.Y:
       //            return Input.GetButton("Y_button_PS4");
       //        case BUTTON_INPUT.L1:
       //            return Input.GetButton("L1_button_PS4");
       //        case BUTTON_INPUT.R1:
       //            return Input.GetButton("R1_button_PS4");
       //        case BUTTON_INPUT.BACK:
       //            return Input.GetButton("back_button_PS4");
       //        case BUTTON_INPUT.START:
       //            return Input.GetButton("start_button_PS4");
       //        case BUTTON_INPUT.L3:
       //            return Input.GetButton("L3_button_PS4");
       //        case BUTTON_INPUT.R3:
       //            return Input.GetButton("R3_button_PS4");


       //    }

       //}
       //else if (player == PLAYER.PLAYER_TWO)
       //{
       //    switch (keyNumber)
       //    {
       //        case BUTTON_INPUT.A:
       //            return Input.GetButton("A_button_PS4_player2");
       //        case BUTTON_INPUT.B:
       //            return Input.GetButton("B_button_PS4_player2");
       //        case BUTTON_INPUT.X:
       //            return Input.GetButton("X_button_PS4_player2");
       //        case BUTTON_INPUT.Y:
       //            return Input.GetButton("Y_button_PS4_player2");
       //        case BUTTON_INPUT.L1:
       //            return Input.GetButton("L1_button_PS4_player2");
       //        case BUTTON_INPUT.R1:
       //            return Input.GetButton("R1_button_PS4_player2");
       //        case BUTTON_INPUT.BACK:
       //            return Input.GetButton("back_button_PS4_player2");
       //        case BUTTON_INPUT.START:
       //            return Input.GetButton("start_button_PS4_player2");
       //        case BUTTON_INPUT.L3:
       //            return Input.GetButton("L3_button_PS4_player2");
       //        case BUTTON_INPUT.R3:
       //            return Input.GetButton("R3_button_PS4_player2");


       //    }
       //}
       //return false;
   }

   public override bool CheckForKeyPressUp(BUTTON_INPUT keyNumber, PLAYER player)
   {
       if (keyNumber < BUTTON_INPUT.TOTAL && player < PLAYER.MAX_PLAYERS)
       {
           string button = "joystick " + ((int)player + 1) + " button " + (int)keyNumber;
           return Input.GetKeyUp(button);
       }
       return false;
       //if (player == PLAYER.PLAYER_ONE)
       //{
       //    switch (keyNumber)
       //    {
       //        case BUTTON_INPUT.A:
       //            return Input.GetButtonUp("A_button_PS4");
       //        case BUTTON_INPUT.B:
       //            return Input.GetButtonUp("B_button_PS4");
       //        case BUTTON_INPUT.X:
       //            return Input.GetButtonUp("X_button_PS4");
       //        case BUTTON_INPUT.Y:
       //            return Input.GetButtonUp("Y_button_PS4");
       //        case BUTTON_INPUT.L1:
       //            return Input.GetButtonUp("L1_button_PS4");
       //        case BUTTON_INPUT.R1:
       //            return Input.GetButtonUp("R1_button_PS4");
       //        case BUTTON_INPUT.BACK:
       //            return Input.GetButtonUp("back_button_PS4");
       //        case BUTTON_INPUT.START:
       //            return Input.GetButtonUp("start_button_PS4");
       //        case BUTTON_INPUT.L3:
       //            return Input.GetButtonUp("L3_button_PS4");
       //        case BUTTON_INPUT.R3:
       //            return Input.GetButtonUp("R3_button_PS4");


       //    }

       //}
       //else if (player == PLAYER.PLAYER_TWO)
       //{
       //    switch (keyNumber)
       //    {
       //        case BUTTON_INPUT.A:
       //            return Input.GetButtonUp("A_button_PS4_player2");
       //        case BUTTON_INPUT.B:
       //            return Input.GetButtonUp("B_button_PS4_player2");
       //        case BUTTON_INPUT.X:
       //            return Input.GetButtonUp("X_button_PS4_player2");
       //        case BUTTON_INPUT.Y:
       //            return Input.GetButtonUp("Y_button_PS4_player2");
       //        case BUTTON_INPUT.L1:
       //            return Input.GetButtonUp("L1_button_PS4_player2");
       //        case BUTTON_INPUT.R1:
       //            return Input.GetButtonUp("R1_button_PS4_player2");
       //        case BUTTON_INPUT.BACK:
       //            return Input.GetButtonUp("back_button_PS4_player2");
       //        case BUTTON_INPUT.START:
       //            return Input.GetButtonUp("start_button_PS4_player2");
       //        case BUTTON_INPUT.L3:
       //            return Input.GetButtonUp("L3_button_PS4_player2");
       //        case BUTTON_INPUT.R3:
       //            return Input.GetButtonUp("R3_button_PS4_player2");


       //    }
       //}
       //return false;
   }

   public override FloatAndBool CheckForJoyStickAxis(JOYSTICK_AXIS_INPUT joyStickNumber, PLAYER player)
   {
       if (player == PLAYER.PLAYER_ONE)
       {

           switch (joyStickNumber)
           {
               case JOYSTICK_AXIS_INPUT.L2:
                   floatAndBool.setFloatAndBool(Input.GetAxis("L2_button_PS4") + 1, Input.GetAxis("L2_button_PS4") + 1 > 0);
                   break;
               case JOYSTICK_AXIS_INPUT.R2:
                   floatAndBool.setFloatAndBool(Input.GetAxis("R2_button_PS4") + 1, Input.GetAxis("R2_button_PS4") + 1 > 0);
                   break;
               case JOYSTICK_AXIS_INPUT.L3_UP:
                   floatAndBool.setFloatAndBool(Input.GetAxis("leftStick_Y_PS4"), Input.GetAxis("leftStick_Y_PS4") > 0);
                   break;
               case JOYSTICK_AXIS_INPUT.L3_DOWN:
                   floatAndBool.setFloatAndBool(Input.GetAxis("leftStick_Y_PS4"), Input.GetAxis("leftStick_Y_PS4") < 0);
                   break;
               case JOYSTICK_AXIS_INPUT.L3_LEFT:
                   floatAndBool.setFloatAndBool(Input.GetAxis("leftStick_X_PS4"), Input.GetAxis("leftStick_X_PS4") < 0);
                   break;
               case JOYSTICK_AXIS_INPUT.L3_RIGHT:
                   floatAndBool.setFloatAndBool(Input.GetAxis("leftStick_X_PS4"), Input.GetAxis("leftStick_X_PS4") > 0);
                   break;
               case JOYSTICK_AXIS_INPUT.L3_Y:
                   floatAndBool.setFloatAndBool(Input.GetAxis("leftStick_Y_PS4"), false);
                   break;
               case JOYSTICK_AXIS_INPUT.L3_X:
                   floatAndBool.setFloatAndBool(Input.GetAxis("leftStick_X_PS4"), false);
                   break;
               case JOYSTICK_AXIS_INPUT.R3_UP:
                   floatAndBool.setFloatAndBool(Input.GetAxis("rightStick_Y_PS4"), Input.GetAxis("rightStick_Y_PS4") > 0);
                   break;
               case JOYSTICK_AXIS_INPUT.R3_DOWN:
                   floatAndBool.setFloatAndBool(Input.GetAxis("rightStick_Y_PS4"), Input.GetAxis("rightStick_Y_PS4") < 0);
                   break;
               case JOYSTICK_AXIS_INPUT.R3_LEFT:
                   floatAndBool.setFloatAndBool(Input.GetAxis("rightStick_X_PS4"), Input.GetAxis("rightStick_X_PS4") < 0);
                   break;
               case JOYSTICK_AXIS_INPUT.R3_RIGHT:
                   floatAndBool.setFloatAndBool(Input.GetAxis("rightStick_X_PS4"), Input.GetAxis("rightStick_X_PS4") > 0);
                   break;
               case JOYSTICK_AXIS_INPUT.R3_Y:
                   floatAndBool.setFloatAndBool(Input.GetAxis("rightStick_Y_PS4"), false);
                   break;
               case JOYSTICK_AXIS_INPUT.R3_X:
                   floatAndBool.setFloatAndBool(Input.GetAxis("rightStick_X_PS4"), false);
                   break;
               case JOYSTICK_AXIS_INPUT.DPAD_UP:
                   floatAndBool.setFloatAndBool(Input.GetAxis("DPad_Y_PS4"), Input.GetAxis("DPad_Y_PS4") > 0);
                   break;
               case JOYSTICK_AXIS_INPUT.DPAD_DOWN:
                   floatAndBool.setFloatAndBool(Input.GetAxis("DPad_Y_PS4"), Input.GetAxis("DPad_Y_PS4") < 0);
                   break;
               case JOYSTICK_AXIS_INPUT.DPAD_LEFT:
                   floatAndBool.setFloatAndBool(Input.GetAxis("DPad_X_PS4"), Input.GetAxis("DPad_X_PS4") < 0);
                   break;
               case JOYSTICK_AXIS_INPUT.DPAD_RIGHT:
                   floatAndBool.setFloatAndBool(Input.GetAxis("DPad_X_PS4"), Input.GetAxis("DPad_X_PS4") > 0);
                   break;
               case JOYSTICK_AXIS_INPUT.DPAD_Y:
                   floatAndBool.setFloatAndBool(Input.GetAxis("DPad_Y_PS4"), false);
                   break;
               case JOYSTICK_AXIS_INPUT.DPAD_X:
                   floatAndBool.setFloatAndBool(Input.GetAxis("DPad_X_PS4"), false);
                   break;
               default:
                   floatAndBool.setFloatAndBool(0, false);
                   break;

           }
           return floatAndBool;

       }
       else if (player == PLAYER.PLAYER_TWO)
       {
           switch (joyStickNumber)
           {
               case JOYSTICK_AXIS_INPUT.L2:
                   floatAndBool.setFloatAndBool(Input.GetAxis("L2_button_PS4_player2") + 1, Input.GetAxis("L2_button_PS4_player2") + 1 > 0);
                   break;
               case JOYSTICK_AXIS_INPUT.R2:
                   floatAndBool.setFloatAndBool(Input.GetAxis("R2_button_PS4_player2") + 1, Input.GetAxis("R2_button_PS4_player2") + 1 > 0);
                   break;
               case JOYSTICK_AXIS_INPUT.L3_UP:
                   floatAndBool.setFloatAndBool(Input.GetAxis("leftStick_Y_PS4_player2"), Input.GetAxis("leftStick_Y_PS4_player2") > 0);
                   break;
               case JOYSTICK_AXIS_INPUT.L3_DOWN:
                   floatAndBool.setFloatAndBool(Input.GetAxis("leftStick_Y_PS4_player2"), Input.GetAxis("leftStick_Y_PS4_player2") < 0);
                   break;
               case JOYSTICK_AXIS_INPUT.L3_LEFT:
                   floatAndBool.setFloatAndBool(Input.GetAxis("leftStick_X_PS4_player2"), Input.GetAxis("leftStick_X_PS4_player2") < 0);
                   break;
               case JOYSTICK_AXIS_INPUT.L3_RIGHT:
                   floatAndBool.setFloatAndBool(Input.GetAxis("leftStick_X_PS4_player2"), Input.GetAxis("leftStick_X_PS4_player2") > 0);
                   break;
               case JOYSTICK_AXIS_INPUT.L3_Y:
                   floatAndBool.setFloatAndBool(Input.GetAxis("leftStick_Y_PS4_player2"), false);
                   break;
               case JOYSTICK_AXIS_INPUT.L3_X:
                   floatAndBool.setFloatAndBool(Input.GetAxis("leftStick_X_PS4_player2"), false);
                   break;
               case JOYSTICK_AXIS_INPUT.R3_UP:
                   floatAndBool.setFloatAndBool(Input.GetAxis("rightStick_Y_PS4_player2"), Input.GetAxis("rightStick_Y_PS4_player2") > 0);
                   break;
               case JOYSTICK_AXIS_INPUT.R3_DOWN:
                   floatAndBool.setFloatAndBool(Input.GetAxis("rightStick_Y_PS4_player2"), Input.GetAxis("rightStick_Y_PS4_player2") < 0);
                   break;
               case JOYSTICK_AXIS_INPUT.R3_LEFT:
                   floatAndBool.setFloatAndBool(Input.GetAxis("rightStick_X_PS4_player2"), Input.GetAxis("rightStick_X_PS4_player2") < 0);
                   break;
               case JOYSTICK_AXIS_INPUT.R3_RIGHT:
                   floatAndBool.setFloatAndBool(Input.GetAxis("rightStick_X_PS4_player2"), Input.GetAxis("rightStick_X_PS4_player2") > 0);
                   break;
               case JOYSTICK_AXIS_INPUT.R3_Y:
                   floatAndBool.setFloatAndBool(Input.GetAxis("rightStick_Y_PS4_player2"), false);
                   break;
               case JOYSTICK_AXIS_INPUT.R3_X:
                   floatAndBool.setFloatAndBool(Input.GetAxis("rightStick_X_PS4_player2"), false);
                   break;
               case JOYSTICK_AXIS_INPUT.DPAD_UP:
                   floatAndBool.setFloatAndBool(Input.GetAxis("DPad_Y_PS4_player2"), Input.GetAxis("DPad_Y_PS4_player2") > 0);
                   break;
               case JOYSTICK_AXIS_INPUT.DPAD_DOWN:
                   floatAndBool.setFloatAndBool(Input.GetAxis("DPad_Y_PS4_player2"), Input.GetAxis("DPad_Y_PS4_player2") < 0);
                   break;
               case JOYSTICK_AXIS_INPUT.DPAD_LEFT:
                   floatAndBool.setFloatAndBool(Input.GetAxis("DPad_X_PS4_player2"), Input.GetAxis("DPad_X_PS4_player2") < 0);
                   break;
               case JOYSTICK_AXIS_INPUT.DPAD_RIGHT:
                   floatAndBool.setFloatAndBool(Input.GetAxis("DPad_X_PS4_player2"), Input.GetAxis("DPad_X_PS4_player2") > 0);
                   break;
               case JOYSTICK_AXIS_INPUT.DPAD_Y:
                   floatAndBool.setFloatAndBool(Input.GetAxis("DPad_Y_PS4_player2"), false);
                   break;
               case JOYSTICK_AXIS_INPUT.DPAD_X:
                   floatAndBool.setFloatAndBool(Input.GetAxis("DPad_X_PS4_player2"), false);
                   break;
               default:
                   floatAndBool.setFloatAndBool(0, false);
                   break;

           }
           return floatAndBool;
       }
       else
       {
           floatAndBool.setFloatAndBool(0, false);
           return floatAndBool;
       }

   }

   public override bool CheckForJoyStickAxisDown(JOYSTICK_AXIS_INPUT joyStickNumber, PLAYER player)
   {
       if (player == PLAYER.PLAYER_ONE)
       {

           switch (joyStickNumber)
           {
               case JOYSTICK_AXIS_INPUT.L2:
                   //L2_button_PS4
                   updateAndReturnIfkeyIsDown(joyStickNumber, "L2_button_PS4", 1.0f);
                   break;
               case JOYSTICK_AXIS_INPUT.R2:
                   updateAndReturnIfkeyIsDown(joyStickNumber, "R2_button_PS4", 1.0f);
                   break;
               case JOYSTICK_AXIS_INPUT.L3_UP:
                   updateAndReturnIfkeyIsDown(joyStickNumber, "leftStick_Y_PS4");
                   break;
               case JOYSTICK_AXIS_INPUT.L3_DOWN:
                   updateAndReturnIfkeyIsDown(joyStickNumber, "leftStick_Y_PS4", 0.0f, false);
                   break;
               case JOYSTICK_AXIS_INPUT.L3_LEFT:
                   updateAndReturnIfkeyIsDown(joyStickNumber, "leftStick_X_PS4", 0.0f, false);
                   break;
               case JOYSTICK_AXIS_INPUT.L3_RIGHT:
                   updateAndReturnIfkeyIsDown(joyStickNumber, "leftStick_X_PS4");
                   break;
               //case JOYSTICK_AXIS_INPUT.L3_Y:
               //    floatAndBool.setFloatAndBool(Input.GetAxis("leftStick_Y_PS4"), false);
               //    break;
               //case JOYSTICK_AXIS_INPUT.L3_X:
               //    floatAndBool.setFloatAndBool(Input.GetAxis("leftStick_X_PS4"), false);
               //    break;
               case JOYSTICK_AXIS_INPUT.R3_UP:
                   updateAndReturnIfkeyIsDown(joyStickNumber, "rightStick_Y_PS4");
                   break;
               case JOYSTICK_AXIS_INPUT.R3_DOWN:
                   updateAndReturnIfkeyIsDown(joyStickNumber, "rightStick_Y_PS4", 0.0f, false);
                   break;
               case JOYSTICK_AXIS_INPUT.R3_LEFT:
                   updateAndReturnIfkeyIsDown(joyStickNumber, "rightStick_X_PS4", 0.0f, false);
                   break;
               case JOYSTICK_AXIS_INPUT.R3_RIGHT:
                   updateAndReturnIfkeyIsDown(joyStickNumber, "rightStick_X_PS4");
                   break;
               //case JOYSTICK_AXIS_INPUT.R3_Y:
               //    floatAndBool.setFloatAndBool(Input.GetAxis("rightStick_Y_PS4"), false);
               //    break;
               //case JOYSTICK_AXIS_INPUT.R3_X:
               //    floatAndBool.setFloatAndBool(Input.GetAxis("rightStick_Y_PS4"), false);
               //    break;
               case JOYSTICK_AXIS_INPUT.DPAD_UP:
                   updateAndReturnIfkeyIsDown(joyStickNumber, "DPad_Y_PS4");
                   break;
               case JOYSTICK_AXIS_INPUT.DPAD_DOWN:
                   updateAndReturnIfkeyIsDown(joyStickNumber, "DPad_Y_PS4",0.0f,false);
                   break;
               case JOYSTICK_AXIS_INPUT.DPAD_LEFT:
                   updateAndReturnIfkeyIsDown(joyStickNumber, "DPad_X_PS4", 0.0f, false);
                   break;
               case JOYSTICK_AXIS_INPUT.DPAD_RIGHT:
                   updateAndReturnIfkeyIsDown(joyStickNumber, "DPad_X_PS4");
                   break;
               //case JOYSTICK_AXIS_INPUT.DPAD_Y:
               //    floatAndBool.setFloatAndBool(Input.GetAxis("DPad_Y_PS4"), false);
               //    break;
               //case JOYSTICK_AXIS_INPUT.DPAD_X:
               //    floatAndBool.setFloatAndBool(Input.GetAxis("DPad_X_PS4"), false);
               //    break;
               default:
                   floatAndBool.setFloatAndBool(0, false);
                   break;

           }
           return floatAndBool.getBool();

       }
       else if (player == PLAYER.PLAYER_TWO)
       {
           switch (joyStickNumber)
           {
               case JOYSTICK_AXIS_INPUT.L2:
                   //L2_button_PS4
                   updateAndReturnIfkeyIsDown(joyStickNumber, "L2_button_PS4_player2", 1.0f);
                   break;
               case JOYSTICK_AXIS_INPUT.R2:
                   updateAndReturnIfkeyIsDown(joyStickNumber, "R2_button_PS4_player2", 1.0f);
                   break;
               case JOYSTICK_AXIS_INPUT.L3_UP:
                   updateAndReturnIfkeyIsDown(joyStickNumber, "leftStick_Y_PS4_player2");
                   break;
               case JOYSTICK_AXIS_INPUT.L3_DOWN:
                   updateAndReturnIfkeyIsDown(joyStickNumber, "leftStick_Y_PS4_player2", 0.0f, false);
                   break;
               case JOYSTICK_AXIS_INPUT.L3_LEFT:
                   updateAndReturnIfkeyIsDown(joyStickNumber, "leftStick_X_PS4_player2", 0.0f, false);
                   break;
               case JOYSTICK_AXIS_INPUT.L3_RIGHT:
                   updateAndReturnIfkeyIsDown(joyStickNumber, "leftStick_X_PS4_player2");
                   break;
               //case JOYSTICK_AXIS_INPUT.L3_Y:
               //    floatAndBool.setFloatAndBool(Input.GetAxis("leftStick_Y_PS4_player2"), false);
               //    break;
               //case JOYSTICK_AXIS_INPUT.L3_X:
               //    floatAndBool.setFloatAndBool(Input.GetAxis("leftStick_X_PS4_player2"), false);
               //    break;
               case JOYSTICK_AXIS_INPUT.R3_UP:
                   updateAndReturnIfkeyIsDown(joyStickNumber, "rightStick_Y_PS4_player2");
                   break;
               case JOYSTICK_AXIS_INPUT.R3_DOWN:
                   updateAndReturnIfkeyIsDown(joyStickNumber, "rightStick_Y_PS4_player2", 0.0f, false);
                   break;
               case JOYSTICK_AXIS_INPUT.R3_LEFT:
                   updateAndReturnIfkeyIsDown(joyStickNumber, "rightStick_X_PS4_player2", 0.0f, false);
                   break;
               case JOYSTICK_AXIS_INPUT.R3_RIGHT:
                   updateAndReturnIfkeyIsDown(joyStickNumber, "rightStick_X_PS4_player2");
                   break;
               //case JOYSTICK_AXIS_INPUT.R3_Y:
               //    floatAndBool.setFloatAndBool(Input.GetAxis("rightStick_Y_PS4_player2"), false);
               //    break;
               //case JOYSTICK_AXIS_INPUT.R3_X:
               //    floatAndBool.setFloatAndBool(Input.GetAxis("rightStick_Y_PS4_player2"), false);
               //    break;
               case JOYSTICK_AXIS_INPUT.DPAD_UP:
                   updateAndReturnIfkeyIsDown(joyStickNumber, "DPad_Y_PS4_player2");
                   break;
               case JOYSTICK_AXIS_INPUT.DPAD_DOWN:
                   updateAndReturnIfkeyIsDown(joyStickNumber, "DPad_Y_PS4_player2", 0.0f,false);
                   break;
               case JOYSTICK_AXIS_INPUT.DPAD_LEFT:
                   updateAndReturnIfkeyIsDown(joyStickNumber, "DPad_X_PS4_player2", 0.0f, false);
                   break;
               case JOYSTICK_AXIS_INPUT.DPAD_RIGHT:
                   updateAndReturnIfkeyIsDown(joyStickNumber, "DPad_X_PS4_player2");
                   break;
               //case JOYSTICK_AXIS_INPUT.DPAD_Y:
               //    floatAndBool.setFloatAndBool(Input.GetAxis("DPad_Y_PS4_player2"), false);
               //    break;
               //case JOYSTICK_AXIS_INPUT.DPAD_X:
               //    floatAndBool.setFloatAndBool(Input.GetAxis("DPad_X_PS4_player2"), false);
               //    break;
               default:
                   floatAndBool.setFloatAndBool(0, false);
                   break;

           }
           return floatAndBool.getBool();
       }
       else
       {
           floatAndBool.setFloatAndBool(0, false);
           return floatAndBool.getBool();
       }*/
