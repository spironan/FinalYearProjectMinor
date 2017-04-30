using UnityEngine;
using System.Collections;

//Should Be the one handling the Timer, which player spawns where
public class BattleSceneManager : MonoBehaviour
{
    //Number of Players
    PlayerData[] players = new PlayerData[(int)PLAYER.MAX_PLAYERS];
    Vector3[] spawnPositions = new Vector3[(int)PLAYER.MAX_PLAYERS];
    float curBattleTimer;
    float maxBattleTimer;
    GAME_MODES gameMode;

    BattleSceneManager() 
    {

    }

    public float GetCurrentBattleTimer() { return curBattleTimer; } 

    public void SetGameMode(GAME_MODES mode)
    {
        switch (mode)
        {
        case GAME_MODES.LOCAL_PVP :
                maxBattleTimer = 90.0f;
            break;
        }
        ResetTimer();
    }

    void Start()
    {
        SetGameMode(GAME_MODES.LOCAL_PVP);
    }

    //Called Once when a Player Starts
    public void StartBattle()
    {
        ResetTimer();
        SetPlayerSpawnPoints();
    }

    public void SetPlayerSpawnPoints()
    {
        Vector3 spawnPoint;
        for (int i = 0; i < (int)PLAYER.MAX_PLAYERS; ++i)
        {
            spawnPoint = spawnPositions[i];
            players[i].GetData().GetChar().transform.position.Set(spawnPoint.x, spawnPoint.y, spawnPoint.z);
        }
    }

    public void Update()
    {
        UpdateTimer();  
    }

    public void UpdateTimer()
    {
        if (curBattleTimer > 0)
            curBattleTimer -= Time.deltaTime;
        else
        {
            EndMatch();
            ResetTimer();
        }
    }

    public void EndMatch()
    {
        int winnerID = -1;
        uint winnerHP = 0;
        uint curPlayerHp = 0;
        for (int i = (int)PLAYER.PLAYER_ONE; i < (int)PLAYER.MAX_PLAYERS; ++i)
        {
            curPlayerHp = players[i].GetData().GetChar().GetHealth();
            if (curPlayerHp > winnerHP)
            {
                winnerHP = curPlayerHp;
                winnerID = i;
            }
        }
        if (winnerID >= (int)PLAYER.MAX_PLAYERS)
            players[winnerID].GetData().WinMatch();
    }

    public void ResetTimer()
    {
        curBattleTimer = maxBattleTimer;
    }

}
