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


    public GameObject charSelectHolder, mapSelectHolder;
    CharacterSelectScript charSelectData;
    MapSelectScript mapSelectData;
	
    // Use this for initialization
	void Start () 
    {
        charSelectData = charSelectHolder.GetComponent<CharacterSelectScript>();
        mapSelectData = mapSelectHolder.GetComponent<MapSelectScript>();
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
                break;
        }
    }

    void CharacterPickingPhase()
    {
        ///Check for controls and navigation of picture here
        if (charSelectData.CheckBothPicked())
        {
            currentPhase = SELECTIONPHASE.MAP_PICK;
            //Turn On Map Straight away
        }
    }

    void MapPickingPhase()
    {
        ///Check for relevant controls and navigation of picture here
        if (mapSelectData.CheckMapPicked())
            currentPhase = SELECTIONPHASE.BEGIN_MATCH;
    }
}
