﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour 
{
    //Buffer Time The Player Needs to Press Before Actually Creating Player
    //public float holdTime;
    //float curHoldTime;

    //Prefab for PlayerBase
    public GameObject playerBasePrefab;
    //Frames Of The Different Players
    //public GameObject[] frameObj = new GameObject[(int)TEAM.MAX_TEAM];
    //List Of Existing Players
    List<PlayerData> playerList = new List<PlayerData>();
    //Master Player Has More Rights then Guest
    GameObject masterPlayer = null;
    //Which Map is Being Selected
    Map currMap = null;
    //Current State Of The Game
    GAMESTATE currState;
    //Current Game Mode
    GAME_MODES currGameMode = GAME_MODES.LOCAL_PVP;
    //Current Number Of Players
    PLAYER playerCount = PLAYER.PLAYER_ONE;
    //Current Team Of the Player
    //TEAM playerTeam = TEAM.TEAM_BEGIN;
    //SoundController soundController;
    SoundSystem soundSystem;
    //Dont destroy on load the manager -> exist permantly
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        //Let Player 1 Control The Main Menu Regardless Is Who
        //GetComponent<PlayerControllerManager>().init(PLAYER.PLAYER_ONE);
        //Create All Players
        for (int i = 0; i < (int)PLAYER.MAX_PLAYERS; ++i)
            CreateNewPlayer();

        //soundController = GameObject.FindWithTag("SoundController").GetComponent<SoundController>();
        soundSystem = GameObject.FindWithTag("SoundSystem").GetComponent<SoundSystem>();
    }

    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space)
        //    ||  GetComponent<PlayerControllerManager>().getIsKeyDownHold(BUTTON_INPUT.START)
        //    )
        //{
            //foreach (PlayerControllerManager controller in GetComponents<PlayerControllerManager>())
            //{
            //    if (controller.getIsKeyDownHold(BUTTON_INPUT.START))
            //    {
            //        curHoldTime += Time.deltaTime;
            //        if (curHoldTime >= holdTime)
            //        {
            //            CreateNewPlayer();
            //            curHoldTime = 0.0f;
            //        }
            //    }
            //}
        //}
    }
    
    //Setter(s)
    public void SetCurrMap(string mapName) { currMap = MapManager.GetInstance().GetMap(mapName); }
    //Each Scene Should have their own "Head of Department" that will call this code once to change scene
    public void ChangeState(GAMESTATE state)
    {
        if (currState != state)
            currState = state;

        switch (currState)
        { 
            case GAMESTATE.MAIN_MENU:
                //soundController.ChangeBGM(AudioClipManager.GetInstance().GetAudioClip("MainMenu"));
                soundSystem.ChangeClip(AUDIO_TYPE.BACKGROUND_MUSIC, AudioClipManager.GetInstance().GetAudioClip("MainMenu"), true);
                break;
            case GAMESTATE.CHAR_SELECT:
                //soundController.ChangeBGM(AudioClipManager.GetInstance().GetAudioClip("CharSelect"));
                soundSystem.ChangeClip(AUDIO_TYPE.BACKGROUND_MUSIC, AudioClipManager.GetInstance().GetAudioClip("CharSelect"), true);
                break;
            case GAMESTATE.IN_GAME:
                //soundController.ChangeBGM(AudioClipManager.GetInstance().GetAudioClip(currMap.GetMapName()));
                soundSystem.ChangeClip(AUDIO_TYPE.BACKGROUND_MUSIC, AudioClipManager.GetInstance().GetAudioClip(currMap.GetMapName()), true);
                break;
        }

        Debug.Log("State Changed to : " + currState);
    }
    //Set The Game Mode Of which Game Type Is chosen
    public void SetGameMode(GAME_MODES mode) 
    {
        if(currGameMode != mode)
            this.currGameMode = mode;

        Debug.Log("GameMode Changed to : " + currGameMode);

    }

    //Getter(s)
    public PlayerData GetPlayer(PLAYER playerNo) 
    {
        if (playerList.Count >= (int)playerNo)
        {
            return playerList[(int)playerNo];
        }
        Debug.Log("There is No such Player of Index : " + playerNo);
        return null;
    }
    public PlayerData GetPlayer(int playerNo) 
    {
        if (playerNo < 0)
            Debug.Log("PlayerNo must be Positive");
        else if (playerNo > playerList.Count)
            Debug.Log("There is No such Player of Index : " + playerNo);
        else 
            return playerList[playerNo];

        return null;
    }
    public PlayerData GetPlayer(TEAM playerTeam)
    {
        foreach (PlayerData player in playerList)
        {
            if (!player.IsAssigned())
                continue;

            if (player.GetInGameData().GetTeam() != playerTeam)
                continue;

            return player;
        }
        Debug.Log("No Player In Team : " + playerTeam + " Are you sure you entered the right one?");
        return null;
    }
    public PlayerData GetMasterPlayerData()
    {
        if (!HasMasterPlayer())
        {
            Debug.Log("No Master Player Found, Please Create one");
            return null;
        }

        return masterPlayer.GetComponent<PlayerData>();
    }
    public Map GetCurrMap() { return currMap; }
    public int GetPlayerSize() { return playerList.Count; }
    public GAMESTATE GetGameState() { return currState; }
    public GAME_MODES GetGameMode() { return currGameMode; }

    public void CreateNewPlayer()
    {
        if (!HasMasterPlayer())
            CreateMasterPlayer();
        else
            CreateGuestPlayer();
    }
    GameObject CreateMasterPlayer()//Takes in a Controller Script
    {
        masterPlayer = CreatePlayer();
        if (masterPlayer != null)// if can create
        {
            masterPlayer.GetComponent<PlayerData>().IsMaster();
            //masterPlayer.AddComponent<PlayerControllerManager>
            //Add controller support here
        }
        Debug.Log("Created Master Player");
        return masterPlayer;
    }
    GameObject CreateGuestPlayer()//Takes in a Controller Script
    {
        GameObject guest = CreatePlayer();
        //guest.AddComponent<ControllerSupport>();
        Debug.Log("Created Guest Player");
        return guest;
    }
    GameObject CreatePlayer()
    {
        if (!CanCreatePlayer())
        {
            Debug.Log("Max Number Of Players Created, Cant Create More!");
            return null;
        }
        GameObject player = Instantiate(playerBasePrefab);
        PlayerData playerData = player.GetComponent<PlayerData>();
        playerData.SetPlayerID(playerCount);
        player.transform.parent = this.transform;
        playerList.Add(playerData);
        playerCount++;
        //playerData.GetInGameData().SetTeam(playerTeam);
        //playerData.selectframe = frameObj[(int)playerTeam];
        //playerTeam++;
        Debug.Log("Player Created with Id Of : " + playerData.GetPlayerID());
        return player;
    }

    public bool TransferMasterPlayer(PLAYER playerNo)
    {
        if (!HasMasterPlayer())
        { 
            Debug.Log("Already a Master Player,Cant switch with yourself");
            return false;
        }

        if (masterPlayer.GetComponent<PlayerData>().GetPlayerID() == playerNo)
        {
            Debug.Log("Already a Master Player,Cant switch with yourself");
            return false;
        }

        //swap here
        masterPlayer.AddComponent<PlayerData>().IsGuest();
        GetPlayer(playerNo).IsMaster();
        Debug.Log("Swapping of Master Successful " + playerNo + " is now the master");
        return true;
    }
    bool HasMasterPlayer()
    {
        return masterPlayer != null;
    }
    bool CanCreatePlayer()
    {
        return (playerList.Count < (int)PLAYER.MAX_PLAYERS);
    }

}
