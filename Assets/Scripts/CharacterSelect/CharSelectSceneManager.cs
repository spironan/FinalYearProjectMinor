using UnityEngine;
using System.Collections;

public class CharSelectSceneManager : MonoBehaviour
{
    enum SELECTIONPHASE
    {
        PICKING,
        MAP_PICK,
        BEGIN_MATCH,
        END_SELECTIONSTAGE
    };

    SELECTIONPHASE currentPhase = SELECTIONPHASE.PICKING;

    public GameObject charSelectHolder, mapSelectHolder, mapSelectSpawner;
    CharacterSelectScript charSelectData;
    MapSelectScript mapSelectData;
	GameManager manager;
    // Use this for initialization
	void Start () 
    {
        manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        charSelectData = charSelectHolder.GetComponent<CharacterSelectScript>();
        mapSelectHolder.SetActive(false);
        manager.ChangeState(GAMESTATE.CHAR_SELECT);
	}
	
	// Update is called once per frame
	void Update () {

        switch (currentPhase)
        {
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
                    //Play Some Animation Maybe?
                    GoToNextScene();
                }
                break;
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
        manager.LoadBattleScene();
    }

}
