using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CharacterSlot : MonoBehaviour
{
    public GameObject up = null, down = null, left = null, right = null;
    Image icon;
    //CHARACTERS charName;
    string charName;

    void Awake()
    {
        icon = GetComponent<Image>();
    }

    public void SetImageSprite(Sprite sprite)
    {
        icon.sprite = sprite;
    }

    //public void SetChar(CHARACTERS chara) { charName = chara; }
    //public CHARACTERS GetChar() { return charName; }
    public void SetCharName(string charName) { this.charName = charName; }
    public string GetCharName() { return charName; }

}
