using UnityEngine;
using System.Collections;

public class PlayerInGameData 
{
    short wonMatches;
    TEAM playerTeam;
    //CharacterBase selectedCharData;
    string characterName = "";

    //public CharacterBase GetCharData() { return selectedCharData; }
    //public void SetChar(string charName) { selectedCharData = CharacterManager.GetInstance().GetCharacterByName(charName); }

    public string GetCharName() { return characterName; }
    public void SetCharName(string charName) { characterName = charName; }

    public TEAM GetTeam() { return playerTeam; }
    public void SetTeam(TEAM newPlayerTeam) { playerTeam = newPlayerTeam; }

    //Call Init When Game Starts
    public void StartMatch() { wonMatches = 0; }

    public void WinMatch() { wonMatches++; }
    public short GetMatchWins() { return wonMatches; }
    
}
