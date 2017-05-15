using UnityEngine;
using System.Collections;

public enum GAMESTATE
{ 
    MAIN_MENU,
    CHAR_SELECT,
    IN_GAME,
    MAX_STATE
};

public enum TEAM
{
    TEAM_BEGIN,
    RED_TEAM = TEAM_BEGIN,
    BLUE_TEAM,
    MAX_TEAM
};

public enum PLAYER
{
    PLAYER_BEGIN,
    PLAYER_ONE = PLAYER_BEGIN,
    PLAYER_TWO,
    MAX_PLAYERS,
};

public enum GAME_MODES
{
    LOCAL_PVP,
    MAX_GAME_MODES
}

public enum PLAYMAPS
{
    STANDARD = 0,
    RANDOM,
    EXTRA,
    TEST_MAP_1,
    TEST_MAP_2,
    TEST_MAP_3,
    TEST_MAP_4,
    MAX_MAP
}

public enum ATTACKTYPE
{
    GLOBAL,
    MID_RANGE,
    MELEE,
    MAX_ATTACKTYPE
};

public enum CHARACTERS
{
    PLAYTEST_CHAR = 0,
    PLAYTEST_CHAR_2,
    MAX_CHARACTER
};

public enum BUTTON_INPUT
{
    A = 0,
    B,
    X,
    Y,
    L1,
    R1,
    BACK,
    START,
    L3,
    R3,
    TOTAL
};

public enum JOYSTICK_AXIS_INPUT
{
    L2 = 0,
    R2,
    L3_UP,
    L3_DOWN,
    L3_LEFT,
    L3_RIGHT,
    L3_Y,
    L3_X,
    R3_UP,
    R3_DOWN,
    R3_LEFT,
    R3_RIGHT,
    R3_Y,
    R3_X,
    DPAD_UP,
    DPAD_DOWN,
    DPAD_LEFT,
    DPAD_RIGHT,
    DPAD_Y,
    DPAD_X,
    TOTAL
};