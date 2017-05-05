using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

//Overall Manager for the scene
public class CharacterSelectScript : MonoBehaviour 
{
    enum SELECTIONPHASE
    {
        PICKING,
        MAP_PICK,
        BEGIN_MATCH,
        END_SELECTIONSTAGE
    };

    SELECTIONPHASE currentPhase = SELECTIONPHASE.PICKING;
    public int row, col;
    public GameObject framePrefab;
    List<GameObject> charSlots = new List<GameObject>();

    List<GameObject> maps = new List<GameObject>();
    public GameObject MapHolder;
    //Player Prolly Has An Image that i can use instead of a gameobject
    public GameObject p1Frame, p2Frame;

	// Use this for initialization
	void Start () 
    {
        //Turn Off Map Straight away
        MapHolder.SetActive(false);
        RectTransform parent = gameObject.GetComponent<RectTransform>();
        GridLayoutGroup grid = gameObject.GetComponent<GridLayoutGroup>();
        grid.cellSize = new Vector2(parent.rect.width / col, parent.rect.height / row);
        
        CharacterSlot tempSlot;
        //Create The Prefabs and add it to a list
        for (int i = 0; i < (int)CHARACTERS.MAX_CHARACTER; ++i)
        {
            GameObject slot = Instantiate(framePrefab);
            charSlots.Add(slot);
            slot.transform.parent = gameObject.transform;
            slot.transform.localScale = new Vector3(1, 1, 1);
            tempSlot = slot.GetComponent<CharacterSlot>();
            //tempSlot.SetImage(CharacterManager.GetInstance().GetCharacterByID(i).GetIcon());
            Debug.Log("Created Prefabs");
        }
        //Allocate CharSlots Up Down Left Right for navigation
        if ((int)CHARACTERS.MAX_CHARACTER > 1)
        {
            int maxIndex = (int)CHARACTERS.MAX_CHARACTER;
            int spawnIndex = 0;
            for (int i = 0; i < maxIndex; ++i)
            {
                Debug.Log("Max Index : " + i);
                tempSlot = charSlots[i].GetComponent<CharacterSlot>();

                int left = i - 1;
                int right = i + 1;

                if (left < 0)
                    left = maxIndex - 1;
                else if (right > maxIndex - 1)
                    right = 0;

                Debug.Log("i  : " + i + " Left : " + left + " Right : " + right);
                tempSlot.left = charSlots[left];
                tempSlot.right = charSlots[right];

                Debug.Log("maxIndex : " + maxIndex + " col : " + col);
                if (maxIndex > col)
                {
                    int up = i - col;
                    int down = i + col;

                    if (up < 0)
                        up += col - 1;
                    else if (down > col - 1)
                        down -= col;

                    tempSlot.up = charSlots[up];
                    tempSlot.down = charSlots[down];
                    spawnIndex = (int)(col * 0.5f);
                }
                else
                    spawnIndex = (int)(maxIndex * 0.5f);
            }
            Debug.Log("Spawn Index is : " + spawnIndex);
            //p1Frame.transform.position = p2Frame.transform.position = charSlots[spawnIndex].transform.position;

        }
        else
        {
            Debug.Log("Can Only Choose 1 Char");
            p1Frame.transform.position = p2Frame.transform.position = charSlots[0].gameObject.transform.position;
        }
        

	}
	
	// Update is called once per frame main update loop for this scene
	void Update () 
    {
        switch(currentPhase)
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
        ///
        if (CheckBothPicked())
        {
            currentPhase = SELECTIONPHASE.MAP_PICK;
            //Turn On Map Straight away
            MapHolder.SetActive(true);
        }
    }

    void MapPickingPhase()
    {
        ///Check for relevant controls and navigation of picture here
        ///
        if (CheckBothPicked())
            currentPhase = SELECTIONPHASE.BEGIN_MATCH;
    }

    public void LockInCharacter()
    {
        
    }
    
    public void PickSelectedMap()
    {
 
    }

    bool CheckBothPicked()
    {
        return false;
    }

    bool CheckMapPicked() 
    {
        return false;
    }
}
