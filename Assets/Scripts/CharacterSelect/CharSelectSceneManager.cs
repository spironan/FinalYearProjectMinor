using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharSelectSceneManager : MonoBehaviour
{
    enum SELECTIONPHASE
    {
        PLAYER_ASSIGN,
        CHARACTER_PICKING,
        MAP_PICK,
        BEGIN_MATCH,
        END_SELECTIONSTAGE
    };
    SELECTIONPHASE currentPhase = SELECTIONPHASE.PLAYER_ASSIGN;
    SELECTIONPHASE previousPhase;

    //Frames Of The Different Players
    public GameObject[] frameObj = new GameObject[(int)TEAM.MAX_TEAM];
    //Current Team Of the Player used to assign player team
    TEAM playerTeam = TEAM.RED_TEAM;
    //All the Different Canvases(SideSelect,CharSelect,MapSelect)
    List<CanvasGroup> canvasGroups = new List<CanvasGroup>();
    //public GameObject charSelectHolder, mapSelectHolder, mapSelectSpawner;
    SideSelectScript sideSelect;
    CharacterSelectScript charSelectData;
    MapSelectScript mapSelectData;
    bool autoCreateSlots = true;
    
	void Awake () 
    {
        GameManager.Instance.ChangeState(GAMESTATE.CHAR_SELECT);
        
        //Find and add all canvas groups to the list
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Canvas"))
            canvasGroups.Add(go.GetComponent<CanvasGroup>());
        
        sideSelect = GameObject.FindWithTag("SideSelect").GetComponent<SideSelectScript>();
        charSelectData = GameObject.FindGameObjectWithTag("CharacterSelect").GetComponent<CharacterSelectScript>();
        mapSelectData = GameObject.FindGameObjectWithTag("MapSpawnArea").GetComponentInChildren<MapSelectScript>();
        autoCreateSlots = true;
        SetActiveCanvas();
	}

    void SetInteractableCanvas(string canvasName)
    {
        foreach (CanvasGroup canvasgroup in canvasGroups)
            if (canvasgroup.gameObject.name == canvasName)
            {
                canvasgroup.gameObject.SetActive(true);
                canvasgroup.interactable = true;
                canvasgroup.alpha = 1.0f;
            }
            else
            {
                canvasgroup.interactable = false;
                canvasgroup.alpha = 0.0f;
                canvasgroup.gameObject.SetActive(false);
            }
    }

    void ChangeSelectionPhase(SELECTIONPHASE newPhase)
    {
        if (currentPhase != newPhase)
        {
            previousPhase = currentPhase;
            currentPhase = newPhase;
            SetActiveCanvas();
        }
    }

    void SetActiveCanvas()
    {
        switch (currentPhase)
        {
            case SELECTIONPHASE.PLAYER_ASSIGN:
                {
                    switch (GameManager.Instance.GetGameMode())
                    {
                        case GAME_MODES.PRACTICE:
                            {
                                SetInteractableCanvas("SideSelectCanvas");
                                GameManager.Instance.RestoreDefaults();
                            }
                            break;
                        case GAME_MODES.LOCAL_PVP:
                            SetInteractableCanvas("CharacterSelectCanvas");
                            break;
                    }
                }
                break;
            case SELECTIONPHASE.CHARACTER_PICKING:
                {
                    switch (GameManager.Instance.GetGameMode())
                    {
                        case GAME_MODES.PRACTICE:
                        case GAME_MODES.LOCAL_PVP:
                                SetInteractableCanvas("CharacterSelectCanvas");
                            break;
                    }
                }
                break;
            case SELECTIONPHASE.MAP_PICK:
                {
                    SetInteractableCanvas("MapSelectCanvas");
                }
                break;
            case SELECTIONPHASE.BEGIN_MATCH:
                {
                }
                break;
        }
    }

	void Update () 
    {
        switch (currentPhase)
        {
            case SELECTIONPHASE.PLAYER_ASSIGN:
                {
                    UpdatePlayerAssign();
                }
                break;
            case SELECTIONPHASE.CHARACTER_PICKING:
                {
                    CharacterPickingPhase();
                }
                break;
            case SELECTIONPHASE.MAP_PICK:
                {
                    MapPickingPhase();
                }
                break;
            case SELECTIONPHASE.BEGIN_MATCH:
                {
                    GoToNextScene();
                }
                break;
        }

    }

    void UpdatePlayerAssign()
    {
        switch (GameManager.Instance.GetGameMode())
        {
            case GAME_MODES.PRACTICE:
                {
                    if (sideSelect.SelectedTeam())
                    {
                        ChangeSelectionPhase(SELECTIONPHASE.CHARACTER_PICKING);
                        charSelectData.CreatePlayerFrame(PLAYER.PLAYER_ONE);
                        //charSelectData.CreatePlayerFrame(PLAYER.PLAYER_TWO);
                    }
                }
                break;

            case GAME_MODES.LOCAL_PVP:
                {
                    bool gotoCharSelect = true;
                    for (int i = 0; i < GameManager.Instance.GetPlayerSize(); ++i)
                    {
                        PlayerData player = GameManager.Instance.GetPlayer(i);
                        if (!player.IsAssigned())
                        {
                            gotoCharSelect = false;
                            if (player.controller.getButtonAction(ACTIONS.START))
                            {
                                autoCreateSlots = false;
                                player.Assign();
                                player.GetInGameData().SetTeam(playerTeam);
                                player.selectframe = frameObj[(int)playerTeam];
                                charSelectData.CreatePlayerFrame(player.GetPlayerID());
                                Debug.Log("Assigned Player ID: " + player.GetPlayerID() + " To Team : " + playerTeam);
                                playerTeam++;
                            }
                        }
                    }

                    if (gotoCharSelect)
                    {
                        if (autoCreateSlots)
                        {
                            for (int i = 0; i < GameManager.Instance.GetPlayerSize(); ++i)
                            {
                                charSelectData.CreatePlayerFrame(GameManager.Instance.GetPlayer(i).GetPlayerID());
                            }
                        }
                        //currentPhase = SELECTIONPHASE.CHARACTER_PICKING;
                        ChangeSelectionPhase(SELECTIONPHASE.CHARACTER_PICKING);
                    }
                }
                break;
        }
    }

    void CharacterPickingPhase()
    {
        //Check for controls and navigation of picture here
        if (charSelectData.FinishedPicking())
        {
            //Lock In All frames
            charSelectData.LockAllFrames();
            //Change Scene to Map Select
            ChangeSelectionPhase(SELECTIONPHASE.MAP_PICK);
            //currentPhase = SELECTIONPHASE.MAP_PICK;
        }
        else if (charSelectData.BackToSideSelect())
        {
            charSelectData.Reset();
            ChangeSelectionPhase(SELECTIONPHASE.PLAYER_ASSIGN);
            sideSelect.Reset();
        }
    }

    void MapPickingPhase()
    {
        //Check for relevant controls and navigation of picture here
        if (mapSelectData.CheckMapPicked())
        {
            //currentPhase = SELECTIONPHASE.BEGIN_MATCH;
            ChangeSelectionPhase(SELECTIONPHASE.BEGIN_MATCH);
        }
        else if (mapSelectData.CancelMapSelect())
        {
            ChangeSelectionPhase(SELECTIONPHASE.CHARACTER_PICKING);
            charSelectData.UnFinish(mapSelectData.GetCancelledTeam());
            mapSelectData.SetCancel(false);
        }
    }

    void GoToNextScene()
    {
        //Play Some Animation Maybe?
        LoadingScreenManager.LoadScene("BattleScene");
    }
    
}
