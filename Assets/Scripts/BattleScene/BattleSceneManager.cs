using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//Should Be the one handling the Timer, which player spawns where
public class BattleSceneManager : MonoBehaviour
{
    GameManager gameManager;
    Map currentMap;
    List<GameObject> playerCharacters = new List<GameObject>();
    PreBattleTextScript preBattleText;
    float curBattleTimer;
    float maxBattleTimer;
    GAME_MODES gameMode;
    bool gameWon = false;
    bool timePaused = false;
    int currentRound = 1;

    public void SetGameMode(GAME_MODES mode)
    {
        switch (mode)
        {
            case GAME_MODES.LOCAL_PVP:
                maxBattleTimer = 12.0f;
                break;
        }
        ResetTimer();
    }

    public float GetCurrentBattleTimer() { return curBattleTimer; }
    public int GetCurrentRound() { return currentRound; }

    void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        gameManager.ChangeState(GAMESTATE.IN_GAME);
        preBattleText = GameObject.FindGameObjectWithTag("PreBattleText").GetComponent<PreBattleTextScript>();
        currentMap = gameManager.GetCurrMap();
        SetGameMode(gameManager.GetGameMode());
        currentRound = 1;
        LoadOncePerScene();
        StartBattle();
    }

    public void LoadOncePerScene()
    {
        for (int i = 0; i < gameManager.GetPlayerSize(); ++i)
        {
            GameObject stunManHardcodingCauseWhyNot = PrefabManager.GetInstance().GetPrefab("StunMan"); //GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/PlayerPrefab/StunMan"));
            stunManHardcodingCauseWhyNot.GetComponent<PlayerCharacterLogicScript>().SetCharacter(gameManager.GetPlayer(i).GetInGameData().GetCharData());
            stunManHardcodingCauseWhyNot.GetComponent<PlayerCharacterLogicScript>().SetPlayerID(gameManager.GetPlayer(i).GetPlayerID());
            stunManHardcodingCauseWhyNot.GetComponent<PlayerCharacterLogicScript>().SetController(gameManager.GetPlayer(i).gameObject.GetComponent<PlayerControllerManager>());

            stunManHardcodingCauseWhyNot.GetComponent<SkillActivator>().player_number = gameManager.GetPlayer(i).GetPlayerID();
            stunManHardcodingCauseWhyNot.GetComponent<SkillActivator>().playerControllerManager = gameManager.GetPlayer(i).gameObject.GetComponent<PlayerControllerManager>();
            stunManHardcodingCauseWhyNot.GetComponent<SkillActivator>().bindedActions = gameManager.GetPlayer(i).gameObject.GetComponent<ListOfControllerActions>();
            playerCharacters.Add(stunManHardcodingCauseWhyNot);
        }
    }

    //Called Once when a Player Starts
    public void StartBattle()
    {
        //Add in Animation Time,Counter Countdown Time next time here
        ResetPlayerCharacters();
        SetPlayerSpawnPoints();
        ResetTimer();
    }

    public void ResetMatch()
    {
        currentRound++;
        ResetPlayerCharacters();
        SetPlayerSpawnPoints();
        preBattleText.PlayAnim();
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
        if (!timePaused)
        {
            if (curBattleTimer > 0)
                curBattleTimer -= Time.deltaTime;
            else
                gameWon = true;
        }
    }

    void CheckForDeath()
    {
        for (int i = 0; i < gameManager.GetPlayerSize(); ++i)
        {
            if (playerCharacters[i].GetComponent<PlayerCharacterLogicScript>().IsDead())
            {
                gameWon = true;
                break;
            }
        }
    }

    void ResetTimer()
    {
        gameWon = false;
        timePaused = false;
        curBattleTimer = maxBattleTimer;
    }

    public void PauseTimer() { timePaused = true; }
    public void UnPauseTimer() { timePaused = false; }

    public void EndMatch()
    {
        int winnerID = -1;
        int winnerHP = 0;
        int curPlayerHp = 0;
        for (int i = 0; i < playerCharacters.Count; ++i)
        {
            curPlayerHp = playerCharacters[i].GetComponent<PlayerCharacterLogicScript>().GetCharacterData().GetHealth();
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

    public GameObject GetPlayerCharacter(int id)
    {
        if (id >= 0 && id < playerCharacters.Count)
        {
            return playerCharacters[id];
        }
        Debug.Log("Id must be between 0 and " + playerCharacters.Count+ " You inputted : "+ id);
        return null;
    }

    public List<GameObject> GetPlayers()
    {
        return playerCharacters;
    }

}
