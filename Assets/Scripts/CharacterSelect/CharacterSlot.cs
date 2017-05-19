using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CharacterSlot : MonoBehaviour
{
    public GameObject up = null, down = null, left = null, right = null;
    Image icon;
    CHARACTERS charName;

    void Awake()
    {
        icon = GetComponent<Image>();
    }

    public void SetImage(Image image)
    {
        icon.sprite = image.sprite;
    }

    public void SetChar(CHARACTERS chara) { charName = chara; }
    public CHARACTERS GetChar() { return charName; }

}
