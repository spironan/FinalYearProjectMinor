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
    //The Max Amount of width before going upwards
    int maxWidth;
    //The Number of Slots to Create for each character
    int charCount;
    //Is The Character Select Finished
    bool finished = false;
    //To Determine whether or not to go back to SideSelect
    bool backToSideSelect = false;
    //AudioClips that the sound play
    AudioClip startSound;
    

	void Awake () 
    {
        //Audio SFX
        startSound = AudioClipManager.GetInstance().GetAudioClip("Start");
        framePrefab = PrefabManager.GetInstance().GetPrefab("CharacterSlot");
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
            tempSlot.SetImageSprite(charData.GetIcon());
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
        //int spawnIndex = 0;
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
                //spawnIndex = (int)(maxIndex * 0.5f);
            }

        }
    }

    public void CreatePlayerFrame(PLAYER playerID)
    {
        if (playerFrames.Count >= (int)PLAYER.MAX_PLAYERS)
            return;

        PlayerData player = GameManager.Instance.GetPlayer(playerID);
        GameObject frame = Instantiate(player.selectframe);
        if (frame != null)
        {
            frame.transform.SetParent(gameObject.transform, false);
            frame.transform.localScale = new Vector3(1, 1, 1);
            CharSelectLocationScript framescript = frame.GetComponent<CharSelectLocationScript>();
            framescript.SetPlayerID(playerID);
            if (player.GetInGameData().GetCharName() != "")
                framescript.AssignCharSlot(charSlots[CharacterManager.GetInstance().GetCharacterIndex(player.GetInGameData().GetCharName())]);
            else
                framescript.AssignCharSlot(charSlots[0]);
            Debug.Log("Created Frame For PlayerID :" + playerID);
            playerFrames.Add(framescript);
            SoundSystem.Instance.PlayClip(AUDIO_TYPE.SOUND_EFFECTS, startSound, false, "SFX_Player" + ((int)playerID + 1));
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
        if (!GlobalUI.Instance.GetConfirmationDisplayActive())
        {
            for (int i = 0; i < GameManager.Instance.GetPlayerSize(); ++i)
            {
                PlayerData player = GameManager.Instance.GetPlayer(i);
                Debug.Log("Player ID :" + player.GetPlayerID() + " Of Team : " + player.GetInGameData().GetTeam() + " Is Assigned : " + player.IsAssigned());
                if (!player.IsAssigned())
                    continue;

                ListOfControllerActions playerController = player.controller;
                if (player.controller == null)
                    continue;

                CharSelectLocationScript curPlayerFrame = GetPlayerFrame(player.GetPlayerID());
                if (curPlayerFrame == null)
                    continue;
                //int team = (int)player.GetInGameData().GetTeam();
                //Debug.Log("Player ID by accessing through player: " + player.GetPlayerID() + " Player ID by accessing through controller : " + player.controller.GetControllerManager().playerID);

                //Move Left Right
                if (playerController.getAxisActionBoolDown(ACTIONS.MOVE_LEFT))
                {
                    curPlayerFrame.MoveLeft();
                }
                else if (playerController.getAxisActionBoolDown(ACTIONS.MOVE_RIGHT))
                {
                    curPlayerFrame.MoveRight();
                }

                // Move Up Down
                if (playerController.getAxisActionBoolDown(ACTIONS.MOVE_UP))
                {
                    curPlayerFrame.MoveUp();
                }
                else if (playerController.getAxisActionBoolDown(ACTIONS.MOVE_DOWN))
                {
                    curPlayerFrame.MoveDown();
                }

                //Player Picks Character
                if (playerController.getButtonAction(ACTIONS.PICK_CHARACTER))
                {
                    Debug.Log("Locking In Character : " + curPlayerFrame.GetCharName() +
                       " By Team :" + player.GetInGameData().GetTeam() + " Of Player ID :" + player.GetPlayerID());
                    if (LockInCharacter(player, curPlayerFrame.GetCharName()))
                        break;
                }
                //Player Unpick Character
                else if (playerController.getButtonAction(ACTIONS.UNPICK_CHARACTER))
                {
                    if (player.GetPickStatus())
                        DeselectCharacter(player);
                    else
                    {
                        switch (GameManager.Instance.GetGameMode())
                        {
                            case GAME_MODES.LOCAL_PVP:
                                GlobalUI.Instance.ToggleConfirmationDisplay(playerController, GameObject.FindWithTag("ChangeSceneButton").GetComponent<Button>(), EXECUTE_ACTION.BACK_TO_MAIN);
                                break;
                            case GAME_MODES.PRACTICE:
                                {
                                    if (player.GetPlayerID() == PLAYER.PLAYER_TWO) // Currently Controlling the AI
                                    {
                                        PlayerData actualPlayer = GameManager.Instance.GetPlayer(PLAYER.PLAYER_ONE);
                                        actualPlayer.SwapControllers(player);
                                        DeselectCharacter(actualPlayer);
                                    }
                                    else
                                    {
                                        backToSideSelect = true;
                                    }
                                }
                                break;
                        }
                    }
                }

            }
        }
    }


    public bool LockInCharacter(PlayerData player, string charaName)
    {
        player.GetInGameData().SetCharName(charaName);
        player.PickChar();
        GetPlayerFrame(player.GetPlayerID()).LockIn();

        if (GameManager.Instance.GetGameMode() == GAME_MODES.PRACTICE)
        {
            //Swap Controllers
            PlayerData opposingPlayer = null;
            if (player.GetPlayerID() == PLAYER.PLAYER_ONE)
            {
                opposingPlayer = GameManager.Instance.GetPlayer(PLAYER.PLAYER_TWO);
                if (!opposingPlayer.IsAssigned())
                {
                    opposingPlayer.Assign();
                    CreatePlayerFrame(opposingPlayer.GetPlayerID());
                }
            }
            else if (player.GetPlayerID() == PLAYER.PLAYER_TWO)
                opposingPlayer = GameManager.Instance.GetPlayer(PLAYER.PLAYER_ONE);

            opposingPlayer.SwapControllers(player);
            return true;
        }
        return false;
    }
    
    public void DeselectCharacter(PlayerData player)
    {
        //DeselectCharacter         
        player.UnPickChar();
        GetPlayerFrame(player.GetPlayerID()).UnLock();
    }

    public void DeselectCharacter(TEAM playerTeam)
    {
        //DeselectCharacter         
        GameManager.Instance.GetPlayer(playerTeam).UnPickChar();
        GetPlayerFrame(playerTeam).UnLock();
    }

    public void DeselectAllCharacters()
    {
        for (int i = 0; i < GameManager.Instance.GetPlayerSize(); ++i)
        {
            GameManager.Instance.GetPlayer(i).UnPickChar();
            GetPlayerFrame(i).UnLock();
        }
    }

    public CharSelectLocationScript GetPlayerFrame(PLAYER playerID)
    {
        CharSelectLocationScript curPlayerFrame = null;
        foreach (CharSelectLocationScript frame in playerFrames)
        {
            if (frame.GetPlayerID() == playerID)
            {
                curPlayerFrame = frame;
                return curPlayerFrame;
            }
        }
        return null;
    }

    public CharSelectLocationScript GetPlayerFrame(TEAM playerTeam)
    {
        PLAYER playerID = GameManager.Instance.GetPlayer(playerTeam).GetPlayerID();
        return GetPlayerFrame(playerID);
    }

    public CharSelectLocationScript GetPlayerFrame(int playerTeam)
    {
        PLAYER playerID = GameManager.Instance.GetPlayer(playerTeam).GetPlayerID();
        return GetPlayerFrame(playerID);
    }


    public void LockAllFrames()
    {
        for (int i = 0; i < playerFrames.Count; ++i)
            playerFrames[i].LockIn();
    }

    bool CheckBothPicked()
    {
        for (int i = 0; i < GameManager.Instance.GetPlayerSize(); ++i)
        {
            if (GameManager.Instance.GetPlayer(i).GetPickStatus() == false)
                return false;
        }
        return true;
    }
    

    public bool BackToSideSelect() { return backToSideSelect; }
    public void Reset() { backToSideSelect = false; }

    public bool FinishedPicking() { return finished; }
    public void UnFinish(TEAM cancelledTeam)
    {
        finished = false;
        if (GameManager.Instance.GetGameMode() == GAME_MODES.PRACTICE)
            DeselectAllCharacters();
        else
            DeselectCharacter(cancelledTeam);
    }

    public string GetCurrChara(TEAM playerteam)
    {
        if ((int)playerteam >= playerFrames.Count)
        {
            Debug.Log("Havent Created Frame Yet For Team : " + playerteam + " Returning Null. You entered a teamNumber :" + playerteam + " Current number of frames created : " + playerFrames.Count);
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

        return CharacterManager.GetInstance().GetCharacterByName(playerFrames[(int)playerteam].GetCharName()).GetCharArt();
    }

}
