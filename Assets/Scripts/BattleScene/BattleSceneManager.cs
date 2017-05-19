using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//Should Be the one handling the Timer, which player spawns where
public class BattleSceneManager : MonoBehaviour
{
    GameManager gameManager;
    Map currentMap;
    List<GameObject> playerCharacters = new List<GameObject>();
    float curBattleTimer;
    float maxBattleTimer;
    GAME_MODES gameMode;

    public float GetCurrentBattleTimer() { return curBattleTimer; }

    public void SetGameMode(GAME_MODES mode)
    {
        switch (mode)
        {
            case GAME_MODES.LOCAL_PVP:
                maxBattleTimer = 99.0f;
                break;
        }
        ResetTimer();
    }

    BattleSceneManager() 
    {
    }

    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        currentMap = gameManager.GetCurrMap();
        SetGameMode(GAME_MODES.LOCAL_PVP);
    }

    public void LoadOncePerScene()
    {
        for (int i = 0; i < gameManager.GetPlayerSize(); ++i)
        {
            playerCharacters.Add(gameManager.GetPlayer(i).GetInGameData().GetChar().gameObject);
        }
    }

    //Called Once when a Player Starts
    public void StartBattle()
    {
        //Add in Animation Time,Counter Countdown Time next time here
        ResetTimer();
        SetPlayerSpawnPoints();
    }

    public void ResetMatch()
    {
        ResetPlayerCharacters();
        SetPlayerSpawnPoints();
        ResetTimer();
    }

    void ResetPlayerCharacters()
    {
        for (int i = 0; i < playerCharacters.Count; ++i)
        {
            playerCharacters[i].GetComponent<CharacterBase>().Reset();
        }
    }

    void SetPlayerSpawnPoints()
    {
        Vector3 spawnPoint;
        for (int i = 0; i < playerCharacters.Count; ++i)
        {
            spawnPoint = currentMap.GetSpawnLocation(i);
            playerCharacters[i].transform.position = spawnPoint;
        }
    }
    
    public void Update()
    {
        UpdateTimer();  
    }

    void UpdateTimer()
    {
        if (curBattleTimer > 0)
            curBattleTimer -= Time.deltaTime;
        else
        {
            EndMatch();
            ResetMatch();
        }
    }

    void ResetTimer()
    {
        curBattleTimer = maxBattleTimer;
    }

    public void EndMatch()
    {
        int winnerID = -1;
        uint winnerHP = 0;
        uint curPlayerHp = 0;
        for (int i = 0; i < playerCharacters.Count; ++i)
        {
            curPlayerHp = playerCharacters[i].GetComponent<CharacterBase>().GetHealth();
            if (curPlayerHp > winnerHP)
            {
                winnerHP = curPlayerHp;
                winnerID = i;
            }
        }
        if (winnerID != -1)
            gameManager.GetPlayer(winnerID).GetInGameData().WinMatch();
    }

}
