using UnityEngine;
using System.Collections;

public class PlayerCharacterData : MonoBehaviour 
{
    //CHARACTERS name;
    string name;
    uint playerID;
    uint win, lose;
    uint charPts;

    void awake()
    {
        //name = CHARACTERS.MAX_CHARACTER;
        name = "";
        playerID = win = lose = charPts = 0;
    }

    void WinandLose() { Win(); Lose(); }
    void Win() { win++; }
    void Lose() { lose++; }

    float GetWinPercentage() { return (win/win+lose)*100.0f; }
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
