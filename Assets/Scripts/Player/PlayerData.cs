using UnityEngine;
using System.Collections;

public class PlayerData : MonoBehaviour
{
    //Unique Player ID <- Important , used to identify that particular player
    uint UniquePlayerID;
    //Player In Game ID
    PLAYER playerID;
    //PlayerName <- Name of the Player           
    string PlayerName;   
    //whether the player is master or guest
    bool isMaster = false;
    //Player Char Select Data
    bool pickedChar = false;
    //Controller For Reading Input
    public ListOfControllerActions controller;
    //PlayerControllerManager controller;
    //the frame outline of the player
    public GameObject selectframe;                                                          
    // Character Data is the progression of the data                                           read from Database As well next time                               
    PlayerCharacterData[] characterData = new PlayerCharacterData[(int)CHARACTERS.MAX_CHARACTER];
    // Player's In Game Data <- Determine which side player is Spawned and used for in game stuff
    PlayerInGameData inGameData = new PlayerInGameData();
    //skillbook

    //if i make a player holds what kind of button is being pressed, am i able to utilize through the fact that this is a player
    //holds all the virtual actions in bool of bitset
    //Gameobject find gamemanager 
    //gamemanager get player (1).getAction("START")
    //i hold the controller script

    //public PlayerControllerManager GetController()
    //{
    //    return controller;
    //}
    //public bool IsKeyDown(BUTTON_INPUT input)
    //{
    //    return controller.getIsKeyDown(input);
    //}
    //public bool IsJoyDown(JOYSTICK_AXIS_INPUT input)
    //{
    //    return controller.getValueFromAxis(input).getBool();
    //}
    

    public void SetPlayerID(PLAYER id)
    {
        playerID = id;
        PlayerControllerManager controller = GetComponent<PlayerControllerManager>();
        controller.init(playerID);
    }
    public PLAYER GetPlayerID()
    {
        return playerID;
    }

    public void IsMaster()
    {
        isMaster = true;
    }
    public void IsGuest()
    {
        isMaster = false;
    }
    public bool GetMasterStatus()
    {
        return isMaster;
    }

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
        controller = GetComponent<ListOfControllerActions>();
	}
	// Update is called once per frame
	void Update () 
    {
	    //Used to Update The Progression of the Players
	}

}
