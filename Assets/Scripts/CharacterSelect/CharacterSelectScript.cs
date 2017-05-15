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
    List<GameObject> playerFrames;
    //p1Frame, p2Frame;

    GameManager gameManager;
    bool finished = false;
    Vector2 moveBy;

	void Start () 
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        for (int i = 0; i < gameManager.GetPlayerSize(); ++i)
        {
            playerFrames.Add(gameManager.GetPlayer(i).GetComponent<PlayerData>().selectframe);
        }
        finished = false;
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

            for (int i = 0; i < playerFrames.Count; ++i)
            {
                GameObject frame = Instantiate(playerFrames[i]);
                frame.transform.SetParent(gameObject.transform, false);
                frame.transform.localScale = new Vector3(1, 1, 1);
                frame.transform.localPosition = charSlots[spawnIndex].transform.localPosition;
                moveBy = new Vector2(frame.GetComponent<Rect>().width, frame.GetComponent<Rect>().height);
            }

            ////Init Player 1 
            //GameObject p1 = Instantiate(p1Frame);
            //p1.transform.SetParent(gameObject.transform, false);
            //p1.transform.localScale = new Vector3(1, 1, 1);
            ////InitPlayer 2 
            //GameObject p2 = Instantiate(p2Frame);
            //p2.transform.SetParent(gameObject.transform, false);
            //p2.transform.localScale = new Vector3(1, 1, 1);
            //p1Frame.transform.localPosition = p2Frame.transform.localPosition = charSlots[spawnIndex].transform.localPosition;
            
        }
        
	}

    public void Update()
    {
        NavigateSelect();
    }

    void NavigateSelect()
    {
        for (int i = 0; i < gameManager.GetPlayerSize(); ++i)
        {
            PlayerData player = gameManager.GetPlayer(i);
            //Move Left Right
            if (player.IsJoyDown(JOYSTICK_AXIS_INPUT.DPAD_LEFT)
                || player.IsJoyDown(JOYSTICK_AXIS_INPUT.L3_LEFT)
                )
            {

            }
            else if (player.IsJoyDown(JOYSTICK_AXIS_INPUT.DPAD_RIGHT)
                || player.IsJoyDown(JOYSTICK_AXIS_INPUT.L3_RIGHT)) 
            {

            }

            // Move Up Down
            if (player.IsJoyDown(JOYSTICK_AXIS_INPUT.DPAD_UP)
                || player.IsJoyDown(JOYSTICK_AXIS_INPUT.L3_UP))
            {

            }
            else if (player.IsJoyDown(JOYSTICK_AXIS_INPUT.DPAD_DOWN)
                || player.IsJoyDown(JOYSTICK_AXIS_INPUT.L3_DOWN))
            {
            }
            
            if(player.IsKeyDown(BUTTON_INPUT.A))
            {
                LockInCharacter(gameManager.GetPlayer(i),);
            }
        }
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
        for (int i = 0; i < gameManager.GetPlayerSize(); ++i)
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
