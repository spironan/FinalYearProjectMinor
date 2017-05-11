using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour 
{
    GAMESTATE currState;
    //Players
    PlayerData[] Players = new PlayerData[(int)PLAYER.MAX_PLAYERS];

    //Getter(s)
    public PlayerData[] GetPlayers() { return Players; }
    public PlayerData GetPlayer(PLAYER playerNo) { return Players[(int)playerNo]; }
    public PlayerData GetPlayer(int playerNo) { return Players[playerNo]; }
    public GAMESTATE GetGameState() { return currState; }
    
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void ChangeState(GAMESTATE state)
    {
        if (currState != state)
            currState = state;
        Debug.Log("State Changed to : " + currState);
    }

}
