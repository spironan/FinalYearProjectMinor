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

    //Frames Of The Different Players
    public GameObject[] frameObj = new GameObject[(int)TEAM.MAX_TEAM];
    //Current Team Of the Player used to assign player team
    TEAM playerTeam = TEAM.RED_TEAM;

    //public GameObject charSelectHolder, mapSelectHolder, mapSelectSpawner;
    GameObject mapSelectHolder;
    CharacterSelectScript charSelectData;
    MapSelectScript mapSelectData;
	GameManager gameManager;

    // Use this for initialization
	void Awake () 
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        gameManager.ChangeState(GAMESTATE.CHAR_SELECT);
        charSelectData = GameObject.FindGameObjectWithTag("CharacterSelect").GetComponent<CharacterSelectScript>();
        mapSelectData = GameObject.FindGameObjectWithTag("MapSpawnArea").GetComponent<MapSelectScript>();
        mapSelectHolder = GameObject.FindGameObjectWithTag("MapHolder");
        mapSelectHolder.SetActive(false);
        //charSelectData = charSelectHolder.GetComponent<CharacterSelectScript>();
        //mapSelectHolder.SetActive(false);
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
        for (int i = 0; i < gameManager.GetPlayerSize(); ++i)
        {
            PlayerData player = gameManager.GetPlayer(i);
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
            //mapSelectData = mapSelectSpawner.GetComponent<MapSelectScript>();
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
