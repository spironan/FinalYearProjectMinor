using UnityEngine;
using System.Collections;

public class PlayerData : MonoBehaviour 
{
    //Unique Player ID <- Important , used to identify that particular player
    uint UniquePlayerID;                     
    //PlayerName <- Name of the Player           
    string PlayerName;                                                                    
    // Character Data is the progression of the data                                                                          
    PlayerCharacterData[] characterData = new PlayerCharacterData[(int)CHARACTERS.MAX_CHARACTER];
    // Player's In Game Data <- Determine which side player is Spawned and used for in game stuff
    PlayerInGameData data;

    //Getter(s)
    public PlayerInGameData GetData() { return data; }


	// Use this for initialization
	void Start () 
    {
	    //On Initialization should determine player Unique ID and Name
	}
	
	// Update is called once per frame
	void Update () {
	    //Used to Update The Progression of the Players
	}
}
