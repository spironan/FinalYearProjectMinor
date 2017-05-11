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
    PlayerInGameData inGameData;

    //Player Char Select Data
    bool pickedChar = false;

    public bool GetPickStatus()
    {
        return pickedChar;
    }

    public void PickChar()
    {
        pickedChar = true;
    }

    public void UnPickChar()
    {
        pickedChar = false;
    }

    public void ResetCharSelect()
    {
        pickedChar = false;
    }


    //Getter(s)
    public PlayerCharacterData GetCharData(CHARACTERS chara) { return characterData[(int)chara]; }
    public PlayerCharacterData GetCharData(int chara) { return characterData[chara]; }
    public PlayerInGameData GetInGameData() { return inGameData; }

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
