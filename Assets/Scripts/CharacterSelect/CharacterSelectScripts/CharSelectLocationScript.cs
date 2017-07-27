using UnityEngine;
using System.Collections;

public class CharSelectLocationScript : MonoBehaviour 
{
    GameObject charSlot;
    CharacterSlot charData;
    bool lockedIn = false;
    AudioClip selectChar, pickChar;
    PLAYER playerID;

    void Start()
    {
        selectChar = AudioClipManager.GetInstance().GetAudioClip("Select");
        pickChar = AudioClipManager.GetInstance().GetAudioClip("PickChar");
    }

    public void AssignCharSlot(GameObject _charSlot) 
    { 
        this.charSlot = _charSlot;
        charData = _charSlot.GetComponent<CharacterSlot>();
        this.gameObject.transform.position = _charSlot.transform.position;
        SoundSystem.Instance.PlayClip(AUDIO_TYPE.SOUND_EFFECTS, selectChar, false, "SFX_Player" + ((int)playerID + 1));
    }

    public Vector3 GetSlotLocation() { return charSlot.transform.position; }
    public string GetCharName() { return charData.GetCharName(); }
    
    public void SetPlayerID(PLAYER id) { playerID = id; }
    public PLAYER GetPlayerID() { return playerID; }

    public void MoveLeft()
    {
        if (!lockedIn && charData && charData.left != null)
            AssignCharSlot(charData.left);
    }
    public void MoveRight()
    {
        if (!lockedIn && charData && charData.right != null)
            AssignCharSlot(charData.right);
    }
    public void MoveUp()
    {
        if (!lockedIn && charData && charData.up != null)
            AssignCharSlot(charData.up);
    }
    public void MoveDown()
    {
        if (!lockedIn && charData && charData.down != null)
            AssignCharSlot(charData.down);
    }
    public void LockIn() { lockedIn = true; SoundSystem.Instance.PlayClip(AUDIO_TYPE.SOUND_EFFECTS, pickChar, false, "SFX_Player" + ((int)playerID + 1)); }
    public void UnLock() { lockedIn = false; }
}
