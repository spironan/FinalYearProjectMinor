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
    RED_TEAM,
    BLUE_TEAM,
    MAX_TEAM
};

public enum PLAYER
{
    PLAYER_ONE,
    PLAYER_TWO,
    MAX_PLAYERS,
};

public enum GAME_MODES
{
    LOCAL_PVP,
    MAX_GAME_MODES
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
