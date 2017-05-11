using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

//Overall Manager for the scene
public class CharacterSelectScript : MonoBehaviour 
{
    public GameObject framePrefab;
    List<GameObject> charSlots = new List<GameObject>();

    //Player Prolly Has An Image that i can use instead of a gameobject
    public GameObject p1Frame, p2Frame;

    GameManager gameManager;
    bool finished = false;

	void Start () 
    {
        finished = false;
        //gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        Vector2 parentSize = GetComponent<RectTransform>().rect.size;
        Vector2 prefabSize = framePrefab.GetComponent<RectTransform>().rect.size;

        int width = 0;
        int height = 0;
        int maxWidth = (int)(parentSize.x / prefabSize.x);
        float startPointX = -parentSize.x / 2.0f + prefabSize.x/2.0f;
        CharacterSlot tempSlot;

        //Create The Prefabs and add it to a list
        for (int i = 0; i < (int)CHARACTERS.MAX_CHARACTER; ++i)
        {
            GameObject slot = Instantiate(framePrefab);
            slot.transform.SetParent(gameObject.transform, false);
            slot.transform.localScale = new Vector3(1, 1, 1);
            slot.transform.localPosition = new Vector3(startPointX + (width * prefabSize.x), height * -prefabSize.y, 0);
            tempSlot = slot.GetComponent<CharacterSlot>();
            //tempSlot.SetImage(CharacterManager.GetInstance().GetCharacterByID(i).GetIcon());
            Debug.Log(slot.transform.position);
            charSlots.Add(slot);
            width++;
            if (width > maxWidth)
            {
                height++;
                width = 0;
            }
        }
        //Allocate CharSlots Up Down Left Right for navigation
        if ((int)CHARACTERS.MAX_CHARACTER > 1)
        {
            int maxIndex = (int)CHARACTERS.MAX_CHARACTER;
            int spawnIndex = 0;// = new Vector2(0, 0);
            for (int i = 0; i < maxIndex; ++i)
            {
                tempSlot = charSlots[i].GetComponent<CharacterSlot>();

                int left = i - 1;
                int right = i + 1;

                if (left < 0)
                    left = maxIndex - 1;
                else if (right > maxIndex - 1)
                    right = 0;

                tempSlot.left = charSlots[left];
                tempSlot.right = charSlots[right];

                if (maxIndex > maxWidth)
                {
                    int up = i - maxWidth;
                    int down = i + maxWidth;

                    if (up < 0)
                        up += maxWidth - 1;
                    else if (down > maxWidth - 1)
                        down -= maxWidth;

                    tempSlot.up = charSlots[up];
                    tempSlot.down = charSlots[down];
                    spawnIndex = (int)(maxWidth * 0.5f);
                }
                spawnIndex = (int)(maxIndex * 0.5f);
            }


            //Init Player 1 
            GameObject p1 = Instantiate(p1Frame);
            p1.transform.SetParent(gameObject.transform, false);
            p1.transform.localScale = new Vector3(1, 1, 1);
            
            //InitPlayer 2 
            GameObject p2 = Instantiate(p2Frame);
            p2.transform.SetParent(gameObject.transform, false);
            p2.transform.localScale = new Vector3(1, 1, 1);

            p1Frame.transform.localPosition = p2Frame.transform.localPosition = charSlots[spawnIndex].transform.localPosition;// new Vector3(startPointX + (spawnIndex.x * prefabSize.x), spawnIndex.y * -prefabSize.y, 0);
            
        }
        
	}

    public void Update()
    {
    }

    void NavigateSelect()
    {
        //Move Left Right
        //if()
        //{
        //}
        //else if () 
        //{
        //}
        //// Move Up Down
        //if()
        //{
        //}
        //else if ()
        //{
        //}
    }

    public void LockInCharacter(PLAYER player,CHARACTERS charaName)
    {
        //SetCharacter
        //if (!gameManager.GetPlayer(player).GetPickStatus())//If player haven't picked a character
        //{
            gameManager.GetPlayer(player).GetInGameData().SetChar(charaName);
            gameManager.GetPlayer(player).PickChar();
        //}
        //else if (gameManager.GetPlayer(player).GetInGameData().GetChar() == CharacterManager.GetInstance().GetCharacterByName(charaName))//If player press on the same Character
        //{
        //    gameManager.GetPlayer(player).UnPickChar();
        //}
        //else
        //{
        //}

        if (CheckBothPicked())
        {
            finished = true;
        }
    }
    
    bool CheckBothPicked()
    {
        for(int i = 0; i < (int)PLAYER.MAX_PLAYERS; ++i)
        {
            if (gameManager.GetPlayer(i).GetPickStatus() == false)
                return false;
        }
        return true;
    }

    public bool FinishedPicking()
    {
        return finished;
    }






}
