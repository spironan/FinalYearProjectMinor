using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

//Overall Manager for the scene
public class CharacterSelectScript : MonoBehaviour 
{
    public int row, col;
    public GameObject framePrefab;
    List<GameObject> charSlots = new List<GameObject>();

    //Player Prolly Has An Image that i can use instead of a gameobject
    public GameObject p1Frame, p2Frame;
    
	void Start () 
    {
        RectTransform parent = gameObject.GetComponent<RectTransform>();
        GridLayoutGroup grid = gameObject.GetComponent<GridLayoutGroup>();
        grid.cellSize = new Vector2(parent.rect.width / col, parent.rect.height / row);


        CharacterSlot tempSlot;
        //Create The Prefabs and add it to a list
        for (int i = 0; i < (int)CHARACTERS.MAX_CHARACTER; ++i)
        {
            GameObject slot = Instantiate(framePrefab);
            Debug.Log(slot.transform.position);
            slot.transform.SetParent(gameObject.transform, false);
            Debug.Log(slot.transform.position);
            slot.transform.localScale = new Vector3(1, 1, 1);
            tempSlot = slot.GetComponent<CharacterSlot>();
            //tempSlot.SetImage(CharacterManager.GetInstance().GetCharacterByID(i).GetIcon());
            Debug.Log(slot.transform.position);
            charSlots.Add(slot);
        }
        //Allocate CharSlots Up Down Left Right for navigation
        if ((int)CHARACTERS.MAX_CHARACTER > 1)
        {
            int maxIndex = (int)CHARACTERS.MAX_CHARACTER;
            int spawnIndex = 0;
            for (int i = 0; i < maxIndex; ++i)
            {
                //Debug.Log("Max Index : " + i);
                tempSlot = charSlots[i].GetComponent<CharacterSlot>();

                int left = i - 1;
                int right = i + 1;

                if (left < 0)
                    left = maxIndex - 1;
                else if (right > maxIndex - 1)
                    right = 0;

                //Debug.Log("i  : " + i + " Left : " + left + " Right : " + right);
                tempSlot.left = charSlots[left];
                tempSlot.right = charSlots[right];

                //Debug.Log("maxIndex : " + maxIndex + " col : " + col);
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
            Debug.Log("Gridcellsize X: " + grid.cellSize.x +
                "Spawn Index : " + spawnIndex);
            p1Frame.transform.localPosition = p2Frame.transform.localPosition = new Vector3(spawnIndex * grid.cellSize.x , spawnIndex * grid.cellSize.y, 0);
        }
        
	}

    public void Update()
    {
        for (int i = 0; i < (int)CHARACTERS.MAX_CHARACTER; ++i)
        {
            //Debug.Log(gameObject.transform.GetChild(i).transform.position);
        }
    }
    public void LockInCharacter()
    {   
    }
    
    public bool CheckBothPicked()
    {
        return false;
    }

}
