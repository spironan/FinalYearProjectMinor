using UnityEngine;
using System.Collections;

public class PS4ControllerInput : ControllerInput {
    //public int playernum = 
    public override bool CheckForKeyPress(BUTTON_INPUT keyNumber, PLAYER player)
    {
        if (player == PLAYER.PLAYER_ONE)
        {
            switch (keyNumber)
            {
                case BUTTON_INPUT.A:
                    return Input.GetButtonDown("A_button_PS4");
                case BUTTON_INPUT.B:
                    return Input.GetButtonDown("B_button_PS4");
                case BUTTON_INPUT.X:
                    return Input.GetButtonDown("X_button_PS4");
                case BUTTON_INPUT.Y:
                    return Input.GetButtonDown("Y_button_PS4");
                case BUTTON_INPUT.L1:
                    return Input.GetButtonDown("L1_button_PS4");
                case BUTTON_INPUT.R1:
                    return Input.GetButtonDown("R1_button_PS4");
                case BUTTON_INPUT.BACK:
                    return Input.GetButtonDown("back_button_PS4");
                case BUTTON_INPUT.START:
                    return Input.GetButtonDown("start_button_PS4");
                case BUTTON_INPUT.L3:
                    return Input.GetButtonDown("L3_button_PS4");
                case BUTTON_INPUT.R3:
                    return Input.GetButtonDown("R3_button_PS4");


            }
            
        }
        else if (player == PLAYER.PLAYER_TWO)
        {
            switch (keyNumber)
            {
                case BUTTON_INPUT.A:
                    return Input.GetButtonDown("A_button_PS4_player2");
                case BUTTON_INPUT.B:
                    return Input.GetButtonDown("B_button_PS4_player2");
                case BUTTON_INPUT.X:
                    return Input.GetButtonDown("X_button_PS4_player2");
                case BUTTON_INPUT.Y:
                    return Input.GetButtonDown("Y_button_PS4_player2");
                case BUTTON_INPUT.L1:
                    return Input.GetButtonDown("L1_button_PS4_player2");
                case BUTTON_INPUT.R1:
                    return Input.GetButtonDown("R1_button_PS4_player2");
                case BUTTON_INPUT.BACK:
                    return Input.GetButtonDown("back_button_PS4_player2");
                case BUTTON_INPUT.START:
                    return Input.GetButtonDown("start_button_PS4_player2");
                case BUTTON_INPUT.L3:
                    return Input.GetButtonDown("L3_button_PS4_player2");
                case BUTTON_INPUT.R3:
                    return Input.GetButtonDown("R3_button_PS4_player2");


            }
        }
        return false;
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
                    floatAndBool.setFloatAndBool(Input.GetAxis("rightStick_Y_PS4"), Input.GetAxis("rightStick_Y_PS4") < 0);
                    break;
                case JOYSTICK_AXIS_INPUT.R3_RIGHT:
                    floatAndBool.setFloatAndBool(Input.GetAxis("rightStick_Y_PS4"), Input.GetAxis("rightStick_Y_PS4") > 0);
                    break;
                case JOYSTICK_AXIS_INPUT.R3_Y:
                    floatAndBool.setFloatAndBool(Input.GetAxis("rightStick_Y_PS4"), false);
                    break;
                case JOYSTICK_AXIS_INPUT.R3_X:
                    floatAndBool.setFloatAndBool(Input.GetAxis("rightStick_Y_PS4"), false);
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
                    floatAndBool.setFloatAndBool(Input.GetAxis("rightStick_Y_PS4_player2"), Input.GetAxis("rightStick_Y_PS4_player2") < 0);
                    break;
                case JOYSTICK_AXIS_INPUT.R3_RIGHT:
                    floatAndBool.setFloatAndBool(Input.GetAxis("rightStick_Y_PS4_player2"), Input.GetAxis("rightStick_Y_PS4_player2") > 0);
                    break;
                case JOYSTICK_AXIS_INPUT.R3_Y:
                    floatAndBool.setFloatAndBool(Input.GetAxis("rightStick_Y_PS4_player2"), false);
                    break;
                case JOYSTICK_AXIS_INPUT.R3_X:
                    floatAndBool.setFloatAndBool(Input.GetAxis("rightStick_Y_PS4_player2"), false);
                    break;
                case JOYSTICK_AXIS_INPUT.DPAD_UP:
                    floatAndBool.setFloatAndBool(Input.GetAxis("DPad_Y_PS4_player2"), Input.GetAxis("DPad_Y_PS4_player2_player2") > 0);
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