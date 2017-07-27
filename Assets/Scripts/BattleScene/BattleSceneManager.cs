using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//Should Be the one handling the Timer, which player spawns where
public class BattleSceneManager : MonoBehaviour
{
    Map currentMap;
    List<GameObject> playerCharacters = new List<GameObject>();
    PreBattleTextScript preBattleText;
    List<VictoryDisplayScript> victoryInterface = new List<VictoryDisplayScript>();
    GameObject pauseDisplay, endDisplay;
    ListOfControllerActions loserController = null;

    //Game Mode And Timer
    GAME_MODES gameMode;
    float curBattleTimer;
    float maxBattleTimer;
    //GameStatus
    int noOfPlayers = 0;
    int currentRound = 1;
    bool gameWon = false;
    bool timePaused = false;
    bool gameEnd = false;
    bool timeOut = false;
    bool gamePaused = false;
    //Game Status 
    int winnerID = -1, loserID = -1;
    bool draw = false;

    public void SetGameMode(GAME_MODES mode)
    {
        switch (mode)
        {
            case GAME_MODES.PRACTICE:
                SetWinCondition(1);
                maxBattleTimer = 999.0f;
                break;
            case GAME_MODES.VS_AI:
            case GAME_MODES.LOCAL_PVP:
                SetWinCondition(2);
                maxBattleTimer = 60.0f;
                break;
        }
        ResetTimer();
    }

    void SetWinCondition(short wins) { GameManager.Instance.SetInGameWinCondition(wins); }

    public float GetCurrentBattleTimer() { return curBattleTimer; }
    public int GetCurrentRound() { return currentRound; }

    void Awake()
    {
        GameManager.Instance.ChangeState(GAMESTATE.IN_GAME);
        currentMap = GameManager.Instance.GetCurrMap();
        SetGameMode(GameManager.Instance.GetGameMode());
        noOfPlayers = GameManager.Instance.GetPlayerSize();

        preBattleText = GameObject.FindGameObjectWithTag("PreBattleText").GetComponent<PreBattleTextScript>();
        switch (GameManager.Instance.GetGameMode())
        {
            case GAME_MODES.PRACTICE:
            case GAME_MODES.VS_AI:
                preBattleText.gameObject.SetActive(false);
                break;
            case GAME_MODES.LOCAL_PVP:
                foreach (GameObject display in GameObject.FindGameObjectsWithTag("VictoryDisplay"))
                    victoryInterface.Add(display.GetComponent<VictoryDisplayScript>());
                break;
        }

        endDisplay = GameObject.FindWithTag("EndDisplay");
        endDisplay.SetActive(false);
        pauseDisplay = GameObject.FindWithTag("PauseDisplay");
        pauseDisplay.GetComponent<ToggleActiveScript>().ToggleActive(false);
        currentRound = 1;
        LoadOncePerScene();
    }

    public void LoadOncePerScene()
    {
        for (TEAM currentTeam = TEAM.RED_TEAM; (int)currentTeam < noOfPlayers; ++currentTeam)
        {
            PlayerData player = GameManager.Instance.GetPlayer(currentTeam);
            GameObject character = PrefabManager.GetInstance().GetPrefab(player.GetInGameData().GetCharName());
            character.GetComponent<PlayerCharacterLogicScript>().Init(player);
            character.GetComponent<SkillActivator>().bindedActions = player.gameObject.GetComponent<ListOfControllerActions>();
            playerCharacters.Add(character);
        }
    }
    
    void Start()
    {
        StartBattle();
    }

    //Called Once when a Player Starts
    public void StartBattle()
    {
        SetPlayerSpawnPoints();
        ResetTimer();
        if(preBattleText.gameObject.activeSelf)
            preBattleText.PlayAnim(currentRound);
    }


    public GameObject GetPlayerCharacter(int id)
    {
        if (id >= 0 && id < noOfPlayers)
        {
            return playerCharacters[id];
        }
        Debug.Log("Id must be between 0 and " + playerCharacters.Count + " You inputted : " + id);
        return null;
    }

    public List<GameObject> GetPlayers()
    {
        return playerCharacters;
    }


    void PauseGame(int playerID)
    {
        gamePaused = true;

        Time.timeScale = 0.0f;
        pauseDisplay.GetComponent<ToggleActiveScript>().ToggleActive();
        pauseDisplay.GetComponentInChildren<PauseMenuScript>().Pause(playerID);
        SetPlayerControls(false);
    }

    public void UnPauseGame()
    {
        gamePaused = false;

        Time.timeScale = 1.0f;
        pauseDisplay.GetComponent<ToggleActiveScript>().ToggleActive();
        SetPlayerControls(true);
    }


    public void ResetMatch()
    {
        switch (GameManager.Instance.GetGameMode()) 
        {
            case GAME_MODES.PRACTICE:
            case GAME_MODES.VS_AI:
                {
                    ResetPlayerCharacters();
                    SetPlayerSpawnPoints();
                    ResetTimer();
                }
                break;
            case GAME_MODES.LOCAL_PVP:
                {
                    ++currentRound;
                    ResetPlayerCharacters();
                    SetPlayerSpawnPoints();
                    ResetTimer();
                    preBattleText.PlayAnim(currentRound);
                }
                break;
        }

    }

