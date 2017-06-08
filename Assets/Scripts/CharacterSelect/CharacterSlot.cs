using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CharacterSlot : MonoBehaviour
{
    //Links To the Next Character Slots
    public GameObject up = null, down = null, left = null, right = null;
    //Image To Be Display On the Slot
    Image icon;
    //Name Of Character To Display
    string charName;

    void Awake() { icon = transform.Find("SlotImage").GetComponent<Image>();}

    public void SetImageSprite(Sprite sprite) { icon.sprite = sprite; }
    public void SetCharName(string charName) { this.charName = charName; }
    public string GetCharName() { return charName; }

}
