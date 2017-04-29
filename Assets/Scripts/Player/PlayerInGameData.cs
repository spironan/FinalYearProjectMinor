using UnityEngine;
using System.Collections;

public class PlayerInGameData : MonoBehaviour 
{
    uint playerID;
    short wonMatches;
    TEAM playerTeam;
    CHARACTERS selectedChar;

    //Call Init When Game Starts
    void Init()
    {
        //Generate Blue or Red Team
        //playerTeam = ;
        wonMatches = 0;
    }

    void WinMatch()
    {
        wonMatches++;
    }

    short GetMatchWins()
    {
        return wonMatches;
    }
}
