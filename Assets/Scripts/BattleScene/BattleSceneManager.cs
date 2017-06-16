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
    List<VictoryDisplayScript> victoryInterface = new List<VictoryDisplayScript>();
    GameObject endDisplay;
    float curBattleTimer;
    float maxBattleTimer;
    GAME_MODES gameMode;
    bool gameWon = false;
    bool timePaused = false;
    bool gameEnd = false;
    int currentRound = 1;
    int noOfPlayers;

    public void SetGameMode(GAME_MODES mode)
    {
        switch (mode)
        {
            case GAME_MODES.PRACTICE:
                maxBattleTimer = 100000.0f;
                break;
            case GAME_MODES.VS_AI:
            case GAME_MODES.LOCAL_PVP:
                maxBattleTimer = 60.0f;
                break;
        }
        ResetTimer();
    }

    public float GetCurrentBattleTimer() { return curBattleTimer; }
    public int GetCurrentRound() { return currentRound; }

    void Awake()
    {
        endDisplay = GameObject.FindWithTag("EndDisplay");
        endDisplay.SetActive(false);
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        gameManager.ChangeState(GAMESTATE.IN_GAME);
        noOfPlayers = gameManager.GetPlayerSize();
        preBattleText = GameObject.FindGameObjectWithTag("PreBattleText").GetComponent<PreBattleTextScript>();
        foreach (GameObject display in GameObject.FindGameObjectsWithTag("VictoryDisplay"))
            victoryInterface.Add(display.GetComponent<VictoryDisplayScript>());
        currentMap = gameManager.GetCurrMap();
        SetGameMode(gameManager.GetGameMode());
        currentRound = 1;
        LoadOncePerScene();
    }

    void Start()
    {
        StartBattle();
    }

    public void LoadOncePerScene()
    {
        for (int i = 0; i < noOfPlayers; ++i)
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
        //Add in Animation Time, Counter Countdown Time next time here
        //ResetPlayerCharacters();
        SetPlayerSpawnPoints();
        ResetTimer();
        preBattleText.PlayAnim(currentRound);
    }

    public void ResetMatch()
    {
        ++currentRound;
        preBattleText.PlayAnim(currentRound);
        ResetPlayerCharacters();
        SetPlayerSpawnPoints();
        ResetTimer();
    }

    public void ResetEntireSet()
    {
        preBattleText.ResetAnim();
        for (int i = 0; i < noOfPlayers; ++i)
        {
            gameManager.GetPlayer(i).GetInGameData().StartMatch();// Reset Matches
        }
        foreach (GameObject player in playerCharacters)
        {
            player.GetComponent<PlayerCharacterLogicScript>().StartUpdate(); // Start Updating Players again
            player.GetComponent<PlayerCharacterLogicScript>().EnableAllAttacks();
        }
        endDisplay.SetActive(false);
        gameEnd = false;
        currentRound = 1;
        ResetPlayerCharacters();
        StartBattle();
    }

    void ResetPlayerCharacters()
    {
        for (int i = 0; i < noOfPlayers; ++i)
        {
            playerCharacters[i].GetComponent<PlayerCharacterLogicScript>().Reset();
        }
    }

    void SetPlayerSpawnPoints()
    {
        for (int i = 0; i < noOfPlayers; ++i)
        {
            playerCharacters[i].transform.position = currentMap.GetSpawnLocation(i);
        }
    }

    void ResetTimer()
    {
        gameWon = false;
        timePaused = false;
        curBattleTimer = maxBattleTimer;
    }

    public void Update()
    {
        if (!gameEnd)
        {
            UpdateTimer();
            CheckForDeath();

            if (gameWon)
                EndMatch();
        }
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
        for (int i = 0; i < noOfPlayers; ++i)
        {
            if (playerCharacters[i].GetComponent<PlayerCharacterLogicScript>().IsDead())
            {
                gameWon = true;
                break;
            }
        }
    }

    public void PauseTimer() { timePaused = true; }
    public void UnPauseTimer() { timePaused = false; }
    public bool IsPaused() { return timePaused; }

    public void EndMatch()
    {
        bool doubleKO = true;
        for (int i = 0; i < noOfPlayers; ++i)
        {
            if (!playerCharacters[i].GetComponent<PlayerCharacterLogicScript>().IsDead())
            {
                doubleKO = false;
                gameManager.GetPlayer(i).GetInGameData().WinMatch();
                victoryInterface[i].WinMatch();
                break;
            }
        }
        if (doubleKO)
        {
            for (int i = 0; i < noOfPlayers; ++i)
            {
                gameManager.GetPlayer(i).GetInGameData().WinMatch();
                victoryInterface[i].WinMatch();
            }
        }

        bool displayEndScreen = false;
        for (int i = 0; i < noOfPlayers; ++i)
        {
            if (gameManager.GetPlayer(i).GetInGameData().GetMatchWins() >= 2)
            {
                displayEndScreen = true;
                break;
            }
        }

        if (displayEndScreen)
            DisplayWinResult();
        else
            ResetMatch();
    }

    public GameObject GetPlayerCharacter(int id)
    {
        if (id >= 0 && id < noOfPlayers)
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

    void DisplayWinResult()
    {
        preBattleText.FinishAnim();
        foreach (GameObject player in playerCharacters)
        { 
            player.GetComponent<PlayerCharacterLogicScript>().StopUpdate();
            player.GetComponent<PlayerCharacterLogicScript>().DisableAndResetAllAttacks();
        }

        foreach (VictoryDisplayScript victoryUI in victoryInterface)
            victoryUI.ResetVictories();
        
        gameEnd = true;
        ResetTimer();
        PauseTimer();
        endDisplay.SetActive(true);
        endDisplay.GetComponentInChildren<EndDisplayScript>().Reset();
    }

}
