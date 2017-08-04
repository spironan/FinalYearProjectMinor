using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviourSingletonPersistent<GameManager> 
{
    //Prefab for PlayerBase
    GameObject playerBasePrefab;
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
    //Current Random Player
    PLAYER curRandomPlayer = PLAYER.MAX_PLAYERS;

    //Dont destroy on load the manager -> exist permantly
    void Start()
    {
        playerBasePrefab = PrefabManager.GetInstance().GetPrefab("PlayerPrefab");
        //Create All Players
        for (int i = 0; i < (int)PLAYER.MAX_PLAYERS; ++i)
            CreateNewPlayer();
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
                SoundSystem.Instance.ChangeClip(AUDIO_TYPE.BACKGROUND_MUSIC, AudioClipManager.GetInstance().GetAudioClip("MainMenu"), true);
                break;
            case GAMESTATE.CHAR_SELECT:
                SoundSystem.Instance.ChangeClip(AUDIO_TYPE.BACKGROUND_MUSIC, AudioClipManager.GetInstance().GetAudioClip("CharSelect"), true);
                break;
            case GAMESTATE.IN_GAME:
                SoundSystem.Instance.ChangeClip(AUDIO_TYPE.BACKGROUND_MUSIC, AudioClipManager.GetInstance().GetAudioClip(currMap.GetMapName()), true);
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
    public PlayerData GetRandomPlayer()
    {
        Random.seed = System.DateTime.Now.Millisecond;
        curRandomPlayer = (PLAYER)Random.Range(0, (int)PLAYER.MAX_PLAYERS);
        return GetPlayer(curRandomPlayer);
    }
    public PLAYER GetCurRandomPlayer() { return curRandomPlayer; }
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
        }
        Debug.Log("Created Master Player");
        return masterPlayer;
    }
    GameObject CreateGuestPlayer()//Takes in a Controller Script
    {
        GameObject guest = CreatePlayer();
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


    //In Game Functions
    public void RestoreDefaults()
    {
        for (int i = 0; i < playerList.Count; ++i)
            playerList[i].ResetToDefaults();
    }
    public void StartNewMatch()
    {
        for (int i = 0; i < playerList.Count; ++i) 
            playerList[i].GetInGameData().StartMatch();
    }
    public void SetInGameWinCondition(short winsRequired)
    {
        for (int i = 0; i < playerList.Count; ++i)
            playerList[i].GetInGameData().SetWinCondition(winsRequired);
    }
    public PlayerData GetWinner()
    {
        for (int i = 0; i < playerList.Count; ++i)
        {
            if (playerList[i].GetInGameData().GetSetWon())
                return playerList[i];
        }

        Debug.Log("Cant Find A Winner");
        return null;
    }
    public PLAYER GetWinnerID()
    {
        return GetWinner().GetPlayerID();
    }
    public TEAM GetWinnerTeam()
    {
        return GetWinner().GetInGameData().GetTeam();
    }
    
}
