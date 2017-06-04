using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour 
{
    //Buffer Time The Player Needs to Press Before Actually Creating Player
    public float holdTime;
    float curHoldTime;

    //Prefab for PlayerBase
    public GameObject playerBasePrefab;
    //Frames Of The Different Players
    public GameObject[] frameObj = new GameObject[(int)TEAM.MAX_TEAM];
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
    PLAYER playerCount = PLAYER.PLAYER_BEGIN;
    //Current Team Of the Player
    TEAM playerTeam = TEAM.TEAM_BEGIN;

    //Dont destroy on load the manager -> exist permantly
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        GetComponent<PlayerControllerManager>().init(PLAYER.PLAYER_ONE);
    }
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space)
        //    ||  GetComponent<PlayerControllerManager>().getIsKeyDownHold(BUTTON_INPUT.START)
        //    )
        //{
            foreach (PlayerControllerManager controller in GetComponents<PlayerControllerManager>())
            {
                if (controller.getIsKeyDownHold(BUTTON_INPUT.START))
                {
                    curHoldTime += Time.deltaTime;
                    if (curHoldTime >= holdTime)
                    {
                        CreateNewPlayer();
                        curHoldTime = 0.0f;
                    }
                }
            }

        //}
    }
    
    //Setter(s)
    public void SetCurrMap(string mapName) { currMap = MapManager.GetInstance().GetMap(mapName); }
    //Each Scene Should have their own "Head of Department" that will call this code once to change scene
    public void ChangeState(GAMESTATE state)
    {
        if (currState != state)
            currState = state;
        Debug.Log("State Changed to : " + currState);
    }

    //Getter(s)
    public PlayerData GetPlayer(PLAYER playerNo) 
    {
        if (playerCount >= playerNo)
        {
            return playerList[(int)playerNo];
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
            return playerList[playerNo];

        return null;
    }
    public PlayerData GetPlayer(TEAM playerTeam)
    {
        foreach (PlayerData player in playerList)
        {
            if (player.GetInGameData().GetTeam() != playerTeam)
                continue;

            return player;
        }
        Debug.Log("No Player In Team : " + playerTeam + " Are you sure you entered the right one?");
        return null;
    }
    public PlayerData GetMasterPlayerData()
    {
        if (HasMasterPlayer())
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
        Debug.Log(playerCount + " before Increment ");
        GameObject player = Instantiate(playerBasePrefab);
        player.transform.parent = this.transform;
        PlayerData playerData = player.GetComponent<PlayerData>();
        playerData.SetPlayerID(playerCount);
        playerData.GetInGameData().SetTeam(playerTeam);
        playerData.selectframe = frameObj[(int)playerTeam];
        playerList.Add(playerData);
        playerTeam++;
        playerCount++;
        Debug.Log(playerCount + " after Increment ");
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
        return (playerCount < PLAYER.MAX_PLAYERS);
    }
}
