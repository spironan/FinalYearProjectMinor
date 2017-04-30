using UnityEngine;
using System.Collections;

public class PlayerInGameData : MonoBehaviour 
{
    uint playerID;
    short wonMatches;
    TEAM playerTeam;
    CharacterBase selectedChar;


    public CharacterBase GetChar() { return selectedChar; }

    //Call Init When Game Starts
    public void Init()
    {
        //Generate Blue or Red Team
        //playerTeam = ;
        wonMatches = 0;
    }

    public void WinMatch()
    {
        wonMatches++;
    }

    public short GetMatchWins()
    {
        return wonMatches;
    }
}
