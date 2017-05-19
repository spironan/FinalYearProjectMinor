using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour 
{
    //Current State Of The Game
    GAMESTATE currState;
    //Current Number Of Players
    PLAYER playerCount = PLAYER.PLAYER_BEGIN;
    TEAM playerTeam = TEAM.TEAM_BEGIN;
    public GameObject[] frameObj = new GameObject[(int)TEAM.MAX_TEAM];
    List<PlayerData> PlayerList = new List<PlayerData>();
    GameObject MasterPlayer = null;
    public GameObject PlayerBasePrefab;
    Map playMapData = null;

    //Dont destroy on load the manager -> exist permantly
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        GetComponent<PlayerControllerManager>().init(PLAYER.PLAYER_ONE);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)
            ||GetComponent<PlayerControllerManager>().getIsKeyDown(BUTTON_INPUT.START)
            )
        {
            CreateNewPlayer();
        }
    }

    public void LoadBattleScene()
    {
        if (playMapData != null)
            LoadingScreenManager.LoadScene(playMapData.mapName);
    }
    public void SetMap(Map newMap) { playMapData = newMap; }
    public Map GetMap() { return playMapData; }

    //Getter(s)
    //public PlayerData[] GetPlayers() { return PlayerList; }
    public PlayerData GetPlayer(PLAYER playerNo) 
    {
        if (playerCount >= playerNo)
        {
            return PlayerList[(int)playerNo]; 
        }
        Debug.Log("There is No such Player of Index : " + playerCount);
        return null;
    }
    public PlayerData GetPlayer(int playerNo) 
    {
        if (playerNo < 0)
            Debug.Log("PlayerNo must be Positive");
        else if (playerNo > (int)playerCount)
            Debug.Log("There is No such Player of Index : " + playerCount);
        else 
            return PlayerList[playerNo];

        return null;
    }
    public int GetPlayerSize()
    {
        return PlayerList.Count;
    }

    public GAMESTATE GetGameState() { return currState; }
    //Each Scene Should have their own "Head of Department" that will call this code once to change scene
    public void ChangeState(GAMESTATE state)
    {
        if (currState != state)
            currState = state;
        Debug.Log("State Changed to : " + currState);
    }

    public PlayerData GetMasterPlayerData()
    {
        if (HasMasterPlayer())
        {
            Debug.Log("No Master Player Found,Please Create one");
            return null;
        }

        return MasterPlayer.GetComponent<PlayerData>();
    }
    public GameObject CreateMasterPlayer()//Takes in a Controller Script
    {
        MasterPlayer = CreateGuestPlayer();
        if (MasterPlayer != null)// if can create
        {
            MasterPlayer.GetComponent<PlayerData>().IsMaster();
            //Add controller support here
        }
        return MasterPlayer;
    }
    public GameObject CreateGuestPlayer()//Takes in a Controller Script
    {
        GameObject guest = CreatePlayer();
        //guest.AddComponent<ControllerSupport>();
        return guest;
    }
    public bool TransferMasterPlayer(PLAYER playerNo)
    {
        if (!HasMasterPlayer())
        { 
            Debug.Log("Already a Master Player,Cant switch with yourself");
            return false;
        }

        if (MasterPlayer.GetComponent<PlayerData>().GetPlayerID() == playerNo)
        {
            Debug.Log("Already a Master Player,Cant switch with yourself");
            return false;
        }

        //swap here
        MasterPlayer.AddComponent<PlayerData>().IsGuest();
        GetPlayer(playerNo).IsMaster();
        Debug.Log("Swapping of Master Successful " + playerNo + " is now the master");
        return true;
    }

    void CreateNewPlayer()
    {
        if (!HasMasterPlayer())
            CreateMasterPlayer();
        else
            CreateGuestPlayer();
    }

    bool HasMasterPlayer()
    {
        return MasterPlayer != null;
    }
    bool CanCreatePlayer()
    {
        return (playerCount < PLAYER.MAX_PLAYERS);
    }
    GameObject CreatePlayer()
    {
        if (!CanCreatePlayer())
        {
            Debug.Log("Max Number Of Players Created, Cant Create More!");
            return null;
        }
        Debug.Log(playerCount + " before Increment ");
        GameObject player = Instantiate(PlayerBasePrefab);
        player.transform.parent = this.transform;
        PlayerData playerData = player.GetComponent<PlayerData>();
        playerData.SetPlayerID(playerCount);
        playerData.GetInGameData().SetTeam(playerTeam);
        playerData.selectframe = frameObj[(int)playerTeam];
        PlayerList.Add(playerData);
        playerTeam++;
        playerCount++;
        Debug.Log(playerCount + " after Increment ");
        return player;
    }

}
