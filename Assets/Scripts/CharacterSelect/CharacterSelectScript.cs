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
    //Back To Main Menu Obj
    GameObject backToMainObj;
    //The Max Amount of width before going upwards
    int maxWidth;
    //The Number of Slots to Create for each character
    int charCount;
    //Is The Character Select Finished
    bool finished = false;
    //To Determine Whether or not To Take In Input Of Players
    bool updateNavigation = true;

	void Start () 
    {
        framePrefab = PrefabManager.GetInstance().GetPrefab("CharacterSlot");
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        charCount = CharacterManager.GetInstance().GetCharCount();
        backToMainObj = GameObject.FindGameObjectWithTag("BackToMain");
        backToMainObj.GetComponent<ToggleActiveScript>().ToggleActive();
        finished = false;
        updateNavigation = true;
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
        PlayerData player = gameManager.GetPlayer(playerID).GetComponent<PlayerData>();
        GameObject frame = Instantiate(player.selectframe);
        if (frame != null)
        {
            frame.transform.SetParent(gameObject.transform, false);
            frame.transform.localScale = new Vector3(1, 1, 1);
            CharSelectLocationScript framescript = frame.GetComponent<CharSelectLocationScript>();
            if (player.GetInGameData().GetCharName() != "")
                framescript.AssignCharSlot(charSlots[CharacterManager.GetInstance().GetCharacterIndex(player.GetInGameData().GetCharName())]);
            else
                framescript.AssignCharSlot(charSlots[0]);
            playerFrames.Add(framescript);
        }
        else
            Debug.Log("Unable to Create Player Frame for :" + playerID + "as it is null");
    }

    public void Update()
    {
        if(!finished)
            NavigateSelect();

        if (CheckBothPicked())
            finished = true;
    }

    void NavigateSelect()
    {
        if (updateNavigation)
        {
            for (int i = 0; i < gameManager.GetPlayerSize(); ++i)
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
                    LockInCharacter(player.GetInGameData().GetTeam(), playerFrames[team].GetCharName());
                }
                //Player Unpick Character
                else if (playerController.getButtonAction(ACTIONS.UNPICK_CHARACTER))
                {
                    if (player.GetPickStatus())
                        DeselectCharacter(player.GetInGameData().GetTeam());
                    else
                        ActivateExitConfirmation(playerController);
                }
            }
        }
        else if(!backToMainObj.activeSelf)
        {
            updateNavigation = true;
        }
    }


    void ActivateExitConfirmation(ListOfControllerActions controller)
    {
        backToMainObj.GetComponent<ToggleActiveScript>().ToggleActive();
        backToMainObj.GetComponent<BackToMainScript>().SetControllerToReadFrom(controller);
        backToMainObj.GetComponent<BackToMainScript>().Reset();
        updateNavigation = false;
    }

    public void LockInCharacter(TEAM playerTeam, string charaName)
    {
        //SetCharacter
        //gameManager.GetPlayer(player).GetInGameData().SetChar(charaName);
        //TESTCODE
        gameManager.GetPlayer(playerTeam).GetInGameData().SetCharName(charaName);
        gameManager.GetPlayer(playerTeam).PickChar();
        playerFrames[(int)playerTeam].LockIn();
    }

    public void DeselectCharacter(TEAM playerTeam)
    {
        //DeselectCharacter        
        gameManager.GetPlayer(playerTeam).UnPickChar();
        playerFrames[(int)playerTeam].UnLock();
    }

    public void LockAllFrames()
    {
        for (int i = 0; i < playerFrames.Count; ++i)
        {
            playerFrames[i].LockIn();
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
