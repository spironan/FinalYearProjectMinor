using UnityEngine;
using System.Collections;

public class PlayerCharacterData : MonoBehaviour 
{
    CHARACTERS name;
    uint playerID;
    uint win, lose;
    uint charPts;

    void UsePoints(uint points)
    {
        if (charPts >= points)
        {
            charPts -= points;
            Debug.Log("Points Used");
        }
    }
    uint GetWins() { return win; }
    uint GetLose() { return lose; }

}
