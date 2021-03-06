﻿using UnityEngine;
using System.Collections;

public class PlayerData : MonoBehaviour
{
    //Unique Player ID <- Important , used to identify that particular player
    uint UniquePlayerID;
    //PlayerName <- Name of the Player           
    string PlayerName;
    //Player In Game ID
    PLAYER playerID;
    //Controller For Reading Input
    public ListOfControllerActions controller = null;
    //the frame outline of the player
    public GameObject selectframe;
    // Character Data is the progression of the data, read from Database As well next time                               
    PlayerCharacterData[] characterData = new PlayerCharacterData[CharacterManager.GetInstance().GetCharCount()];
    // Player's In Game Data <- Determine which side player is Spawned and used for in game stuff
    PlayerInGameData inGameData = new PlayerInGameData();
    //whether the player is master or guest
    bool isMaster = false;
    //Whether Player Has Already Assigned itself To be Which Player
    bool assigned = false;
    //Whether Player Has Picked A Character in Character Select
    bool pickedChar = false;
    //Whether Player is A CPU or Not
    bool isCPU = false;

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
    public PLAYER GetPlayerID() { return playerID; }

    public void MakeCPU() { isCPU = true; }
    public void MakeHuman() { isCPU = false; }
    public void ConnectController(ListOfControllerActions controller) { /*if (controller == null)*/ this.controller = controller; }
    public void DisconnectController() { if (controller != null) this.controller = null; }
    public void SwapControllers(PlayerData player) { ListOfControllerActions temp = controller; this.controller = player.controller; player.controller = temp; }
    public bool IsCPU() { return isCPU; }

    public void Assign() { assigned = true; }
    public void UnAssign() { assigned = false; }
    public bool IsAssigned() { return assigned; }

    public void IsMaster() { isMaster = true; }
    public void IsGuest() { isMaster = false; }
    public bool GetMasterStatus() { return isMaster; }

    public bool GetPickStatus() { return pickedChar; }
    public void PickChar() { pickedChar = true; }
    public void UnPickChar() { pickedChar = false; }
    public void ResetCharSelect() { pickedChar = false; }

    public void ResetToDefaults() { assigned = pickedChar = isCPU = false; }
    //Getter(s)
    //public PlayerCharacterData GetCharData(CHARACTERS chara) { return characterData[(int)chara]; }
    public PlayerCharacterData GetCharDataByName(string charName) 
    {
        int index = CharacterManager.GetInstance().GetCharacterIndex(charName);
        if(index != -1)
        {
            return characterData[CharacterManager.GetInstance().GetCharacterIndex(charName)]; 
        }
        Debug.Log("name you passed in : " + charName + "  is invalid,no such character found");
        return null;
    }
    public PlayerCharacterData GetCharDataByIndex(int index) 
    {
        if (index >= 0 && index <= CharacterManager.GetInstance().GetCharCount())
        {
            return characterData[index]; 
        }
        Debug.Log("index you passed in is invalid at : " + index + ", it is either too low or is bigger then total no of charas at : " + CharacterManager.GetInstance().GetCharCount());
        return null;
    }
    public PlayerInGameData GetInGameData() { return inGameData; }

	// Use this for initialization
	void Start ()
    {
        //On Initialization should determine player Unique ID and Name
        controller = GetComponent<ListOfControllerActions>();
        Debug.Log("Finished Initializing Player Data for :" + playerID);
	}

	// Update is called once per frame
	void Update () 
    {
	    //Used to Update The Progression of the Players
	}

}
