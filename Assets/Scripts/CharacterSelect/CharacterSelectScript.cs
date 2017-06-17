using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

//Overall Manager for the scene
public class CharacterSelectScript : MonoBehaviour
{
    //Prefab of the Slots To Spawn
    GameObject framePrefab;
    //List Of Spawned Prefabs
    List<GameObject> charSlots = new List<GameObject>();
    //List Of Player's Different Frames
    List<CharSelectLocationScript> playerFrames = new List<CharSelectLocationScript>();
    //GameManager
    GameManager gameManager;
    //Is The Character Select Finished
    bool finished = false;
    //The Max Amount of width before going upwards
    int maxWidth;
    //The Number of Slots to Create for each character
    int charCount;

	void Start () 
    {
        framePrefab = PrefabManager.GetInstance().GetPrefab("CharacterSlot");
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        charCount = CharacterManager.GetInstance().GetCharCount();
        finished = false;
        SpawnSlots();
        LinkSlots();
	}

    void SpawnSlots()
    {
        Vector2 parentSize = GetComponent<RectTransform>().rect.size;
        Vector2 prefabSize = framePrefab.GetComponent<RectTransform>().rect.size;
        int width = 0;
        int height = 0;
        maxWidth = (int)(parentSize.x / prefabSize.x);
        float startPointX = -parentSize.x / 2.0f + prefabSize.x / 2.0f;

        CharacterSlot tempSlot;
        //Create The Prefabs and add it to a list
        for (int i = 0; i < charCount; ++i)
        {
            GameObject slot = Instantiate(framePrefab);
            slot.transform.SetParent(gameObject.transform, false);
            slot.transform.localScale = new Vector3(1, 1, 1);
            slot.transform.localPosition = new Vector3(startPointX + (width * prefabSize.x), height * -prefabSize.y, 0);
            tempSlot = slot.GetComponent<CharacterSlot>();

            CharacterBase charData = CharacterManager.GetInstance().GetCharacterByIndex(i);
            tempSlot.SetCharName(charData.GetName());
            tempSlot.SetImageSprite(charData.GetCharArt());
            charSlots.Add(slot);

            width++;
            if (width > maxWidth)
            {
                height++;
                width = 0;
            }
        }
    }

    void LinkSlots()
    {
        CharacterSlot tempSlot;
        int spawnIndex = 0;
        //Allocate CharSlots Up Down Left Right for navigation
        if (charCount > 1)
        {
            int maxIndex = charCount;
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
                }
                spawnIndex = (int)(maxIndex * 0.5f);
            }

        }
    }

    public void CreatePlayerFrame(int playerID)
    {
        GameObject frame = Instantiate(gameManager.GetPlayer(playerID).GetComponent<PlayerData>().selectframe);
        if (frame != null)
        {
            frame.transform.SetParent(gameObject.transform, false);
            frame.transform.localScale = new Vector3(1, 1, 1);
            frame.GetComponent<CharSelectLocationScript>().AssignCharSlot(charSlots[0]);
            playerFrames.Add(frame.GetComponent<CharSelectLocationScript>());
        }
        else
            Debug.Log("Unable to Create Player Frame for :" + playerID + "as it is null");
    }

    public void Update()
    {
        if(!finished)
            NavigateSelect();
    }

    void NavigateSelect()
    {
        for (int i = 0; i < playerFrames.Count; ++i)
        {
            PlayerData player = gameManager.GetPlayer(i);
            if (!player.IsAssigned())
                continue;
            ListOfControllerActions playerController = player.controller;
            int team = (int)player.GetInGameData().GetTeam();
            //Move Left Right
            if (playerController.getAxisActionBoolDown(ACTIONS.MOVE_LEFT))
            {
                playerFrames[team].MoveLeft();
            }
            else if (playerController.getAxisActionBoolDown(ACTIONS.MOVE_RIGHT)) 
            {
                playerFrames[team].MoveRight();
            }

            // Move Up Down
            if (playerController.getAxisActionBoolDown(ACTIONS.MOVE_UP))
            {
                playerFrames[team].MoveUp();
            }
            else if (playerController.getAxisActionBoolDown(ACTIONS.MOVE_DOWN))
            {
                playerFrames[team].MoveDown();
            }

            //Player Picks Character
            if (playerController.getButtonAction(ACTIONS.PICK_CHARACTER))
            {
                LockInCharacter(player.GetPlayerID(), playerFrames[team].GetCharName());
            }
            //Player Unpick Character
            else if (playerController.getButtonAction(ACTIONS.UNPICK_CHARACTER))
            {
                DeselectCharacter(player.GetPlayerID());
            }
        }
    }

    public void LockInCharacter(PLAYER player, string charaName)
    {
        //SetCharacter
        //gameManager.GetPlayer(player).GetInGameData().SetChar(charaName);
        //TESTCODE
        gameManager.GetPlayer(player).GetInGameData().SetCharName(charaName);

        gameManager.GetPlayer(player).PickChar();
        playerFrames[(int)player].LockIn();

        if (CheckBothPicked())
        {
            finished = true;
        }
    }

    public void DeselectCharacter(PLAYER player)
    {
        //DeselectCharacter        
        gameManager.GetPlayer(player).UnPickChar();
        playerFrames[(int)player].UnLock();
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

    public bool FinishedPicking() { return finished; }
    public void UnFinish() { finished = false; }

    public string GetCurrChara(TEAM playerteam)
    {
        if ((int)playerteam >= playerFrames.Count)
        {
            Debug.Log("Havent Created Frame Yet For Team : ,Returning Null. You entered a teamNumber :" + playerteam + " Current number of frames created : " + playerFrames.Count);
            return null;
        }
        if (playerteam >= TEAM.MAX_TEAM)
        {
            Debug.Log("You fucked up, you entered a teamNumber too big :" + (int)playerteam + " Max team number is : " + (int)TEAM.MAX_TEAM);
            return null;
        }

        return playerFrames[(int)playerteam].GetCharName().ToString().ToUpper();
    }
    public Sprite GetCharaArt(TEAM playerteam)
    {
        if (playerteam >= TEAM.MAX_TEAM)
        {
            Debug.Log("You fucked up, you entered a teamNumber too big :" + (int)playerteam + " Max team number is : " + (int)TEAM.MAX_TEAM);
            return null;
        }

        Debug.Log("Player Char Name is : " + playerFrames[(int)playerteam].GetCharName());
        return CharacterManager.GetInstance().GetCharacterByName(playerFrames[(int)playerteam].GetCharName()).GetCharArt();
    }

}
