using UnityEngine;
using System.Collections;

public class PlayerInGameData 
{
    short wonMatches;
    short winsToCompleteSet;
    TEAM playerTeam;
    string characterName = "";
    bool winSet = false;

    public string GetCharName() { return characterName; }
    public void SetCharName(string charName) { characterName = charName; }

    public TEAM GetTeam() { return playerTeam; }
    public void SetTeam(TEAM newPlayerTeam) { playerTeam = newPlayerTeam; }

    //Call Init When Game Starts
    public void SetWinCondition(short winsToCompleteSet) 
    {
        if (winsToCompleteSet < 0)
        {
            Debug.Log("Win Condition must be larger than zero, you inputted : " + winsToCompleteSet);
        }
        this.winsToCompleteSet = winsToCompleteSet;
    }

    public void StartMatch() { winSet = false; wonMatches = 0; }

    public void WinMatch() 
    {
        wonMatches++;
        if (wonMatches == winsToCompleteSet)
            winSet = true;
    }
    public short GetMatchWins() { return wonMatches; }

    public bool GetSetWon() { return winSet; }
}