    public void ResetEntireSet()
    {
        switch (GameManager.Instance.GetGameMode())
        {
            case GAME_MODES.PRACTICE:
            case GAME_MODES.VS_AI:
                {
                    GameManager.Instance.StartNewMatch();
                }
                break;
            case GAME_MODES.LOCAL_PVP:
                {
                    preBattleText.ResetAnim();
                    GameManager.Instance.StartNewMatch();
                }
                break;
        }
    }

    public void Rematch()
    {
        ResetEntireSet();
        SetPlayerControls(true);
        endDisplay.SetActive(false);
        gameEnd = false;
        currentRound = 1;
        SoundSystem.Instance.ChangeClip(AUDIO_TYPE.BACKGROUND_MUSIC,AudioClipManager.GetInstance().GetAudioClip(currentMap.GetMapName()),true);
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
        timeOut = false;
        timePaused = false;
        curBattleTimer = maxBattleTimer;
    }


    public void Update()
    {
        if (!gameEnd && !gamePaused)
        {
            if(GameManager.Instance.GetGameMode() != GAME_MODES.PRACTICE)
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
            { 
                gameWon = true;
                timeOut = true;
            }
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
            if (GameManager.Instance.GetPlayer(i).controller != null && GameManager.Instance.GetPlayer(i).controller.getButtonAction(ACTIONS.START))
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
        switch (GameManager.Instance.GetGameMode())
        {
            case GAME_MODES.PRACTICE:
            case GAME_MODES.VS_AI:
                ResetMatch();
                break;
            case GAME_MODES.LOCAL_PVP:
                {
                    PlayerCharacterLogicScript player1 = playerCharacters[0].GetComponent<PlayerCharacterLogicScript>();
                    PlayerCharacterLogicScript player2 = playerCharacters[1].GetComponent<PlayerCharacterLogicScript>();

                    if (timeOut && player1.GetHealthPercentage() == player2.GetHealthPercentage()
                        || !timeOut && player1.IsDead() && player2.IsDead())
                    {
                        draw = true;
                    }
                    else if (timeOut && player1.GetHealthPercentage() > player2.GetHealthPercentage()
                        || !timeOut && player2.IsDead())
                    {
                        draw = false;
                        winnerID = (int)player1.GetPlayerID();
                        loserID = (int)player2.GetPlayerID();
                    }
                    else
                    {
                        draw = false;
                        winnerID = (int)player2.GetPlayerID();
                        loserID = (int)player1.GetPlayerID();
                    }

                    if (draw)
                    {
                        GameManager.Instance.GetPlayer(player1.GetPlayerID()).GetInGameData().WinMatch();
                        GameManager.Instance.GetPlayer(player2.GetPlayerID()).GetInGameData().WinMatch();
                        victoryInterface[0].WinMatch();
                        victoryInterface[1].WinMatch();
                    }
                    else
                    {
                        GameManager.Instance.GetPlayer(winnerID).GetInGameData().WinMatch();
                        loserController = GameManager.Instance.GetPlayer(loserID).GetComponent<ListOfControllerActions>();
                        victoryInterface[(int)GameManager.Instance.GetPlayer(winnerID).GetInGameData().GetTeam()].WinMatch();
                    }

                    //Determine if Set Won By Anyone
                    bool displayEndScreen = false;
                    for (int i = 0; i < noOfPlayers; ++i)
                    {
                        PlayerData player = GameManager.Instance.GetPlayer(i);
                        if (player.GetInGameData().GetSetWon())
                        {
                            SetPlayerSpawnPoints();
                            DisplayWinResult();
                            displayEndScreen = true;
                            break;
                        }
                    }

                    if (!displayEndScreen)
                    {
                        ResetMatch();
                    }
                }
                break;
        }
    }

    void DisplayWinResult()
    {
        preBattleText.FinishAnim();
        SetPlayerControls(false);

        foreach (VictoryDisplayScript victoryUI in victoryInterface)
            victoryUI.ResetVictories();
        
        gameEnd = true;
        ResetTimer();
        PauseTimer();
        endDisplay.SetActive(true);
        endDisplay.GetComponentInChildren<EndDisplayScript>().Reset(loserController, draw);
    }

    void SetPlayerControls(bool controllActive)
    {
        foreach (GameObject player in playerCharacters)
        {
            PlayerCharacterLogicScript playerCharacter = player.GetComponent<PlayerCharacterLogicScript>();
            if (controllActive)
            {
                player.GetComponent<PlayerCharacterLogicScript>().StartUpdate(); // Start Updating Players again
                player.GetComponent<PlayerCharacterLogicScript>().EnableAllAttacks();
            }
            else
            {
                playerCharacter.StopUpdate();
                playerCharacter.DisableAndResetAllAttacks();
            }
        }
    }
}
