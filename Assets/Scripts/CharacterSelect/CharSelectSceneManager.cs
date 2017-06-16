using UnityEngine;
using System.Collections;

public class CharSelectSceneManager : MonoBehaviour
{
    enum SELECTIONPHASE
    {
        PLAYER_ASSIGN,
        PICKING,
        MAP_PICK,
        BEGIN_MATCH,
        END_SELECTIONSTAGE
    };
    SELECTIONPHASE currentPhase = SELECTIONPHASE.PLAYER_ASSIGN;

    public GameObject charSelectHolder, mapSelectHolder, mapSelectSpawner;
    CharacterSelectScript charSelectData;
    MapSelectScript mapSelectData;
	GameManager manager;

    //Frames Of The Different Players
    public GameObject[] frameObj = new GameObject[(int)TEAM.MAX_TEAM];
    //Current Team Of the Player
    TEAM playerTeam = TEAM.RED_TEAM;

    // Use this for initialization
	void Start () 
    {
        manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        manager.ChangeState(GAMESTATE.CHAR_SELECT);
        charSelectData = charSelectHolder.GetComponent<CharacterSelectScript>();
        mapSelectHolder.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () 
    {
        switch (currentPhase)
        {
            case SELECTIONPHASE.PLAYER_ASSIGN:
                {
                    UpdatePlayerAssign();
                }
                break;
            case SELECTIONPHASE.PICKING:
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
        bool gotoCharSelect = true;
        for (int i = 0; i < manager.GetPlayerSize(); ++i)
        {
            PlayerData player = manager.GetPlayer(i);
            if (!player.IsAssigned())
            {
                gotoCharSelect = false;
                if(player.controller.getButtonAction(ACTIONS.START))
                {
                    player.Assign();
                    player.GetInGameData().SetTeam(playerTeam);
                    player.selectframe = frameObj[(int)playerTeam];
                    charSelectData.CreatePlayerFrame((int)player.GetPlayerID());
                    playerTeam++;
                    Debug.Log("Assigned Player ID +" + player.GetPlayerID() + " To Team : " + playerTeam);
                }
            }
        }

        if (gotoCharSelect)
        {
            currentPhase = SELECTIONPHASE.PICKING;
        }

    }

    void CharacterPickingPhase()
    {
        ///Check for controls and navigation of picture here
        if (charSelectData.FinishedPicking())
        {
            //Turn On Map Straight away
            mapSelectHolder.SetActive(true);
            mapSelectData = mapSelectSpawner.GetComponent<MapSelectScript>();
            currentPhase = SELECTIONPHASE.MAP_PICK;
        }
    }

    void MapPickingPhase()
    {
        ///Check for relevant controls and navigation of picture here
        if (mapSelectData.CheckMapPicked())
            currentPhase = SELECTIONPHASE.BEGIN_MATCH;
        else if (mapSelectData.CancelMapSelect())
        {
            mapSelectData.SetCancel(false);
            mapSelectHolder.SetActive(false);
            charSelectData.UnFinish();
            currentPhase = SELECTIONPHASE.PICKING;
        }
    }

    void GoToNextScene()
    {
        //Play Some Animation Maybe?
        LoadingScreenManager.LoadScene("BattleScene");
    }

}
