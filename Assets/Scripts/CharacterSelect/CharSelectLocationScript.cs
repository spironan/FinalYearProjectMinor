using UnityEngine;
using System.Collections;

public class CharSelectLocationScript : MonoBehaviour {

    GameObject charSlot;
    CharacterSlot charData;
    bool playerLockedIn = false;
    public void AssignCharSlot(GameObject _charSlot) 
    { 
        this.charSlot = _charSlot;
        charData = _charSlot.GetComponent<CharacterSlot>();
        this.gameObject.transform.position = _charSlot.transform.position;
    }

    public Vector3 GetSlotLocation() { return charSlot.transform.position; }

    void Update()
    {
    }
    
    public CHARACTERS GetCharName()
    {
        return charData.GetChar();
    }

    public void MoveLeft()
    {
        if (!playerLockedIn && charData && charData.left != null)
            AssignCharSlot(charData.left);
    }

    public void MoveRight()
    {
        if (!playerLockedIn && charData && charData.right != null)
            AssignCharSlot(charData.right);
    }

    public void MoveUp()
    {
        if (!playerLockedIn && charData && charData.up != null)
            AssignCharSlot(charData.up);
    }

    public void MoveDown()
    {
        if (!playerLockedIn && charData && charData.down != null)
            AssignCharSlot(charData.down);
    }

    public void LockedIn()
    {
        playerLockedIn = true;
    }

}
