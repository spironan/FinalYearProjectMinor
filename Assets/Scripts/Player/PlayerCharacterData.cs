using UnityEngine;
using System.Collections;

public class PlayerCharacterData 
{
    string name;
    uint playerID;
    uint win, lose;
    uint charPts;

    public void Set(string characterName, uint playerID, uint wins, uint loses, uint charPts)
    {
        this.name = characterName;
        this.playerID = playerID;
        this.win = wins;
        this.lose = loses;
        this.charPts = charPts;
    }

    void WinandLose() { Win(); Lose(); }
    void Win() { win++; }
    void Lose() { lose++; }

    float GetWinPercentage() { return (win/win+lose) * 100.0f; }
    uint GetWins() { return win; }
    uint GetLose() { return lose; }
    uint GetCharPts() { return charPts; }

    void UsePoints(uint points)
    {
        if (charPts >= points)
        {
            charPts -= points;
            Debug.Log("Points Used!");
        }
    }
}
