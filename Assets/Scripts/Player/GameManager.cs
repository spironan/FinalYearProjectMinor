using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour 
{
    GAMESTATE currState;
    //Players
    PLAYER playerCount = PLAYER.PLAYER_BEGIN;
    List<PlayerData> PlayerList = new List<PlayerData>();
    GameObject MasterPlayer = null;

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

    public GAMESTATE GetGameState() { return currState; }

    //Each Scene Should have their own "Head of Department" that will call this code once to change scene
    public void ChangeState(GAMESTATE state)
    {
        if (currState != state)
            currState = state;
        Debug.Log("State Changed to : " + currState);
    }

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
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
        playerCount++;
        GameObject Master = Instantiate(gameObject);
        gameObject.transform.parent = transform;
        PlayerData masterData = Master.AddComponent<PlayerData>();
        masterData.SetPlayerID(playerCount);
        PlayerList.Add(masterData);
        return Master;
    }

}
