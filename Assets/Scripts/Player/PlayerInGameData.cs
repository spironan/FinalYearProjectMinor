using UnityEngine;
using System.Collections;

public class PlayerInGameData : MonoBehaviour 
{
    short wonMatches;
    TEAM playerTeam;
    CharacterBase selectedCharData;

    public CharacterBase GetCharData() { return selectedCharData; }
    //public void SetChar(CHARACTERS charaName) { selectedChar = CharacterManager.GetInstance().GetCharacterByName(charaName); }
    //public void SetChar(int charaID) { selectedChar = CharacterManager.GetInstance().GetCharacterByID(charaID); }
    public void SetChar(string charName) { selectedCharData = CharacterManager.GetInstance().GetCharacterByName(charName); }
    public float GetHealthPercentage()
    {
        return selectedCharData.GetHealth() / selectedCharData.GetMaxHp();
    }

    public TEAM GetTeam() { return playerTeam; }
    public void SetTeam(TEAM newPlayerTeam) { playerTeam = newPlayerTeam; }

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
