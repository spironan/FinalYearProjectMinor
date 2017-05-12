using UnityEngine;
using System.Collections;

public class PlayerCharacterData : MonoBehaviour 
{
    CHARACTERS name;
    uint playerID;
    uint win, lose;
    uint charPts;

    void awake()
    {
        name = CHARACTERS.MAX_CHARACTER;
        playerID = win = lose = charPts = 0;
    }

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
