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
    bool gameWon = false;
    int currentRound = 1;

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

    public float GetCurrentBattleTimer() { return curBattleTimer; }
    public int GetCurrentRound() { return currentRound; }

    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        currentMap = gameManager.GetCurrMap();
        //SetGameMode(GAME_MODES.LOCAL_PVP);
        SetGameMode(gameManager.GetGameMode());
        currentRound = 1;
    }

    public void LoadOncePerScene()
    {
        for (int i = 0; i < gameManager.GetPlayerSize(); ++i)
        {
            GameObject chara = new GameObject();
            PlayerCharacterLogicScript charaData = chara.AddComponent<PlayerCharacterLogicScript>();
            //TODO : ADD DATA TO CHARA DATA BASED ON PLAYER INFO

            //playerCharacters.Add(gameManager.GetPlayer(i).GetInGameData().GetCharData().gameObject);
        }
    }

    //Called Once when a Player Starts
    public void StartBattle()
    {
        //Add in Animation Time,Counter Countdown Time next time here
        ResetMatch();
    }

    public void ResetMatch()
    {
        currentRound++;
        ResetPlayerCharacters();
        SetPlayerSpawnPoints();
        ResetTimer();
    }

    void ResetPlayerCharacters()
    {
        for (int i = 0; i < playerCharacters.Count; ++i)
        {
            playerCharacters[i].GetComponent<PlayerCharacterLogicScript>().Reset();
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
        CheckForDeath();

        if (gameWon)
            EndMatch();
    }

    void UpdateTimer()
    {
        if (curBattleTimer > 0)
            curBattleTimer -= Time.deltaTime;
        else
            gameWon = true;
    }

    void CheckForDeath()
    {
        for (int i = 0; i < gameManager.GetPlayerSize(); ++i)
        {
            if (playerCharacters[i].GetComponent<PlayerCharacterLogicScript>().IsDead())
                gameWon = true;
        }
    }

    void ResetTimer()
    {
        gameWon = false;
        curBattleTimer = maxBattleTimer;
    }

    public void EndMatch()
    {
        int winnerID = -1;
        int winnerHP = 0;
        int curPlayerHp = 0;
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

        ResetMatch();
    }

}
