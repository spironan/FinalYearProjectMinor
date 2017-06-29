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
    GameObject pauseDisplay, endDisplay;
    SoundSystem soundSystem;
    float curBattleTimer;
    float maxBattleTimer;
    GAME_MODES gameMode;
    bool gameWon = false;
    bool timePaused = false;
    bool gameEnd = false;
    bool gamePaused = false;
    int currentRound = 1;
    int noOfPlayers = 0;

    public void SetGameMode(GAME_MODES mode)
    {
        switch (mode)
        {
            case GAME_MODES.PRACTICE:
                SetWinCondition(1);
                maxBattleTimer = 100000.0f;
                break;
            case GAME_MODES.VS_AI:
            case GAME_MODES.LOCAL_PVP:
                SetWinCondition(2);
                maxBattleTimer = 60.0f;
                break;
        }
        ResetTimer();
    }

    void SetWinCondition(short wins)
    {
        for (int i = 0; i < noOfPlayers; ++i)
        {
            gameManager.GetPlayer(i).GetInGameData().SetWinCondition(wins);
        }
    }

    public float GetCurrentBattleTimer() { return curBattleTimer; }
    public int GetCurrentRound() { return currentRound; }

    void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        gameManager.ChangeState(GAMESTATE.IN_GAME);
        soundSystem = GameObject.FindWithTag("SoundSystem").GetComponent<SoundSystem>();
        noOfPlayers = gameManager.GetPlayerSize();
        preBattleText = GameObject.FindGameObjectWithTag("PreBattleText").GetComponent<PreBattleTextScript>();
        foreach (GameObject display in GameObject.FindGameObjectsWithTag("VictoryDisplay"))
            victoryInterface.Add(display.GetComponent<VictoryDisplayScript>());
        currentMap = gameManager.GetCurrMap();
        SetGameMode(gameManager.GetGameMode());
        endDisplay = GameObject.FindWithTag("EndDisplay");
        endDisplay.SetActive(false);
        pauseDisplay = GameObject.FindWithTag("PauseDisplay");
        pauseDisplay.SetActive(false);
        currentRound = 1;
        LoadOncePerScene();
    }

    void Start()
    {
        StartBattle();
    }

    public void LoadOncePerScene()
    {
        for (TEAM currentTeam = TEAM.RED_TEAM; (int)currentTeam < noOfPlayers; ++currentTeam)
        {
            PlayerData player = gameManager.GetPlayer(currentTeam);
            GameObject character = PrefabManager.GetInstance().GetPrefab(player.GetInGameData().GetCharName());
            character.GetComponent<PlayerCharacterLogicScript>().SetCharacter(player.GetInGameData().GetCharName());
            character.GetComponent<PlayerCharacterLogicScript>().SetPlayerID(player.GetPlayerID());
            character.GetComponent<PlayerCharacterLogicScript>().SetController(player.gameObject.GetComponent<PlayerControllerManager>());
            character.GetComponent<SkillActivator>().bindedActions = player.gameObject.GetComponent<ListOfControllerActions>();
            playerCharacters.Add(character);
        }

        //TEAM currentTeam = TEAM.RED_TEAM;
        //for (int i = 0; i < noOfPlayers; ++i)
        //{
        //    PlayerData player = gameManager.GetPlayer(currentTeam);
        //    GameObject character = PrefabManager.GetInstance().GetPrefab(player.GetInGameData().GetCharName());
        //    character.GetComponent<PlayerCharacterLogicScript>().SetCharacter(player.GetInGameData().GetCharName());
        //    character.GetComponent<PlayerCharacterLogicScript>().SetPlayerID(player.GetPlayerID());
        //    character.GetComponent<PlayerCharacterLogicScript>().SetController(player.gameObject.GetComponent<PlayerControllerManager>());
        //    character.GetComponent<SkillActivator>().bindedActions = player.gameObject.GetComponent<ListOfControllerActions>();
        //    playerCharacters.Add(character);
        //    currentTeam++;
        //}
    }

    //Called Once when a Player Starts
    public void StartBattle()
    {
        //ResetPlayerCharacters();
        SetPlayerSpawnPoints();
        ResetTimer();
        preBattleText.PlayAnim(currentRound);
    }

    void PauseGame(int playerID)
    {
        gamePaused = true;

        Time.timeScale = 0.0f;
        pauseDisplay.SetActive(true);
        pauseDisplay.GetComponentInChildren<PauseMenuScript>().Pause(playerID);
        foreach (GameObject player in playerCharacters)
        {
            player.GetComponent<PlayerCharacterLogicScript>().StopUpdate();
            player.GetComponent<PlayerCharacterLogicScript>().DisableAllAttacks();
        }
    }

    public void UnPauseGame()
    {
        gamePaused = false;

        Time.timeScale = 1.0f;
        pauseDisplay.SetActive(false);
        foreach (GameObject player in playerCharacters)
        {
            player.GetComponent<PlayerCharacterLogicScript>().StartUpdate(); // Start Updating Players again
            player.GetComponent<PlayerCharacterLogicScript>().EnableAllAttacks();
        }
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
    }

    public void Rematch()
    {
        ResetEntireSet();
        foreach (GameObject player in playerCharacters)
        {
            player.GetComponent<PlayerCharacterLogicScript>().StartUpdate(); // Start Updating Players again
            player.GetComponent<PlayerCharacterLogicScript>().EnableAllAttacks();
        }
        endDisplay.SetActive(false);
        gameEnd = false;
        currentRound = 1;
        soundSystem.ChangeClip(AUDIO_TYPE.BACKGROUND_MUSIC,AudioClipManager.GetInstance().GetAudioClip(currentMap.GetMapName()),true);
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
        if (!gameEnd && !gamePaused)
        {
            UpdateTimer();
            CheckForDeath();

            if (gameWon)
                EndMatch();

            CheckForPause();
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

    void CheckForPause()
    {
        for (int i = 0; i < noOfPlayers; ++i)
        {
            if (gameManager.GetPlayer(i).controller.getButtonAction(ACTIONS.START))
            {
                PauseGame(i);
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
            if (playerCharacters[i].GetComponent<PlayerCharacterLogicScript>().IsDead())
                continue;

            doubleKO = false;
            gameManager.GetPlayer(i).GetInGameData().WinMatch();
            victoryInterface[i].WinMatch();
        }
        if (doubleKO)
        {
            for (int i = 0; i < noOfPlayers; ++i)
            {
                gameManager.GetPlayer(i).GetInGameData().WinMatch();
                victoryInterface[i].WinMatch();
            }
        }

        //Determine if Set Won By Anyone
        bool displayEndScreen = false;
        for (int i = 0; i < noOfPlayers; ++i)
        {
            if (gameManager.GetPlayer(i).GetInGameData().GetSetWon())
            {
                SetPlayerSpawnPoints();
                DisplayWinResult();
                displayEndScreen = true;
                break;
            }
        }

        if (!displayEndScreen)
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
